using Application.DTOs;
using Application.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Models;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PrdMallaPchService : IPrdMallaPchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private const decimal FactorProd = 1.24M;
        public PrdMallaPchService(
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task CreateAsync(PrdMallaPchDTO dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var prdMallaPch = new PrdMallaPch
                {
                    IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>()),
                    Fecha = dto.Fecha,
                    Observaciones = dto.Observaciones,
                    ProduccionDia = 0, // Will be calculated from details
                    TiempoParo = dto.TiempoParo,
                    IdUsuarioCreacion = dto.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    AprobadoSupervisor = false,
                    AprobadoGerencia = false
                };

                await _unitOfWork.PrdMallaPchRepository.AddAsync(prdMallaPch);
                await _unitOfWork.SaveChangesAsync();
                var idPrdMallaPch = prdMallaPch.Id;

                decimal totalProduccion = 0;

                // Create DetPrdPchMaquinaA records
                if (dto.DetPrdPchMaquinaAs != null && dto.DetPrdPchMaquinaAs.Any())
                {
                    var detPrdPchMaquinaAs = dto.DetPrdPchMaquinaAs.Select(detalle => new DetPrdPchMaquinaA
                    {
                        PrdMallaPchId = idPrdMallaPch,
                        IdMaquina = detalle.IdMaquina,
                        HilosTransversalesUn = detalle.HilosTransversalesUn,
                        MermaHilosTransversalesKg = detalle.MermaHilosTransversalesKg,
                        IdTipoFabricacion = detalle.IdTipoFabricacion,
                        NumeroPedido = detalle.NumeroPedido,
                        Longitud = detalle.Longitud,
                        Cantidad = detalle.Cantidad,
                        Produccion = (detalle.HilosTransversalesUn * detalle.Longitud) * FactorProd,
                        NumeroAlambreA = detalle.NumeroAlambreA,
                        PesoAlambreKgA = detalle.PesoAlambreKgA,
                        IdUsuarioCreacion = dto.IdUsuarioCreacion,
                        FechaCreacion = prdMallaPch.FechaCreacion
                    }).ToList();

                    totalProduccion += detPrdPchMaquinaAs.Sum(x => x.Produccion);
                    await _unitOfWork.DetPrdPchMaquinaARepository.BulkInsertAsync(detPrdPchMaquinaAs);
                }

                // Create DetPrdPchMaquinaB records
                if (dto.DetPrdPchMaquinaBs != null && dto.DetPrdPchMaquinaBs.Any())
                {
                    var detPrdPchMaquinaBs = dto.DetPrdPchMaquinaBs.Select(detalle => new DetPrdPchMaquinaB
                    {
                        PrdMallaPchId = idPrdMallaPch,
                        IdMaquina = detalle.IdMaquina,
                        HilosLongitudinalesUn = detalle.HilosLongitudinalesUn,
                        MermaHilosLongitudinalesKg = detalle.MermaHilosLongitudinalesKg,
                        IdTipoFabricacion = detalle.IdTipoFabricacion,
                        NumeroPedido = detalle.NumeroPedido,
                        Longitud = detalle.Longitud,
                        Cantidad = detalle.Cantidad,
                        Produccion = (detalle.HilosLongitudinalesUn * detalle.Longitud) * FactorProd,
                        NumeroAlambreB = detalle.NumeroAlambreB,
                        PesoAlambreKgB = detalle.PesoAlambreKgB,
                        IdUsuarioCreacion = dto.IdUsuarioCreacion,
                        FechaCreacion = prdMallaPch.FechaCreacion
                    }).ToList();

                    totalProduccion += detPrdPchMaquinaBs.Sum(x => x.Produccion);
                    await _unitOfWork.DetPrdPchMaquinaBRepository.BulkInsertAsync(detPrdPchMaquinaBs);
                }

                // Create DetPrdPchMaquinaC records
                if (dto.DetPrdPchMaquinaCs != null && dto.DetPrdPchMaquinaCs.Any())
                {
                    var detPrdPchMaquinaCs = dto.DetPrdPchMaquinaCs.Select(detalle => new DetPrdPchMaquinaC
                    {
                        PrdMallaPchId = idPrdMallaPch,
                        IdMaquina = detalle.IdMaquina,
                        IdTipoMalla = detalle.IdTipoMalla,
                        MermaMallasKg = detalle.MermaMallasKg,
                        IdTipoFabricacion = detalle.IdTipoFabricacion,
                        NumeroPedido = detalle.NumeroPedido,
                        Longitud = detalle.Longitud,
                        Cantidad = detalle.Cantidad,
                        Produccion = (detalle.Cantidad * detalle.Longitud) * FactorProd,
                        IdUsuarioCreacion = dto.IdUsuarioCreacion,
                        FechaCreacion = prdMallaPch.FechaCreacion
                    }).ToList();

                    totalProduccion += detPrdPchMaquinaCs.Sum(x => x.Produccion);
                    await _unitOfWork.DetPrdPchMaquinaCRepository.BulkInsertAsync(detPrdPchMaquinaCs);
                }

                // Update ProduccionDia with calculated total
                prdMallaPch.ProduccionDia = totalProduccion;
                _unitOfWork.PrdMallaPchRepository.Update(prdMallaPch);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ShowPrdMallaPchDTO>> GetAllAsync()
        {
            var productions = await _unitOfWork.PrdMallaPchRepository.GetAllAsync();

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
                return new ShowPrdMallaPchDTO
                {
                    Id = p.Id,
                    Operarios = string.Join(", ", userNames),
                    Fecha = p.Fecha,
                    AprobadoSupervisor = p.AprobadoSupervisor,
                    AprobadoGerencia = p.AprobadoGerencia,
                    IdAprobadoSupervisor = p.IdAprobadoSupervisor,
                    IdAprobadoGerencia = p.IdAprobadoGerencia
                };
            });

            return dtos.OrderByDescending(x => x.Id);
        }

        public async Task<PrdMallaPchDTO> GetByIdAsync(int id)
        {
            var lista = await _unitOfWork.PrdMallaPchRepository.GetAllAsync(
                filter: p => p.Id == id,
                include: q => q
                    .Include(p => p.DetPrdPchMaquinaAs)
                    .Include(p => p.DetPrdPchMaquinaBs)
                    .Include(p => p.DetPrdPchMaquinaCs)
            );

            var prd = lista.SingleOrDefault();
            if (prd == null) return null!;

            var userIds = prd.IdUsuarios.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            return new PrdMallaPchDTO
            {
                Id = prd.Id,
                IdUsuarios = userIds,
                Fecha = prd.Fecha,
                Observaciones = prd.Observaciones,
                ProduccionDia = prd.ProduccionDia,
                TiempoParo = prd.TiempoParo,
                IdUsuarioCreacion = prd.IdUsuarioCreacion,
                FechaCreacion = prd.FechaCreacion,
                IdUsuarioActualizacion = prd.IdUsuarioActualizacion,
                FechaActualizacion = prd.FechaActualizacion,
                AprobadoSupervisor = prd.AprobadoSupervisor,
                AprobadoGerencia = prd.AprobadoGerencia,
                IdAprobadoSupervisor = prd.IdAprobadoSupervisor,
                IdAprobadoGerencia = prd.IdAprobadoGerencia,
                DetPrdPchMaquinaAs = prd.DetPrdPchMaquinaAs?.Select(entity => new DetPrdPchMaquinaADTO
                {
                    Id = entity.Id,
                    PrdMallaPchId = entity.PrdMallaPchId,
                    IdMaquina = entity.IdMaquina,
                    HilosTransversalesUn = entity.HilosTransversalesUn,
                    MermaHilosTransversalesKg = entity.MermaHilosTransversalesKg,
                    IdTipoFabricacion = entity.IdTipoFabricacion,
                    NumeroPedido = entity.NumeroPedido,
                    Longitud = entity.Longitud,
                    Cantidad = entity.Cantidad,
                    Produccion = entity.Produccion,
                    NumeroAlambreA = entity.NumeroAlambreA,
                    PesoAlambreKgA = entity.PesoAlambreKgA,
                    IdUsuarioCreacion = entity.IdUsuarioCreacion,
                    FechaCreacion = entity.FechaCreacion,
                    IdUsuarioActualizacion = entity.IdUsuarioActualizacion,
                    FechaActualizacion = entity.FechaActualizacion
                }).ToList(),
                DetPrdPchMaquinaBs = prd.DetPrdPchMaquinaBs?.Select(entity => new DetPrdPchMaquinaBDTO
                {
                    Id = entity.Id,
                    PrdMallaPchId = entity.PrdMallaPchId,
                    IdMaquina = entity.IdMaquina,
                    HilosLongitudinalesUn = entity.HilosLongitudinalesUn,
                    MermaHilosLongitudinalesKg = entity.MermaHilosLongitudinalesKg,
                    IdTipoFabricacion = entity.IdTipoFabricacion,
                    NumeroPedido = entity.NumeroPedido,
                    Longitud = entity.Longitud,
                    Cantidad = entity.Cantidad,
                    Produccion = entity.Produccion,
                    NumeroAlambreB = entity.NumeroAlambreB,
                    PesoAlambreKgB = entity.PesoAlambreKgB,
                    IdUsuarioCreacion = entity.IdUsuarioCreacion,
                    FechaCreacion = entity.FechaCreacion,
                    IdUsuarioActualizacion = entity.IdUsuarioActualizacion,
                    FechaActualizacion = entity.FechaActualizacion
                }).ToList(),
                DetPrdPchMaquinaCs = prd.DetPrdPchMaquinaCs?.Select(entity => new DetPrdPchMaquinaCDTO
                {
                    Id = entity.Id,
                    PrdMallaPchId = entity.PrdMallaPchId,
                    IdMaquina = entity.IdMaquina,
                    IdTipoMalla = entity.IdTipoMalla,
                    MermaMallasKg = entity.MermaMallasKg,
                    IdTipoFabricacion = entity.IdTipoFabricacion,
                    NumeroPedido = entity.NumeroPedido,
                    Longitud = entity.Longitud,
                    Cantidad = entity.Cantidad,
                    Produccion = entity.Produccion,
                    IdUsuarioCreacion = entity.IdUsuarioCreacion,
                    FechaCreacion = entity.FechaCreacion,
                    IdUsuarioActualizacion = entity.IdUsuarioActualizacion,
                    FechaActualizacion = entity.FechaActualizacion
                }).ToList()
            };
        }

        public async Task UpdateAsync(PrdMallaPchDTO dto)
        {
            var prd = await _unitOfWork.PrdMallaPchRepository.GetByIdAsync(dto.Id);
            if (prd != null)
            {
                prd.IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>());
                prd.Fecha = dto.Fecha;
                prd.Observaciones = dto.Observaciones;
                prd.TiempoParo = dto.TiempoParo;
                prd.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                prd.FechaActualizacion = DateTime.Now;

                // Recalculate ProduccionDia from all details
                await RecalculateProduccionDiaAsync(prd);

                _unitOfWork.PrdMallaPchRepository.Update(prd);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetPrd(DetPrdPchMaquinaADTO dto)
        {
            var det = await _unitOfWork.DetPrdPchMaquinaARepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                det.IdMaquina = dto.IdMaquina;
                det.HilosTransversalesUn = dto.HilosTransversalesUn;
                det.MermaHilosTransversalesKg = dto.MermaHilosTransversalesKg;
                det.IdTipoFabricacion = dto.IdTipoFabricacion;
                det.NumeroPedido = dto.NumeroPedido;
                det.Longitud = dto.Longitud;
                det.Cantidad = dto.Cantidad;
                det.Produccion = (dto.HilosTransversalesUn * dto.Longitud) * FactorProd;
                det.NumeroAlambreA = dto.NumeroAlambreA;
                det.PesoAlambreKgA = dto.PesoAlambreKgA;
                det.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                det.FechaActualizacion = DateTime.Now;

                _unitOfWork.DetPrdPchMaquinaARepository.Update(det);

                // Update ProduccionDia in main entity
                var prd = await _unitOfWork.PrdMallaPchRepository.GetByIdAsync(det.PrdMallaPchId);
                if (prd != null)
                {
                    await RecalculateProduccionDiaAsync(prd);
                    _unitOfWork.PrdMallaPchRepository.Update(prd);
                }

                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetPrd(DetPrdPchMaquinaBDTO dto)
        {
            var det = await _unitOfWork.DetPrdPchMaquinaBRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                det.IdMaquina = dto.IdMaquina;
                det.HilosLongitudinalesUn = dto.HilosLongitudinalesUn;
                det.MermaHilosLongitudinalesKg = dto.MermaHilosLongitudinalesKg;
                det.IdTipoFabricacion = dto.IdTipoFabricacion;
                det.NumeroPedido = dto.NumeroPedido;
                det.Longitud = dto.Longitud;
                det.Cantidad = dto.Cantidad;
                det.Produccion = (dto.HilosLongitudinalesUn * dto.Longitud) * FactorProd;
                det.NumeroAlambreB = dto.NumeroAlambreB;
                det.PesoAlambreKgB = dto.PesoAlambreKgB;
                det.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                det.FechaActualizacion = DateTime.Now;

                _unitOfWork.DetPrdPchMaquinaBRepository.Update(det);

                // Update ProduccionDia in main entity
                var prd = await _unitOfWork.PrdMallaPchRepository.GetByIdAsync(det.PrdMallaPchId);
                if (prd != null)
                {
                    await RecalculateProduccionDiaAsync(prd);
                    _unitOfWork.PrdMallaPchRepository.Update(prd);
                }

                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetPrd(DetPrdPchMaquinaCDTO dto)
        {
            var det = await _unitOfWork.DetPrdPchMaquinaCRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                det.IdMaquina = dto.IdMaquina;
                det.IdTipoMalla = dto.IdTipoMalla;
                det.MermaMallasKg = dto.MermaMallasKg;
                det.IdTipoFabricacion = dto.IdTipoFabricacion;
                det.NumeroPedido = dto.NumeroPedido;
                det.Longitud = dto.Longitud;
                det.Cantidad = dto.Cantidad;
                det.Produccion = (dto.Cantidad * dto.Longitud) * FactorProd;
                det.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                det.FechaActualizacion = DateTime.Now;

                _unitOfWork.DetPrdPchMaquinaCRepository.Update(det);

                // Update ProduccionDia in main entity
                var prd = await _unitOfWork.PrdMallaPchRepository.GetByIdAsync(det.PrdMallaPchId);
                if (prd != null)
                {
                    await RecalculateProduccionDiaAsync(prd);
                    _unitOfWork.PrdMallaPchRepository.Update(prd);
                }

                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<CrearPrdMallaPchDTO> GetCreateData()
        {
            var catMaquina = await _unitOfWork.CatMaquinaRepository.FindAsync(x => x.Activo == true);
            var catTipoFabricacion = await _unitOfWork.TipoFabricacionRepository.FindAsync(x => x.Activo == true);
            var catTipoMalla = await _unitOfWork.CatTipoMallaRepository.FindAsync(x => x.Activo == true);

            var dto = new CrearPrdMallaPchDTO
            {
                CatMaquina = catMaquina.Select(m => new MaquinaDto
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    Activo = m.Activo
                }).ToList(),
                CatTipoFabricacion = catTipoFabricacion.Select(t => new TipoFabricacionDto
                {
                    Id = t.Id,
                    Descripcion = t.Descripcion,
                    Activo = t.Activo
                }).ToList(),
                CatTipoMalla = catTipoMalla.ToList()
            };

            return dto;
        }

        public async Task<bool> ValidatePrdMallaPchByIdAsync(int id, string userId)
        {
            var prdMallaPch = await _unitOfWork.PrdMallaPchRepository.GetByIdAsync(id);
            if (prdMallaPch == null)
            {
                return false;
            }

            if (prdMallaPch.AprobadoSupervisor)
            {
                return false;
            }

            prdMallaPch.AprobadoSupervisor = true;
            prdMallaPch.IdAprobadoSupervisor = userId;
            prdMallaPch.IdUsuarioActualizacion = userId;
            prdMallaPch.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdMallaPchRepository.Update(prdMallaPch);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AprovePrdMallaPchByIdAsync(int id, string userId)
        {
            var prdMallaPch = await _unitOfWork.PrdMallaPchRepository.GetByIdAsync(id);
            if (prdMallaPch == null)
            {
                return false;
            }

            if (prdMallaPch.AprobadoGerencia)
            {
                return false;
            }

            prdMallaPch.AprobadoGerencia = true;
            prdMallaPch.IdAprobadoGerencia = userId;
            prdMallaPch.IdUsuarioActualizacion = userId;
            prdMallaPch.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdMallaPchRepository.Update(prdMallaPch);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        // Helper method to recalculate ProduccionDia from all detail entities
        private async Task RecalculateProduccionDiaAsync(PrdMallaPch prdMallaPch)
        {
            var detA = await _unitOfWork.DetPrdPchMaquinaARepository.FindAsync(x => x.PrdMallaPchId == prdMallaPch.Id);
            var detB = await _unitOfWork.DetPrdPchMaquinaBRepository.FindAsync(x => x.PrdMallaPchId == prdMallaPch.Id);
            var detC = await _unitOfWork.DetPrdPchMaquinaCRepository.FindAsync(x => x.PrdMallaPchId == prdMallaPch.Id);

            var totalProduccion = detA.Sum(x => x.Produccion) + 
                                 detB.Sum(x => x.Produccion) + 
                                 detC.Sum(x => x.Produccion);

            prdMallaPch.ProduccionDia = totalProduccion;
            prdMallaPch.IdUsuarioActualizacion = prdMallaPch.IdUsuarioActualizacion;
            prdMallaPch.FechaActualizacion = DateTime.Now;
        }

        // Additional methods following the pattern from other services
        public async Task DeleteDetPrd(DetPrdPchMaquinaADTO dto)
        {
            var det = await _unitOfWork.DetPrdPchMaquinaARepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                var prdMallaPchId = det.PrdMallaPchId;
                _unitOfWork.DetPrdPchMaquinaARepository.Remove(det);

                // Update ProduccionDia in main entity after deletion
                var prd = await _unitOfWork.PrdMallaPchRepository.GetByIdAsync(prdMallaPchId);
                if (prd != null)
                {
                    await RecalculateProduccionDiaAsync(prd);
                    _unitOfWork.PrdMallaPchRepository.Update(prd);
                }

                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteDetPrd(DetPrdPchMaquinaBDTO dto)
        {
            var det = await _unitOfWork.DetPrdPchMaquinaBRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                var prdMallaPchId = det.PrdMallaPchId;
                _unitOfWork.DetPrdPchMaquinaBRepository.Remove(det);

                // Update ProduccionDia in main entity after deletion
                var prd = await _unitOfWork.PrdMallaPchRepository.GetByIdAsync(prdMallaPchId);
                if (prd != null)
                {
                    await RecalculateProduccionDiaAsync(prd);
                    _unitOfWork.PrdMallaPchRepository.Update(prd);
                }

                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteDetPrd(DetPrdPchMaquinaCDTO dto)
        {
            var det = await _unitOfWork.DetPrdPchMaquinaCRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                var prdMallaPchId = det.PrdMallaPchId;
                _unitOfWork.DetPrdPchMaquinaCRepository.Remove(det);

                // Update ProduccionDia in main entity after deletion
                var prd = await _unitOfWork.PrdMallaPchRepository.GetByIdAsync(prdMallaPchId);
                if (prd != null)
                {
                    await RecalculateProduccionDiaAsync(prd);
                    _unitOfWork.PrdMallaPchRepository.Update(prd);
                }

                await _unitOfWork.SaveChangesAsync();
            }
        }

     
        public async Task<IEnumerable<PrdMallaPCHReporteDTO>> GetAllPrdMallaPCHWithDetailsAsync(DateTime start, DateTime end)
        {
            return await _unitOfWork.ReportesDapperRepository.GetAllPrdMallaPCHWithDetailsAsync(start, end);
        }


        }
}
