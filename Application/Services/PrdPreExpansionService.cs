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
    public class PrdPreExpansionService : IPrdPreExpansionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public PrdPreExpansionService(
             IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
                 
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<bool> ApprobarSupervisorAsync(int id, string userId)
        {
            var prd = await _unitOfWork.PrdpreExpansionRepository.GetByIdAsync(id);
            if (prd == null)
            {
                return false;
            }

            if (prd.AprobadoSupervisor)
            {
                return false;
            }

            prd.AprobadoSupervisor = true;
            prd.IdAprobadoSupervisor = userId;
            prd.IdUsuarioActualizacion = userId;
            prd.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdpreExpansionRepository.Update(prd);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AprobarGerenciaAsync(int id, string userId)
        {
            var prd = await _unitOfWork.PrdpreExpansionRepository.GetByIdAsync(id);
            if (prd == null)
            {
                return false;
            }

            if (prd.AprobadoGerencia)
            {
                return false;
            }

            prd.AprobadoGerencia = true;
            prd.IdAprobadoGerencia = userId;
            prd.IdUsuarioActualizacion = userId;
            prd.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdpreExpansionRepository.Update(prd);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<int> CreateAsync(PrdpreExpansionDto prdPreExpansion)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entity = new PrdpreExpansion
                {
                    IdMaquina = prdPreExpansion.IdMaquina,
                    Fecha = prdPreExpansion.Fecha,
                    HoraInicio = prdPreExpansion.HoraInicio,
                    HoraFin = prdPreExpansion.HoraFin,
                    TiempoParo = prdPreExpansion.TiempoParo.HasValue ? (int?)Convert.ToInt32(prdPreExpansion.TiempoParo.Value) : null,
                    Observaciones = prdPreExpansion.Observaciones,
                    IdTipoReporte = prdPreExpansion.IdTipoReporte,
                    IdUsuarios = string.Join(",", prdPreExpansion.IdUsuarios ?? new List<string>()),
                    PresionCaldera = prdPreExpansion.PresionCaldera,
                    IdTipoFabricacion = prdPreExpansion.IdTipoFabricacion,
                    NumeroPedido = prdPreExpansion.NumeroPedido,
                    IdUsuarioCreacion = prdPreExpansion.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    AprobadoSupervisor = false,
                    AprobadoGerencia = false
                };

                await _unitOfWork.PrdpreExpansionRepository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                var prdId = entity.Id;

                if (prdPreExpansion.PreDetPrdpreExpansions != null && prdPreExpansion.PreDetPrdpreExpansions.Any())
                {
                    foreach (var preDet in prdPreExpansion.PreDetPrdpreExpansions)
                    {
                        var preDetEntity = new PreDetPrdpreExpansion
                        {
                            PrdpreExpansionId = prdId,
                            MarcaTipo = preDet.MarcaTipo,
                            CodigoSaco = preDet.CodigoSaco,
                            Lote = preDet.Lote,
                            FechaProduccion = preDet.FechaProduccion,
                            FechaCreacion = DateTime.Now
                        };

                        await _unitOfWork.PreDetPrdpreExpansionRepository.AddAsync(preDetEntity);
                        await _unitOfWork.SaveChangesAsync();
                        var preDetId = preDetEntity.Id;

                        if (preDet.DetPrdpreExpansions != null && preDet.DetPrdpreExpansions.Any())
                        {
                            var detalles = preDet.DetPrdpreExpansions.Select(det => new DetPrdpreExpansion
                            {
                                PreDetPrdpreExpansionId = preDetId,
                                Hora = det.Hora,
                                NoBatch = det.NoBatch,
                                DensidadEsperada = det.DensidadEsperada,
                                PesoBatchGr = det.PesoBatchGr,
                                Densidad = det.Densidad,
                                KgPorBatch = det.KgPorBatch,
                                PresionPsi = det.PresionPsi,
                                TiempoBatchSeg = det.TiempoBatchSeg,
                                TemperaturaC = det.TemperaturaC,
                                Silo = det.Silo,
                                Paso = det.Paso,
                                IdUsuarioCreacion = prdPreExpansion.IdUsuarioCreacion,
                                FechaCreacion = DateTime.Now
                            });

                            await _unitOfWork.DetPrdpreExpansionRepository.BulkInsertAsync(detalles);
                        }
                    }
                }

                await _unitOfWork.CommitAsync();
                return prdId;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.PrdpreExpansionRepository.GetByIdAsync(id);
            if (entity != null)
            {
                _unitOfWork.PrdpreExpansionRepository.Remove(entity);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ShowPrdPreExpansionDto>> GetAllAsync()
        {
            var maquinas = await _unitOfWork.CatMaquinaRepository.GetAllAsync();
            var productions = await _unitOfWork.PrdpreExpansionRepository.GetAllAsync();

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
                return new ShowPrdPreExpansionDto
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

        public Task<IEnumerable<PrdPreExpansionReporteDTO>> GetAllPrdPreExpansionWithDetailsAsync(DateTime start, DateTime end)
        {
            return _unitOfWork.ReportesDapperRepository.GetAllPrdPreExpansionWithDetailsAsync(start, end);
        }

        public async Task<PrdpreExpansionDto> GetByIdAsync(int id)
        {
            var prd = await _unitOfWork.PrdpreExpansionRepository
                .GetByIdIncludeAsync(x => x.Id == id,
                    x => x.PreDetPrdpreExpansions);
            if (prd == null) return null!;

            var userIds = prd.IdUsuarios.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

            // Load the sub-details for each pre-detail
            var preDetPrdpreExpansions = new List<PreDetPrdpreExpansionDTO>();
            if (prd.PreDetPrdpreExpansions != null)
            {
                foreach (var preDet in prd.PreDetPrdpreExpansions)
                {
                    var preDetDto = new PreDetPrdpreExpansionDTO
                    {
                        Id = preDet.Id,
                        PrdpreExpansionId = preDet.PrdpreExpansionId,
                        MarcaTipo = preDet.MarcaTipo,
                        CodigoSaco = preDet.CodigoSaco,
                        Lote = preDet.Lote,
                        FechaProduccion = preDet.FechaProduccion,
                        FechaCreacion = preDet.FechaCreacion,
                        IdUsuarioActualizacion = preDet.IdUsuarioActualizacion,
                        FechaActualizacion = preDet.FechaActualizacion
                    };

                    // Load the details for this pre-detail
                    var detPrdpreExpansions = await _unitOfWork.DetPrdpreExpansionRepository
                        .FindAsync(x => x.PreDetPrdpreExpansionId == preDet.Id);

                    preDetDto.DetPrdpreExpansions = detPrdpreExpansions.Select(det => new DetPrdpreExpansionDTO
                    {
                        Id = det.Id,
                        PreDetPrdpreExpansionId = det.PreDetPrdpreExpansionId,
                        Hora = det.Hora,
                        NoBatch = det.NoBatch,
                        DensidadEsperada = det.DensidadEsperada,
                        PesoBatchGr = det.PesoBatchGr,
                        Densidad = det.Densidad,
                        KgPorBatch = det.KgPorBatch,
                        PresionPsi = det.PresionPsi,
                        TiempoBatchSeg = det.TiempoBatchSeg,
                        TemperaturaC = det.TemperaturaC,
                        Silo = det.Silo,
                        Paso = det.Paso,
                        IdUsuarioCreacion = det.IdUsuarioCreacion,
                        FechaCreacion = det.FechaCreacion,
                        IdUsuarioActualizacion = det.IdUsuarioActualizacion,
                        FechaActualizacion = det.FechaActualizacion
                    }).ToList();

                    preDetPrdpreExpansions.Add(preDetDto);
                }
            }

            return new PrdpreExpansionDto
            {
                Id = prd.Id,
                IdUsuarios = userIds,
                IdMaquina = prd.IdMaquina,
                Fecha = prd.Fecha,
                HoraInicio = prd.HoraInicio,
                HoraFin = prd.HoraFin,
                TiempoParo = prd.TiempoParo.HasValue ? (decimal?)prd.TiempoParo.Value : null,
                Observaciones = prd.Observaciones,
                IdTipoReporte = prd.IdTipoReporte,
                PresionCaldera = prd.PresionCaldera,
                IdTipoFabricacion = prd.IdTipoFabricacion,
                NumeroPedido = prd.NumeroPedido,
                IdUsuarioCreacion = prd.IdUsuarioCreacion,
                FechaCreacion = prd.FechaCreacion,
                IdUsuarioActualizacion = prd.IdUsuarioActualizacion,
                FechaActualizacion = prd.FechaActualizacion,
                AprobadoSupervisor = prd.AprobadoSupervisor,
                AprobadoGerencia = prd.AprobadoGerencia,
                IdAprobadoSupervisor = prd.IdAprobadoSupervisor,
                IdAprobadoGerencia = prd.IdAprobadoGerencia,
                NotaSupervisor = prd.NotaSupervisor,
                PreDetPrdpreExpansions = preDetPrdpreExpansions
            };
        }

        public async Task<CrearPrdPreExpansionDto> GetCatalogosAsync()
        {
            var maquinas = await _unitOfWork.CatMaquinaRepository.FindAsync(x => x.Activo == true);
            var tipos = await _unitOfWork.TipoFabricacionRepository.FindAsync(x => x.Activo == true);

            var dto = new CrearPrdPreExpansionDto
            {
                CatMaquina = _mapper.Map<List<MaquinaDto>>(maquinas.ToList()),
                CatTipoFabricacion = _mapper.Map<List<TipoFabricacionDto>>(tipos.ToList())
            };

            return dto;
        }

        public async Task UpdateAsync(PrdpreExpansionDto prdPreExpansion)
        {
            var entity = await _unitOfWork.PrdpreExpansionRepository.GetByIdAsync(prdPreExpansion.Id);
            if (entity != null)
            {
                entity.IdMaquina = prdPreExpansion.IdMaquina;
                entity.Fecha = prdPreExpansion.Fecha;
                entity.HoraInicio = prdPreExpansion.HoraInicio;
                entity.HoraFin = prdPreExpansion.HoraFin;
                entity.TiempoParo = prdPreExpansion.TiempoParo.HasValue ? (int?)Convert.ToInt32(prdPreExpansion.TiempoParo.Value) : null;
                entity.Observaciones = prdPreExpansion.Observaciones;
                entity.IdUsuarios = string.Join(",", prdPreExpansion.IdUsuarios ?? new List<string>());
                entity.PresionCaldera = prdPreExpansion.PresionCaldera;
                entity.IdTipoFabricacion = prdPreExpansion.IdTipoFabricacion;
                entity.NumeroPedido = prdPreExpansion.NumeroPedido;
                entity.IdUsuarioActualizacion = prdPreExpansion.IdUsuarioActualizacion;
                entity.FechaActualizacion = DateTime.Now;

                _unitOfWork.PrdpreExpansionRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetPrd(DetPrdpreExpansionDTO dto)
        {
            var det = await _unitOfWork.DetPrdpreExpansionRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                det.Hora = dto.Hora;
                det.NoBatch = dto.NoBatch;
                det.DensidadEsperada = dto.DensidadEsperada;
                det.PesoBatchGr = dto.PesoBatchGr;
                det.Densidad = dto.Densidad;
                det.KgPorBatch = dto.KgPorBatch;
                det.PresionPsi = dto.PresionPsi;
                det.TiempoBatchSeg = dto.TiempoBatchSeg;
                det.TemperaturaC = dto.TemperaturaC;
                det.Silo = dto.Silo;
                det.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                det.FechaActualizacion = DateTime.Now;

                _unitOfWork.DetPrdpreExpansionRepository.Update(det);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdatePreDetPrd(PreDetPrdpreExpansionDTO dto)
        {
            var preDet = await _unitOfWork.PreDetPrdpreExpansionRepository.GetByIdAsync(dto.Id);
            if (preDet != null)
            {
                preDet.MarcaTipo = dto.MarcaTipo;
                preDet.CodigoSaco = dto.CodigoSaco;
                preDet.Lote = dto.Lote;
                preDet.FechaProduccion = dto.FechaProduccion;
                preDet.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                preDet.FechaActualizacion = DateTime.Now;

                _unitOfWork.PreDetPrdpreExpansionRepository.Update(preDet);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId)
        {
            var prd = await _unitOfWork.PrdpreExpansionRepository.GetByIdAsync(id);
            if (prd == null)
            {
                return false;
            }

            prd.NotaSupervisor = notaSupervisor;
            prd.IdUsuarioActualizacion = userId;
            prd.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdpreExpansionRepository.Update(prd);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


       

           
         



    }
}
