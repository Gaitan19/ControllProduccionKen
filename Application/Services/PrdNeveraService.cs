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
    public class PrdNeveraService : IPrdNeveraService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public PrdNeveraService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
        }

        public async Task CreateAsync(PrdNeveraDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var prdNevera = new PrdNevera
                {
                    IdMaquina = dto.IdMaquina,
                    Fecha = dto.Fecha,
                    HoraInicio = dto.HoraInicio,
                    HoraFin = dto.HoraFin,
                    TiempoParo = dto.TiempoParo,
                    Observaciones = dto.Observaciones,
                    IdUsuarioCreacion = dto.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    IdTipoReporte = 3, // Default tipo reporte for nevera
                    IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>()),
                    AprobadoSupervisor = false,
                    AprobadoGerencia = false
                };

                await _unitOfWork.PrdNeveraRepository.AddAsync(prdNevera);
                await _unitOfWork.SaveChangesAsync();
                var idPrdNevera = prdNevera.Id;

                if (dto.DetPrdNeveras != null && dto.DetPrdNeveras.Any())
                {
                    var detPrdNeveras = dto.DetPrdNeveras
         .Select((detalle, index) => new DetPrdNevera
         {
             PrdNeveraId = idPrdNevera,
             Posicion = index + 1, // index inicia en 0, así que sumamos 1
             IdArticulo = detalle.IdArticulo,
             CantidadConforme = detalle.CantidadConforme,
             CantidadNoConforme = detalle.CantidadNoConforme,
             IdTipoFabricacion = detalle.IdTipoFabricacion,
             NumeroPedido = detalle.NumeroPedido,
             IdUsuarioCreacion = dto.IdUsuarioCreacion,
             FechaCreacion = prdNevera.FechaCreacion
         });

                    await _unitOfWork.DetPrdNeveraRepository.BulkInsertAsync(detPrdNeveras);
                }

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ShowPrdNeveraDto>> GetAllAsync()
        {
            var maquinas = await _unitOfWork.CatMaquinaRepository.GetAllAsync();
            var productions = await _unitOfWork.PrdNeveraRepository.GetAllAsync(
                x => x.DetPrdNeveras);

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
                return new ShowPrdNeveraDto
                {
                    Id = p.Id,
                    IdTipoReporte = p.IdTipoReporte,
                    Operarios = string.Join(", ", userNames),
                    Maquina = maquinas.FirstOrDefault(x => x.Id == p.IdMaquina)?.Nombre,
                    Fecha = p.Fecha,
                    AprobadoSupervisor = p.AprobadoSupervisor,
                    AprobadoGerencia = p.AprobadoGerencia,
                    IdAprobadoSupervisor = p.IdAprobadoSupervisor,
                    IdAprobadoGerencia = p.IdAprobadoGerencia
                };
            });

            return dtos.OrderByDescending(x => x.Id);
        }

        public async Task<PrdNeveraDto> GetByIdAsync(int id)
        {
            var prd = await _unitOfWork.PrdNeveraRepository
                .GetByIdIncludeAsync(x => x.Id == id,
                    x => x.DetPrdNeveras);
            if (prd == null) return null;

            var userIds = prd.IdUsuarios.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            return new PrdNeveraDto
            {
                Id = prd.Id,
                IdUsuarios = userIds,
                IdMaquina = prd.IdMaquina,
                Fecha = prd.Fecha,
                HoraInicio = prd.HoraInicio,
                HoraFin = prd.HoraFin,
                TiempoParo = prd.TiempoParo,
                Observaciones = prd.Observaciones,
                IdUsuarioCreacion = prd.IdUsuarioCreacion,
                FechaCreacion = prd.FechaCreacion,
                IdUsuarioActualizacion = prd.IdUsuarioActualizacion,
                FechaActualizacion = prd.FechaActualizacion,
                IdTipoReporte = prd.IdTipoReporte,
                AprobadoSupervisor = prd.AprobadoSupervisor,
                AprobadoGerencia = prd.AprobadoGerencia,
                IdAprobadoSupervisor = prd.IdAprobadoSupervisor,
                IdAprobadoGerencia = prd.IdAprobadoGerencia,
                NotaSupervisor = prd.NotaSupervisor,
                DetPrdNeveras = prd.DetPrdNeveras.Select(entity => new DetPrdNeveraDTO
                {
                    Id = entity.Id,
                    PrdNeveraId = entity.PrdNeveraId,
                    Posicion = entity.Posicion,
                    IdArticulo = entity.IdArticulo,
                    CantidadConforme = entity.CantidadConforme,
                    CantidadNoConforme = entity.CantidadNoConforme,
                    IdTipoFabricacion = entity.IdTipoFabricacion,
                    NumeroPedido = entity.NumeroPedido,
                    IdUsuarioCreacion = entity.IdUsuarioCreacion,
                    FechaCreacion = entity.FechaCreacion,
                    IdUsuarioActualizacion = entity.IdUsuarioActualizacion,
                    FechaActualizacion = entity.FechaActualizacion
                }).ToList()
            };
        }

        public async Task<CrearPrdNeveraDto> GetCreateData()
        {


            var CatMaquina = await _unitOfWork.CatMaquinaRepository.FindAsync(x => x.Activo == true);

            var dto = new CrearPrdNeveraDto
            {
                CatMaquina = _mapper.Map<List<MaquinaDto>>(CatMaquina.ToList()),
                CatalogoNeveras = (await _unitOfWork.MaestroCatalogoRepository.FindAsync(x => x.Activo == true && x.IdPadre==1)).ToList(),
                CatTipoFabricacion = _mapper.Map<List<TipoFabricacionDto>>(
                    (await _unitOfWork.TipoFabricacionRepository.FindAsync(x => x.Activo == true)).ToList())
            };

            return dto;
        }

        public async Task<bool> ValidatePrdNeveraByIdAsync(int id, string userId)
        {
            var prdNevera = await _unitOfWork.PrdNeveraRepository.GetByIdAsync(id);
            if (prdNevera == null)
            {
                return false;
            }

            if (prdNevera.AprobadoSupervisor)
            {
                return false;
            }

            prdNevera.AprobadoSupervisor = true;
            prdNevera.IdAprobadoSupervisor = userId;
            prdNevera.IdUsuarioActualizacion = userId;
            prdNevera.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdNeveraRepository.Update(prdNevera);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AprovePrdNeveraByIdAsync(int id, string userId)
        {
            var prdNevera = await _unitOfWork.PrdNeveraRepository.GetByIdAsync(id);
            if (prdNevera == null)
            {
                return false;
            }

            if (prdNevera.AprobadoGerencia)
            {
                return false;
            }

            prdNevera.AprobadoGerencia = true;
            prdNevera.IdAprobadoGerencia = userId;
            prdNevera.IdUsuarioActualizacion = userId;
            prdNevera.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdNeveraRepository.Update(prdNevera);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId)
        {
            var prd = await _unitOfWork.PrdNeveraRepository.GetByIdAsync(id);
            if (prd == null)
            {
                return false;
            }

            prd.NotaSupervisor = notaSupervisor;
            prd.IdUsuarioActualizacion = userId;
            prd.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdNeveraRepository.Update(prd);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAsync(PrdNeveraDto dto)
        {
            var prd = await _unitOfWork.PrdNeveraRepository.GetByIdAsync(dto.Id);
            if (prd != null)
            {
                prd.IdMaquina = dto.IdMaquina;
                prd.Fecha = dto.Fecha;
                prd.HoraInicio = dto.HoraInicio;
                prd.HoraFin = dto.HoraFin;
                prd.TiempoParo = dto.TiempoParo;
                prd.Observaciones = dto.Observaciones;
                prd.IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>());
                prd.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                prd.FechaActualizacion = DateTime.Now;
                _unitOfWork.PrdNeveraRepository.Update(prd);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetPrd(DetPrdNeveraDTO dto)
        {
            var det = await _unitOfWork.DetPrdNeveraRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
              //  det.Posicion = dto.Posicion;
                det.IdArticulo = dto.IdArticulo;
                det.CantidadConforme = dto.CantidadConforme;
                det.CantidadNoConforme = dto.CantidadNoConforme;
                det.IdTipoFabricacion = dto.IdTipoFabricacion;
                det.NumeroPedido = dto.NumeroPedido;
                det.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                det.FechaActualizacion = DateTime.Now;
                _unitOfWork.DetPrdNeveraRepository.Update(det);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteDetPrd(DetPrdNeveraDTO dto)
        {
            var det = await _unitOfWork.DetPrdNeveraRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                _unitOfWork.DetPrdNeveraRepository.Remove(det);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<PrdNeveraReporteDTO>> GetAllPrdNeveraWithDetailsAsync(DateTime start, DateTime end)
        {
            return _unitOfWork.ReportesDapperRepository.GetAllPrdNeveraWithDetailsAsync(start, end);
        }
    }
}
