using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.DTO;
using Infrastructure.Models;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services
{
    public class PrdMallasCovintecService : IPrdMallasCovintecService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public PrdMallasCovintecService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
        }

        public async Task CreateAsync(PrdMallaCovintecDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var catalogoMallas = await _unitOfWork.CatalogoMallasCovintecRepository.GetAllAsync();
                await _unitOfWork.BeginTransactionAsync();

                var prdMalla = new PrdMallaCovintec
                {
                    IdMaquina = dto.IdMaquina,
                    Fecha = dto.Fecha ?? DateTime.Now,
                    Observaciones = dto.Observaciones,
                    MermaAlambre = dto.MermaAlambre,
                    TiempoParo = dto.TiempoParo,
                    IdUsuarioCreacion = dto.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    IdTipoReporte = dto.IdTipoReporte ?? 9,
                    IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>()),
                    AprobadoSupervisor = false,
                    AprobadoGerencia = false
                };
                await _unitOfWork.PrdMallaCovintecRepository.AddAsync(prdMalla);
                await _unitOfWork.SaveChangesAsync();
                var idPrdMalla = prdMalla.Id;

                if (dto.DetPrdMallasCovintecs != null)
                {
                    foreach (var detalle in dto.DetPrdMallasCovintecs)
                    {
                        var produccionDia = (detalle.CantidadProducida - detalle.CantidadNoConforme)
                            * (catalogoMallas.FirstOrDefault(x => x.Id == detalle.IdArticulo)?.LongitudCentimetros ?? 0);
                        var detallePrd = new DetPrdMallaCovintec
                        {
                            IdMalla = idPrdMalla,
                            IdArticulo = detalle.IdArticulo,
                            CantidadProducida = detalle.CantidadProducida,
                            CantidadNoConforme = detalle.CantidadNoConforme,
                            IdTipoFabricacion = detalle.IdTipoFabricacion,
                            NumeroPedido = detalle.NumeroPedido ?? 0,
                            ProduccionDia = produccionDia,
                            AprobadoSupervisor = false,
                            AprobadoGerencia = false,
                            IdUsuarioCreacion = dto.IdUsuarioCreacion,
                            FechaCreacion = prdMalla.FechaCreacion
                        };
                        await _unitOfWork.DetPrdMallaCovintecRepository.AddAsync(detallePrd);
                    }
                }

                if (dto.DetAlambrePrdMallasCovintecs != null)
                {
                    foreach (var detAl in dto.DetAlambrePrdMallasCovintecs)
                    {
                        var detalleAl = new DetAlambrePrdMallaCovintec
                        {
                            IdMalla = idPrdMalla,
                            NumeroAlambre = detAl.NumeroAlambre,
                            PesoAlambre = detAl.PesoAlambre,
                            AprobadoSupervisor = false,
                            AprobadoGerencia = false,
                            IdUsuarioCreacion = dto.IdUsuarioCreacion,
                            FechaCreacion = prdMalla.FechaCreacion
                        };
                        await _unitOfWork.DetAlambrePrdMallaCovintecRepository.AddAsync(detalleAl);
                    }
                }

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
         
        }

        public async Task<IEnumerable<ShowPrdMallasCovintecDto>> GetAllAsync()
        {
            var maquinas = await _unitOfWork.CatMaquinaRepository.GetAllAsync();
            var productions = await _unitOfWork.PrdMallaCovintecRepository.GetAllAsync(
                x => x.DetAlambrePrdMallaCovintecs,
                x => x.DetPrdMallaCovintecs);

            var distinctUserIds = productions
                .SelectMany(p => p.IdUsuarios.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .Distinct()
                .ToList();
            var identityUsers = await _userManager.Users
                .Where(u => distinctUserIds.Contains(u.Id))
                .ToListAsync();
            var userDictionary = identityUsers.ToDictionary(u => u.Id, u => u.UserName);

            var dtos = productions.Select(p =>
            {
                var userNames = p.IdUsuarios.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => userDictionary.ContainsKey(id) ? userDictionary[id] : id);
                return new ShowPrdMallasCovintecDto
                {
                    Id = p.Id,
                    IdTipoReporte = p.IdTipoReporte,
                    Operarios = string.Join(", ", userNames),
                    Maquina = maquinas.FirstOrDefault(x => x.Id == p.IdMaquina)?.Nombre,
                    Fecha = p.Fecha,
                    AprobadoSupervisor = p.AprobadoSupervisor,
                    AprobadoGerencia = p.AprobadoGerencia
                };
            });

            return dtos.OrderByDescending(x => x.Id);
        }

        public async Task<PrdMallaCovintecDto> GetByIdAsync(int id)
        {
            var prd = await _unitOfWork.PrdMallaCovintecRepository
                .GetByIdIncludeAsync(x => x.Id == id,
                    x => x.DetAlambrePrdMallaCovintecs,
                    x => x.DetPrdMallaCovintecs);
            if (prd == null) return null;

            var userIds = prd.IdUsuarios.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            return new PrdMallaCovintecDto
            {
                Id = prd.Id,
                IdUsuarios = userIds,
                IdMaquina = prd.IdMaquina,
                Fecha = prd.Fecha,
                Observaciones = prd.Observaciones,
                MermaAlambre = prd.MermaAlambre ?? 0,
                TiempoParo = prd.TiempoParo,
                IdUsuarioCreacion = prd.IdUsuarioCreacion,
                FechaCreacion = prd.FechaCreacion,
                AprobadoSupervisor = prd.AprobadoSupervisor,
                AprobadoGerencia = prd.AprobadoGerencia,
                DetPrdMallasCovintecs = prd.DetPrdMallaCovintecs.Select(entity => new DetPrdMallaCovintecDTO
                {
                    Id = entity.Id,
                    IdMalla = entity.IdMalla,
                    IdArticulo = entity.IdArticulo,
                    CantidadProducida = entity.CantidadProducida,
                    CantidadNoConforme = entity.CantidadNoConforme,
                    IdTipoFabricacion = entity.IdTipoFabricacion,
                    NumeroPedido = entity.NumeroPedido,
                    ProduccionDia = entity.ProduccionDia,
                    AprobadoSupervisor = entity.AprobadoSupervisor,
                    AprobadoGerencia = entity.AprobadoGerencia,
                    IdUsuarioCreacion = entity.IdUsuarioCreacion,
                    FechaCreacion = entity.FechaCreacion,
                    IdUsuarioActualizacion = entity.IdUsuarioActualizacion,
                    FechaActualizacion = entity.FechaActualizacion
                }).ToList(),
                DetAlambrePrdMallasCovintecs = prd.DetAlambrePrdMallaCovintecs.Select(entity => new DetAlambrePrdMallaCovintecDTO
                {
                    Id = entity.Id,
                    IdMalla = entity.IdMalla,
                    NumeroAlambre = entity.NumeroAlambre,
                    PesoAlambre = entity.PesoAlambre,
                    AprobadoSupervisor = entity.AprobadoSupervisor,
                    AprobadoGerencia = entity.AprobadoGerencia,
                    IdUsuarioCreacion = entity.IdUsuarioCreacion,
                    FechaCreacion = entity.FechaCreacion,
                    IdUsuarioActualizacion = entity.IdUsuarioActualizacion,
                    FechaActualizacion = entity.FechaActualizacion
                }).ToList()
            };
        }

        public async Task<CrearPrdMallaCovintecDto> GetCreateData()
        {
            var allowedMachines = (await _unitOfWork.catalogosPermitidosPorReporteRepository.FindAsync(cp =>
                cp.IdTipoReporte == 8 && cp.Catalogo == "cp.Maquinas"))
                .Select(x => x.IdCatalogo).ToList();

            var CatMaquina = allowedMachines.Any()
                ? await _unitOfWork.CatMaquinaRepository.FindAsync(x => allowedMachines.Contains(x.Id))
                : await _unitOfWork.CatMaquinaRepository.FindAsync(x => true);

            var dto = new CrearPrdMallaCovintecDto
            {
                CatMaquina = _mapper.Map<List<MaquinaDto>>(CatMaquina.Where(x => x.Activo == true).ToList()),
                CatalogoMallas = _mapper.Map<List<CatalogoMallasCovintecDTO>>(await _unitOfWork.CatalogoMallasCovintecRepository.GetAllAsync()),
                CatTipoFabricacion = _mapper.Map<List<TipoFabricacionDto>>(
                    (await _unitOfWork.TipoFabricacionRepository.FindAsync(x => x.Activo == true)).ToList())
            };

            return dto;
        }

        public async Task<bool> ValidateDetAlambrePrdMallaCovintecByIdAsync(int id, string userId)
        {
            var det = await _unitOfWork.DetAlambrePrdMallaCovintecRepository.GetByIdAsync(id);

            if (det.AprobadoSupervisor != true)
            {
                if (det.AprobadoSupervisor)
                    det.AprobadoSupervisor = false;
                else
                    det.AprobadoSupervisor = true;

                det.IdAprobadoSupervisor = userId;
                det.FechaActualizacion = DateTime.Now;
                det.IdUsuarioActualizacion = userId;

                _unitOfWork.DetAlambrePrdMallaCovintecRepository.Update(det);
                await _unitOfWork.SaveChangesAsync();
            }

            return det.AprobadoSupervisor;
        }

        public async Task<bool> ValidateDetPrdMallaCovintecByIdAsync(int id, string userId)
        {
            var det = await _unitOfWork.DetPrdMallaCovintecRepository.GetByIdAsync(id);

            if (det.AprobadoSupervisor != true)
            {
                if (det.AprobadoSupervisor)
                    det.AprobadoSupervisor = false;
                else
                    det.AprobadoSupervisor = true;

                det.IdAprobadoSupervisor = userId;
                det.FechaActualizacion = DateTime.Now;
                det.IdUsuarioActualizacion = userId;

                _unitOfWork.DetPrdMallaCovintecRepository.Update(det);
                await _unitOfWork.SaveChangesAsync();
            }

            return det.AprobadoSupervisor;
        }

        public async Task<bool> AproveDetAlambrePrdMallaCovintecByIdAsync(int id, string userId)
        {
            var det = await _unitOfWork.DetAlambrePrdMallaCovintecRepository.GetByIdAsync(id);

            if (det.AprobadoGerencia != true)
            {
                if (det.AprobadoGerencia)
                    det.AprobadoGerencia = false;
                else
                    det.AprobadoGerencia = true;

                det.IdAprobadoGerencia = userId;
                det.FechaActualizacion = DateTime.Now;
                det.IdUsuarioActualizacion = userId;

                _unitOfWork.DetAlambrePrdMallaCovintecRepository.Update(det);
                await _unitOfWork.SaveChangesAsync();
            }

            return det.AprobadoGerencia;
        }

        public async Task<bool> AproveDetPrdMallaCovintecByIdAsync(int id, string userId)
        {
            var det = await _unitOfWork.DetPrdMallaCovintecRepository.GetByIdAsync(id);

            if (det.AprobadoGerencia != true)
            {
                if (det.AprobadoGerencia)
                    det.AprobadoGerencia = false;
                else
                    det.AprobadoGerencia = true;

                det.IdAprobadoGerencia = userId;
                det.FechaActualizacion = DateTime.Now;
                det.IdUsuarioActualizacion = userId;

                _unitOfWork.DetPrdMallaCovintecRepository.Update(det);
                await _unitOfWork.SaveChangesAsync();
            }

            return det.AprobadoGerencia;
        }


        public async Task UpdateAsync(PrdMallaCovintecDto dto)
        {
            var prd = await _unitOfWork.PrdMallaCovintecRepository.GetByIdAsync(dto.Id);
            if (prd != null)
            {
                prd.IdMaquina = dto.IdMaquina;
                prd.Fecha = dto.Fecha ?? prd.Fecha;
                prd.Observaciones = dto.Observaciones;
                prd.MermaAlambre = dto.MermaAlambre;
                prd.TiempoParo = dto.TiempoParo;
                prd.IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>());
                prd.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                prd.FechaActualizacion = DateTime.Now;
                _unitOfWork.PrdMallaCovintecRepository.Update(prd);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetPrd(DetPrdMallaCovintecDTO dto)
        {
            var det = await _unitOfWork.DetPrdMallaCovintecRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                det.IdArticulo = dto.IdArticulo;
                det.CantidadProducida = dto.CantidadProducida;
                det.CantidadNoConforme = dto.CantidadNoConforme;
                det.IdTipoFabricacion = dto.IdTipoFabricacion;
                det.NumeroPedido = dto.NumeroPedido;
                var mts = (await _unitOfWork.CatalogoMallasCovintecRepository.GetByIdAsync(dto.IdArticulo))?.LongitudCentimetros ?? 0;
                det.ProduccionDia = (dto.CantidadProducida - dto.CantidadNoConforme) * mts;
                det.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                det.FechaActualizacion = DateTime.Now;
                _unitOfWork.DetPrdMallaCovintecRepository.Update(det);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteDetPrd(DetPrdMallaCovintecDTO dto)
        {
            var det = await _unitOfWork.DetPrdMallaCovintecRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                _unitOfWork.DetPrdMallaCovintecRepository.Remove(det);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetAlambrePrd(DetAlambrePrdMallaCovintecDTO dto)
        {
            var det = await _unitOfWork.DetAlambrePrdMallaCovintecRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                det.NumeroAlambre = dto.NumeroAlambre;
                det.PesoAlambre = dto.PesoAlambre;
                det.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                det.FechaActualizacion = DateTime.Now;
                _unitOfWork.DetAlambrePrdMallaCovintecRepository.Update(det);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PrdMallasCovintecReporteDTO>> GetAllMallasProduccionWithDetailsAsync(DateTime start, DateTime end)
        {
            return await _unitOfWork.ReportesDapperRepository.GetAllMallaProduccionWithDetailsAsync(start, end);
        }

        public async Task<bool> AprovePrdMallaCovintecByIdAsync(int id, string userId)
        {
            var prd =await _unitOfWork.PrdMallaCovintecRepository.GetByIdAsync(id);
            if (prd == null) return await Task.FromResult(false);
            if (prd.AprobadoGerencia != true)
            {
                if (prd.AprobadoGerencia)
                    prd.AprobadoGerencia = false;
                else
                    prd.AprobadoGerencia = true;
                prd.IdAprobadoGerencia = userId;
                prd.FechaActualizacion = DateTime.Now;
                prd.IdUsuarioActualizacion = userId;
                _unitOfWork.PrdMallaCovintecRepository.Update(prd);
                await _unitOfWork.SaveChangesAsync();
            }
            return await Task.FromResult(prd.AprobadoGerencia);
        }

        public async Task<bool> ValidatePrdMallaCovintecByIdAsync(int id, string userId)
        {
            var prd = await _unitOfWork.PrdMallaCovintecRepository.GetByIdAsync(id);
            if (prd == null) return await Task.FromResult(false);
            if (prd.AprobadoSupervisor != true)
            {
                if (prd.AprobadoSupervisor)
                    prd.AprobadoSupervisor = false;
                else
                    prd.AprobadoSupervisor = true;
                prd.IdAprobadoSupervisor = userId;
                prd.FechaActualizacion = DateTime.Now;
                prd.IdUsuarioActualizacion = userId;
                _unitOfWork.PrdMallaCovintecRepository.Update(prd);
                await _unitOfWork.SaveChangesAsync();
            }
            return await Task.FromResult(prd.AprobadoSupervisor);
        }

        public async Task<bool> UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId)
        {
            var prd = await _unitOfWork.PrdMallaCovintecRepository.GetByIdAsync(id);
            if (prd == null)
            {
                return false;
            }

            prd.NotaSupervisor = notaSupervisor;
            prd.IdUsuarioActualizacion = userId;
            prd.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdMallaCovintecRepository.Update(prd);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
