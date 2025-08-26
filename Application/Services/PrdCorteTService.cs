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
    public class PrdCorteTService : IPrdCorteTService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public PrdCorteTService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userManager = userManager;
        }

        public async Task CreateAsync(PrdCorteTDTO dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var prdCorteT = new PrdCorteT
                {
                    TiempoParo = dto.TiempoParo,
                    IdTipoReporte = dto.IdTipoReporte,
                    IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>()),
                    IdMaquina = dto.IdMaquina,
                    Fecha = dto.Fecha,
                    Observaciones = dto.Observaciones,
                    IdUsuarioCreacion = dto.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    AprobadoSupervisor = false,
                    AprobadoGerencia = false
                };

                await _unitOfWork.PrdCorteTRepository.AddAsync(prdCorteT);
                await _unitOfWork.SaveChangesAsync();
                var idPrd = prdCorteT.Id;

                if (dto.DetPrdCorteTs != null && dto.DetPrdCorteTs.Any())
                {
                    var detalles = dto.DetPrdCorteTs.Select(d => new DetPrdCorteT
                    {
                        PrdCorteTid = idPrd,
                        No = d.No,
                        IdArticulo = d.IdArticulo,
                        IdTipoFabricacion = d.IdTipoFabricacion,
                        NumeroPedido = d.NumeroPedido,
                        PrdCodigoBloque = d.PrdCodigoBloque,
                        IdDensidad = d.IdDensidad,
                        IdTipoBloque = d.IdTipoBloque,
                        CantidadPiezasConformes = d.CantidadPiezasConformes,
                        CantidadPiezasNoConformes = d.CantidadPiezasNoConformes,
                        Nota = d.Nota,
                        IdUsuarioCreacion = dto.IdUsuarioCreacion,
                        FechaCreacion = prdCorteT.FechaCreacion
                    }).ToList();

                    await _unitOfWork.DetPrdCorteTRepository.BulkInsertAsync(detalles);
                }

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ShowPrdCorteTDto>> GetAllAsync()
        {
            var maquinas = await _unitOfWork.CatMaquinaRepository.GetAllAsync();
            var productions = await _unitOfWork.PrdCorteTRepository.GetAllAsync();

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
                return new ShowPrdCorteTDto
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

        public async Task<PrdCorteTDTO> GetByIdAsync(int id)
        {
            var prd = await _unitOfWork.PrdCorteTRepository
                .GetByIdIncludeAsync(x => x.Id == id, x => x.DetPrdCorteTs);
            if (prd == null) return null!;

            var userIds = prd.IdUsuarios.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            return new PrdCorteTDTO
            {
                Id = prd.Id,
                TiempoParo = prd.TiempoParo,
                IdTipoReporte = prd.IdTipoReporte,
                IdUsuarios = userIds,
                IdMaquina = prd.IdMaquina,
                Fecha = prd.Fecha,
                Observaciones = prd.Observaciones,
                IdUsuarioCreacion = prd.IdUsuarioCreacion,
                FechaCreacion = prd.FechaCreacion,
                IdUsuarioActualizacion = prd.IdUsuarioActualizacion,
                FechaActualizacion = prd.FechaActualizacion,
                AprobadoSupervisor = prd.AprobadoSupervisor,
                AprobadoGerencia = prd.AprobadoGerencia,
                IdAprobadoSupervisor = prd.IdAprobadoSupervisor,
                IdAprobadoGerencia = prd.IdAprobadoGerencia,
                NotaSupervisor = prd.NotaSupervisor,
                DetPrdCorteTs = prd.DetPrdCorteTs?.Select(d => new DetPrdCorteTDTO
                {
                    Id = d.Id,
                    PrdCorteTid = d.PrdCorteTid,
                    No = d.No,
                    IdArticulo = d.IdArticulo,
                    IdTipoFabricacion = d.IdTipoFabricacion,
                    NumeroPedido = d.NumeroPedido,
                    PrdCodigoBloque = d.PrdCodigoBloque,
                    IdDensidad = d.IdDensidad,
                    IdTipoBloque = d.IdTipoBloque,
                    CantidadPiezasConformes = d.CantidadPiezasConformes,
                    CantidadPiezasNoConformes = d.CantidadPiezasNoConformes,
                    Nota = d.Nota,
                    IdUsuarioCreacion = d.IdUsuarioCreacion,
                    FechaCreacion = d.FechaCreacion,
                    IdUsuarioActualizacion = d.IdUsuarioActualizacion,
                    FechaActualizacion = d.FechaActualizacion
                }).ToList()
            };
        }

        public async Task UpdateAsync(PrdCorteTDTO dto)
        {
            var prd = await _unitOfWork.PrdCorteTRepository.GetByIdAsync(dto.Id);
            if (prd != null)
            {
                prd.TiempoParo = dto.TiempoParo;
                prd.IdTipoReporte = dto.IdTipoReporte;
                prd.IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>());
                prd.IdMaquina = dto.IdMaquina;
                prd.Fecha = dto.Fecha;
                prd.Observaciones = dto.Observaciones;
                prd.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                prd.FechaActualizacion = DateTime.Now;
                _unitOfWork.PrdCorteTRepository.Update(prd);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetPrd(DetPrdCorteTDTO dto)
        {
            var det = await _unitOfWork.DetPrdCorteTRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                det.No = dto.No;
                det.IdArticulo = dto.IdArticulo;
                det.IdTipoFabricacion = dto.IdTipoFabricacion;
                det.NumeroPedido = dto.NumeroPedido;
                det.PrdCodigoBloque = dto.PrdCodigoBloque;
                det.IdDensidad = dto.IdDensidad;
                det.IdTipoBloque = dto.IdTipoBloque;
                det.CantidadPiezasConformes = dto.CantidadPiezasConformes;
                det.CantidadPiezasNoConformes = dto.CantidadPiezasNoConformes;
                det.Nota = dto.Nota;
                det.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                det.FechaActualizacion = DateTime.Now;
                _unitOfWork.DetPrdCorteTRepository.Update(det);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteDetPrd(DetPrdCorteTDTO dto)
        {
            var det = await _unitOfWork.DetPrdCorteTRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                _unitOfWork.DetPrdCorteTRepository.Remove(det);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<CrearPrdCorteTDTO> GetCreateData()
        {
            var catMaquina = await _unitOfWork.CatMaquinaRepository.FindAsync(x => x.Activo == true);
            var catTipoFabricacion = await _unitOfWork.TipoFabricacionRepository.FindAsync(x => x.Activo == true);
            var subDetPrdBloqueCodigo = await _unitOfWork.SubDetPrdBloqueRepository.GetAllAsync();
            var catalogoDensidad = await _unitOfWork.MaestroCatalogoRepository.FindAsync(x => x.Activo == true && x.IdPadre == CatalogoIds.Densidad);
            var catalogoTipoBloque = await _unitOfWork.MaestroCatalogoRepository.FindAsync(x => x.Activo == true && x.IdPadre == CatalogoIds.TipoBloque);
            var catLamina = await _unitOfWork.CatLaminaRepository.FindAsync(x => x.Activo == true);

            var dto = new CrearPrdCorteTDTO
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
                SubDetPrdBloqueCodigo =subDetPrdBloqueCodigo.Select(x=>x.CodigoBloque).ToList(),
                CatalogoDensidad = catalogoDensidad.ToList(),
                CatalogoTipoBloque = catalogoTipoBloque.ToList(),
                CatLamina = catLamina.ToList()
            };

            return dto;
        }

        public async Task<bool> ValidatePrdCorteTByIdAsync(int id, string userId)
        {
            var prd = await _unitOfWork.PrdCorteTRepository.GetByIdAsync(id);
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

            _unitOfWork.PrdCorteTRepository.Update(prd);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AprovePrdCorteTByIdAsync(int id, string userId)
        {
            var prd = await _unitOfWork.PrdCorteTRepository.GetByIdAsync(id);
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

            _unitOfWork.PrdCorteTRepository.Update(prd);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<PrdCorteTReporteDTO>> GetAllPrdCorteTWithDetailsAsync(DateTime start, DateTime end)
        {
           return _unitOfWork.ReportesDapperRepository.GetAllPrdCorteTWithDetailsAsync(start, end);
        }

        public async Task<bool> UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId)
        {
            var prd = await _unitOfWork.PrdCorteTRepository.GetByIdAsync(id);
            if (prd == null)
            {
                return false;
            }

            prd.NotaSupervisor = notaSupervisor;
            prd.IdUsuarioActualizacion = userId;
            prd.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdCorteTRepository.Update(prd);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
