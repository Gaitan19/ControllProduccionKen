using Application.DTOs;
using Application.Interfaces;
using Application.Utiles;
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
    public class PrdAccesorioService : IPrdAccesorioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public PrdAccesorioService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userManager = userManager;
        }

        public async Task CreateAsync(PrdAccesorioDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var prdAccesorio = new PrdAccesorio
                {
                    IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>()),
                    IdMaquina = dto.IdMaquina,
                    Fecha = dto.Fecha,
                    Observaciones = dto.Observaciones,
                    MermaMallaCovintecKg = dto.MermaMallaCovintecKg,
                    MermaMallaPchKg = dto.MermaMallaPchKg,
                    MermaBobinasKg = dto.MermaBobinasKg,
                    MermaLitewallKg = dto.MermaLitewallKg,
                    TiempoParo = dto.TiempoParo,
                    IdUsuarioCreacion = dto.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    AprobadoSupervisor = false,
                    AprobadoGerencia = false
                };

                await _unitOfWork.PrdAccesorioRepository.AddAsync(prdAccesorio);
                await _unitOfWork.SaveChangesAsync();
                var idPrd = prdAccesorio.Id;

                if (dto.DetPrdAccesorios != null && dto.DetPrdAccesorios.Any())
                {
                    var detalles = dto.DetPrdAccesorios.Select(d => new DetPrdAccesorio
                    {
                        PrdAccesoriosId = idPrd,
                        IdTipoArticulo = d.IdTipoArticulo,
                        IdArticulo = d.IdArticulo,
                        IdTipoFabricacion = d.IdTipoFabricacion,
                        NumeroPedido = d.NumeroPedido,
                        IdMallaCovintec = d.IdMallaCovintec,
                        CantidadMallaUn = d.CantidadMallaUn,
                        IdTipoMallaPch = d.IdTipoMallaPch,
                        CantidadPchKg = d.CantidadPchKg,
                        IdAnchoBobina = d.IdAnchoBobina,
                        CantidadBobinaKg = d.CantidadBobinaKg,
                        IdCalibre = d.IdCalibre,
                        CantidadCalibreKg = d.CantidadCalibreKg,
                        IdUsuarioCreacion = dto.IdUsuarioCreacion,
                        FechaCreacion = prdAccesorio.FechaCreacion
                    }).ToList();

                    await _unitOfWork.DetPrdAccesorioRepository.BulkInsertAsync(detalles);
                }

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ShowPrdAccesorioDto>> GetAllAsync()
        {
            var maquinas = await _unitOfWork.CatMaquinaRepository.GetAllAsync();
            var productions = await _unitOfWork.PrdAccesorioRepository.GetAllAsync();

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
                return new ShowPrdAccesorioDto
                {
                    Id = p.Id,
                    Operarios = string.Join(", ", userNames),
                    Maquina = maquinas.FirstOrDefault(m => m.Id == p.IdMaquina)?.Nombre ?? "Desconocida",
                    Fecha = p.Fecha,
                    AprobadoSupervisor = p.AprobadoSupervisor,
                    AprobadoGerencia = p.AprobadoGerencia,
                    IdAprobadoSupervisor = p.IdAprobadoSupervisor,
                    IdAprobadoGerencia = p.IdAprobadoGerencia
                };
            }).ToList();

            return dtos;
        }

        public async Task<PrdAccesorioDto> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.PrdAccesorioRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }

            var detalles = await _unitOfWork.DetPrdAccesorioRepository.FindAsync(d => d.PrdAccesoriosId == id);

            var dto = new PrdAccesorioDto
            {
                Id = entity.Id,
                IdUsuarios = entity.IdUsuarios.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                IdMaquina = entity.IdMaquina,
                Fecha = entity.Fecha,
                Observaciones = entity.Observaciones,
                MermaMallaCovintecKg = entity.MermaMallaCovintecKg,
                MermaMallaPchKg = entity.MermaMallaPchKg,
                MermaBobinasKg = entity.MermaBobinasKg,
                MermaLitewallKg = entity.MermaLitewallKg,
                TiempoParo = entity.TiempoParo,
                NotaSupervisor = entity.NotaSupervisor,
                AprobadoSupervisor = entity.AprobadoSupervisor,
                AprobadoGerencia = entity.AprobadoGerencia,
                IdAprobadoSupervisor = entity.IdAprobadoSupervisor,
                IdAprobadoGerencia = entity.IdAprobadoGerencia,
                IdUsuarioCreacion = entity.IdUsuarioCreacion,
                FechaCreacion = entity.FechaCreacion,
                IdUsuarioActualizacion = entity.IdUsuarioActualizacion,
                FechaActualizacion = entity.FechaActualizacion,
                DetPrdAccesorios = detalles.Select(d => new DetPrdAccesorioDto
                {
                    Id = d.Id,
                    PrdAccesoriosId = d.PrdAccesoriosId,
                    IdTipoArticulo = d.IdTipoArticulo,
                    IdArticulo = d.IdArticulo,
                    IdTipoFabricacion = d.IdTipoFabricacion,
                    NumeroPedido = d.NumeroPedido,
                    IdMallaCovintec = d.IdMallaCovintec,
                    CantidadMallaUn = d.CantidadMallaUn,
                    IdTipoMallaPch = d.IdTipoMallaPch,
                    CantidadPchKg = d.CantidadPchKg,
                    IdAnchoBobina = d.IdAnchoBobina,
                    CantidadBobinaKg = d.CantidadBobinaKg,
                    IdCalibre = d.IdCalibre,
                    CantidadCalibreKg = d.CantidadCalibreKg,
                    IdUsuarioCreacion = d.IdUsuarioCreacion,
                    FechaCreacion = d.FechaCreacion,
                    IdUsuarioActualizacion = d.IdUsuarioActualizacion,
                    FechaActualizacion = d.FechaActualizacion
                }).ToList()
            };

            return dto;
        }

        public async Task<CrearPrdAccesorioDto> GetCreateDataAsync()
        {
            var catMaquina = await _unitOfWork.CatMaquinaRepository.FindAsync(x => x.Activo == true && x.Id==CatalogoIds.IdMaquinaPrdAccesorios);
            var catTipoFabricacion = await _unitOfWork.TipoFabricacionRepository.FindAsync(x => x.Activo == true);
           
            var catalogoTipoArticulo = await _unitOfWork.MaestroCatalogoRepository.FindAsync(x => x.Activo == true && x.IdPadre == CatalogoIds.TipoAccesorioArticulo); 
            var catalogoMallasCovintec = await _unitOfWork.CatalogoMallasCovintecRepository.FindAsync(x => x.Activo == true);
            var catalogoTipoMallaPch = await _unitOfWork.CatTipoMallaRepository.FindAsync(x => x.Activo == true);
            var catalogoAnchoBobina = await _unitOfWork.AnchoBobinaRepository.FindAsync(x => x.Activo == true);
            var catalogoCalibre = await _unitOfWork.MaestroCatalogoRepository.FindAsync(x => x.Activo == true && x.IdPadre == CatalogoIds.TipoCalibre); 

            var dto = new CrearPrdAccesorioDto
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
                CatalogoAccesorios = new List<CatalogoAccesorio>(),
                CatalogoTipoArticulo = catalogoTipoArticulo.ToList(),
                CatalogoMallasCovintec = catalogoMallasCovintec.ToList(),
                CatalogoTipoMallaPch = catalogoTipoMallaPch.ToList(),
                CatalogoAnchoBobina = catalogoAnchoBobina.ToList(),
                CatalogoCalibre = catalogoCalibre.ToList()
            };

            return dto;
        }

        public async Task UpdateAsync(PrdAccesorioDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entity = await _unitOfWork.PrdAccesorioRepository.GetByIdAsync(dto.Id);
                if (entity == null)
                {
                    throw new ArgumentException("Entidad no encontrada");
                }

                // No permitir editar si ya fue aprobada
                if (entity.AprobadoGerencia)
                {
                    throw new InvalidOperationException("No se puede editar una producción ya aprobada");
                }

                entity.IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>());
                entity.IdMaquina = dto.IdMaquina;
                entity.Fecha = dto.Fecha;
                entity.Observaciones = dto.Observaciones;
                entity.MermaMallaCovintecKg = dto.MermaMallaCovintecKg;
                entity.MermaMallaPchKg = dto.MermaMallaPchKg;
                entity.MermaBobinasKg = dto.MermaBobinasKg;
                entity.MermaLitewallKg = dto.MermaLitewallKg;
                entity.TiempoParo = dto.TiempoParo;
                entity.NotaSupervisor = dto.NotaSupervisor;
                entity.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                entity.FechaActualizacion = DateTime.Now;

                _unitOfWork.PrdAccesorioRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateDetPrdAsync(DetPrdAccesorioDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var parentEntity = await _unitOfWork.PrdAccesorioRepository.GetByIdAsync(dto.PrdAccesoriosId);
                if (parentEntity == null)
                {
                    throw new ArgumentException("Entidad padre no encontrada");
                }

                // No permitir editar detalles si ya fue aprobada
                if (parentEntity.AprobadoGerencia)
                {
                    throw new InvalidOperationException("No se puede editar detalles de una producción ya aprobada");
                }

                // Validación condicional: si TipoFabricacion = "Por Pedido", NumeroPedido requerido
                var tipoFabricacion = await _unitOfWork.TipoFabricacionRepository.GetByIdAsync(dto.IdTipoFabricacion);
                if (tipoFabricacion != null && tipoFabricacion.Descripcion == "Por Pedido" && !dto.NumeroPedido.HasValue)
                {
                    throw new InvalidOperationException("El número de pedido es requerido cuando el tipo de fabricación es 'Por Pedido'");
                }

                var entity = await _unitOfWork.DetPrdAccesorioRepository.GetByIdAsync(dto.Id);
                if (entity == null)
                {
                    throw new ArgumentException("Detalle no encontrado");
                }

                entity.IdTipoArticulo = dto.IdTipoArticulo;
                entity.IdArticulo = dto.IdArticulo;
                entity.IdTipoFabricacion = dto.IdTipoFabricacion;
                entity.NumeroPedido = dto.NumeroPedido;
                entity.IdMallaCovintec = dto.IdMallaCovintec;
                entity.CantidadMallaUn = dto.CantidadMallaUn;
                entity.IdTipoMallaPch = dto.IdTipoMallaPch;
                entity.CantidadPchKg = dto.CantidadPchKg;
                entity.IdAnchoBobina = dto.IdAnchoBobina;
                entity.CantidadBobinaKg = dto.CantidadBobinaKg;
                entity.IdCalibre = dto.IdCalibre;
                entity.CantidadCalibreKg = dto.CantidadCalibreKg;
                entity.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                entity.FechaActualizacion = DateTime.Now;

                _unitOfWork.DetPrdAccesorioRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ValidatePrdAccesorioByIdAsync(int id, string userId)
        {
            var prd = await _unitOfWork.PrdAccesorioRepository.GetByIdAsync(id);
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

            _unitOfWork.PrdAccesorioRepository.Update(prd);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AprovePrdAccesorioByIdAsync(int id, string userId)
        {
            var prd = await _unitOfWork.PrdAccesorioRepository.GetByIdAsync(id);
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

            _unitOfWork.PrdAccesorioRepository.Update(prd);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId)
        {
            var prd = await _unitOfWork.PrdAccesorioRepository.GetByIdAsync(id);
            if (prd == null)
            {
                return false;
            }

            prd.NotaSupervisor = notaSupervisor;
            prd.IdUsuarioActualizacion = userId;
            prd.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdAccesorioRepository.Update(prd);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public  Task<IEnumerable<PrdAccesorioReporteDTO>> GetAllWithDetailsAsync(DateTime start, DateTime end)
        {
            return _unitOfWork.ReportesDapperRepository.GetAllPrdAccesorioWithDetailsAsync(start, end);
        }

        public async Task<IEnumerable<CatalogoAccesorio>> GetCatalogoAccesoriosByTipoAsync(int idTipo)
        {
            return await _unitOfWork.CatalogoAccesorioRepository
                                    .FindAsync(c => c.Activo == true && c.IdTipo == idTipo);
        }
    }
}