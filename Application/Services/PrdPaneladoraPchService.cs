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
    public class PrdPaneladoraPchService : IPrdPaneladoraPchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        
        // Fixing the problematic line by providing a valid identifier and value for the constant field.
        private const decimal ProdConst = 1.2M;
        public PrdPaneladoraPchService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userManager = userManager;
        }

        public async Task CreateAsync(PrdPaneladoraPchDTO dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var prdPaneladoraPch = new PrdPaneladoraPch
                {
                    IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>()),
                    IdMaquina = dto.IdMaquina,
                    Fecha = dto.Fecha,
                    Observaciones = dto.Observaciones,
                    ProduccionDia = 0, // Will be calculated from details
                    TiempoParo = dto.TiempoParo,
                    IdUsuarioCreacion = dto.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    AprobadoSupervisor = false,
                    AprobadoGerencia = false
                };

                await _unitOfWork.PrdPaneladoraPchRepository.AddAsync(prdPaneladoraPch);
                await _unitOfWork.SaveChangesAsync();
                var idPrd = prdPaneladoraPch.Id;

                decimal totalProduccion = 0;

                if (dto.DetPrdPaneladoraPches != null && dto.DetPrdPaneladoraPches.Any())
                {
                    var detalles = dto.DetPrdPaneladoraPches.Select(d => new DetPrdPaneladoraPch
                    {
                        PrdPaneladoraPchId = idPrd,
                        IdArticulo = d.IdArticulo,
                        Longitud = d.Longitud,
                        CantidadProducida = d.CantidadProducida,
                        CantidadNoConforme = d.CantidadNoConforme,
                        Mts2PorPanel = ProdConst *(d.Longitud*d.CantidadProducida),
                        IdTipoFabricacion = d.IdTipoFabricacion,
                        NumeroPedido = d.NumeroPedido,
                        NumeroAlambre = d.NumeroAlambre,
                        PesoAlambreKg = d.PesoAlambreKg,
                        MermaAlambreKg = d.MermaAlambreKg,
                        IdUsuarioCreacion = dto.IdUsuarioCreacion,
                        FechaCreacion = prdPaneladoraPch.FechaCreacion
                    }).ToList();

                    // Calculate total production (Mts2PorPanel)
                    totalProduccion = detalles.Sum(x => x.Mts2PorPanel);

                    await _unitOfWork.DetPrdPaneladoraPchRepository.BulkInsertAsync(detalles);
                }

                // Update the production total
                prdPaneladoraPch.ProduccionDia = totalProduccion;
                _unitOfWork.PrdPaneladoraPchRepository.Update(prdPaneladoraPch);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ShowPrdPaneladoraPchDTO>> GetAllAsync()
        {
            var maquinas = await _unitOfWork.CatMaquinaRepository.GetAllAsync();
            var productions = await _unitOfWork.PrdPaneladoraPchRepository.GetAllAsync();

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
                return new ShowPrdPaneladoraPchDTO
                {
                    Id = p.Id,
                    Operarios = string.Join(", ", userNames),
                    Maquina = maquinas.FirstOrDefault(m => m.Id == p.IdMaquina)?.Nombre ?? "N/A",
                    Fecha = p.Fecha,
                    Observaciones = p.Observaciones,
                    ProduccionDia = p.ProduccionDia,
                    TiempoParo = p.TiempoParo,
                    AprobadoSupervisor = p.AprobadoSupervisor,
                    AprobadoGerencia = p.AprobadoGerencia,
                    IdAprobadoSupervisor = p.IdAprobadoSupervisor,
                    IdAprobadoGerencia = p.IdAprobadoGerencia,
                    FechaCreacion = p.FechaCreacion,
                    IdUsuarioCreacion = p.IdUsuarioCreacion
                };
            }).OrderByDescending(x => x.FechaCreacion);

            return dtos;
        }

        public async Task<PrdPaneladoraPchDTO> GetByIdAsync(int id)
        {
            var production = await _unitOfWork.PrdPaneladoraPchRepository
                .GetByIdIncludeAsync(x => x.Id == id, p => p.DetPrdPaneladoraPches);

            if (production == null) return null;

            var userIds = production.IdUsuarios.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

            return new PrdPaneladoraPchDTO
            {
                Id = production.Id,
                IdUsuarios = userIds,
                IdMaquina = production.IdMaquina,
                Fecha = production.Fecha,
                Observaciones = production.Observaciones,
                ProduccionDia = production.ProduccionDia,
                TiempoParo = production.TiempoParo,
                NotaSupervisor = production.NotaSupervisor,
                IdUsuarioCreacion = production.IdUsuarioCreacion,
                FechaCreacion = production.FechaCreacion,
                IdUsuarioActualizacion = production.IdUsuarioActualizacion,
                FechaActualizacion = production.FechaActualizacion,
                AprobadoSupervisor = production.AprobadoSupervisor,
                AprobadoGerencia = production.AprobadoGerencia,
                IdAprobadoSupervisor = production.IdAprobadoSupervisor,
                IdAprobadoGerencia = production.IdAprobadoGerencia,
                DetPrdPaneladoraPches = production.DetPrdPaneladoraPches?.Select(d => new DetPrdPaneladoraPchDTO
                {
                    Id = d.Id,
                    PrdPaneladoraPchId = d.PrdPaneladoraPchId,
                    IdArticulo = d.IdArticulo,
                    Longitud = d.Longitud,
                    CantidadProducida = d.CantidadProducida,
                    CantidadNoConforme = d.CantidadNoConforme,
                    Mts2PorPanel = d.Mts2PorPanel,
                    IdTipoFabricacion = d.IdTipoFabricacion,
                    NumeroPedido = d.NumeroPedido,
                    NumeroAlambre = d.NumeroAlambre,
                    PesoAlambreKg = d.PesoAlambreKg,
                    MermaAlambreKg = d.MermaAlambreKg,
                    IdUsuarioCreacion = d.IdUsuarioCreacion,
                    FechaCreacion = d.FechaCreacion,
                    IdUsuarioActualizacion = d.IdUsuarioActualizacion,
                    FechaActualizacion = d.FechaActualizacion
                }).ToList()
            };
        }

        public async Task UpdateAsync(PrdPaneladoraPchDTO dto)
        {
            var existing = await _unitOfWork.PrdPaneladoraPchRepository.GetByIdAsync(dto.Id);
            if (existing == null) throw new InvalidOperationException("Production record not found");

            existing.IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>());
            existing.IdMaquina = dto.IdMaquina;
            existing.Fecha = dto.Fecha;
            existing.Observaciones = dto.Observaciones;
            existing.TiempoParo = dto.TiempoParo;
            existing.NotaSupervisor = dto.NotaSupervisor;
            existing.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
            existing.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdPaneladoraPchRepository.Update(existing);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId)
        {
            var entity = await _unitOfWork.PrdPaneladoraPchRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new ArgumentException("Entidad no encontrada");
            }

            entity.NotaSupervisor = notaSupervisor;
            entity.IdUsuarioActualizacion = userId;
            entity.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdPaneladoraPchRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDetPrd(DetPrdPaneladoraPchDTO dto)
        {
            var existing = await _unitOfWork.DetPrdPaneladoraPchRepository.GetByIdAsync(dto.Id);
            if (existing == null) throw new InvalidOperationException("Detail record not found");

            existing.IdArticulo = dto.IdArticulo;
            existing.Longitud = dto.Longitud;
            existing.CantidadProducida = dto.CantidadProducida;
            existing.CantidadNoConforme = dto.CantidadNoConforme;
            existing.Mts2PorPanel = ProdConst * (dto.Longitud * dto.CantidadProducida);
            existing.IdTipoFabricacion = dto.IdTipoFabricacion;
            existing.NumeroPedido = dto.NumeroPedido;
            existing.NumeroAlambre = dto.NumeroAlambre;
            existing.PesoAlambreKg = dto.PesoAlambreKg;
            existing.MermaAlambreKg = dto.MermaAlambreKg;
            existing.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
            existing.FechaActualizacion = DateTime.Now;

            _unitOfWork.DetPrdPaneladoraPchRepository.Update(existing);

            // Recalculate total production for parent
            await RecalculateProduccionDia(existing.PrdPaneladoraPchId);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteDetPrd(DetPrdPaneladoraPchDTO dto)
        {
            var existing = await _unitOfWork.DetPrdPaneladoraPchRepository.GetByIdAsync(dto.Id);
            if (existing == null) throw new InvalidOperationException("Detail record not found");

            var parentId = existing.PrdPaneladoraPchId;
            _unitOfWork.DetPrdPaneladoraPchRepository.Remove(existing);

            // Recalculate total production for parent
            await RecalculateProduccionDia(parentId);

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task RecalculateProduccionDia(int prdPaneladoraPchId)
        {
            var parent = await _unitOfWork.PrdPaneladoraPchRepository.GetByIdAsync(prdPaneladoraPchId);
            if (parent == null) return;

            var details = await _unitOfWork.DetPrdPaneladoraPchRepository
                .FindAsync(d => d.PrdPaneladoraPchId == prdPaneladoraPchId);

            parent.ProduccionDia = details.Sum(d => d.Mts2PorPanel);
            _unitOfWork.PrdPaneladoraPchRepository.Update(parent);
        }

        public async Task<CrearPrdPaneladoraPchDTO> GetCreateData()
        {
            var maquinas = await _unitOfWork.CatMaquinaRepository.GetAllAsync();
            var tiposFabricacion = await _unitOfWork.TipoFabricacionRepository.GetAllAsync();
            var catalogoPaneles = await _unitOfWork.CatalogoPanelesPchRepository.GetAllAsync();

            return new CrearPrdPaneladoraPchDTO
            {
                CatMaquina = maquinas.Select(m => new MaquinaDto
                {
                    Id = m.Id,
                    Nombre = m.Nombre
                }).ToList(),
                CatTipoFabricacion = tiposFabricacion.Select(t => new TipoFabricacionDto
                {
                    Id = t.Id,
                    Descripcion = t.Descripcion
                }).ToList(),
                CatalogoPanelesPch = catalogoPaneles.ToList()
            };
        }

        public async Task<bool> ValidatePrdPaneladoraPchByIdAsync(int id, string userId)
        {
            try
            {
                var production = await _unitOfWork.PrdPaneladoraPchRepository.GetByIdAsync(id);
                if (production == null) return false;

                production.AprobadoSupervisor = true;
                production.IdAprobadoSupervisor = userId;
                production.IdUsuarioActualizacion = userId;
                production.FechaActualizacion = DateTime.Now;

                _unitOfWork.PrdPaneladoraPchRepository.Update(production);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AprovePrdPaneladoraPchByIdAsync(int id, string userId)
        {
            try
            {
                var production = await _unitOfWork.PrdPaneladoraPchRepository.GetByIdAsync(id);
                if (production == null) return false;

                production.AprobadoGerencia = true;
                production.IdAprobadoGerencia = userId;
                production.IdUsuarioActualizacion = userId;
                production.FechaActualizacion = DateTime.Now;

                _unitOfWork.PrdPaneladoraPchRepository.Update(production);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<PrdPaneladoraPchReporteDTO>> GetAllPrdPaneladoraPchWithDetailsAsync(DateTime start, DateTime end)
        {
            // Using repository pattern to get data with includes
            var productions = await _unitOfWork.ReportesDapperRepository
                .GetAllPrdPaneladoraPchWithDetailsAsync(start, end);

            return productions;
        }
    }
}