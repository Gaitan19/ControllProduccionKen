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
    public class PrdBloquesService : IPrdBloquesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public PrdBloquesService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
        }

        public async Task CreateAsync(PrdBloqueDTO dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var prdBloque = new PrdBloque
                {
                    Fecha = dto.Fecha,
                    TiempoParo = dto.TiempoParo,
                    IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>()),
                    IdUsuarioCreacion = dto.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    AprobadoSupervisor = false,
                    AprobadoGerencia = false
                };

                await _unitOfWork.PrdBloqueRepository.AddAsync(prdBloque);
                await _unitOfWork.SaveChangesAsync();
                var idPrdBloque = prdBloque.Id;

                if (dto.DetPrdBloques != null && dto.DetPrdBloques.Any())
                {
                    foreach (var detalle in dto.DetPrdBloques)
                    {
                        var detPrdBloque = new DetPrdBloque
                        {
                            PrdBloqueId = idPrdBloque,
                            IdMaquina = detalle.IdMaquina,
                            ProduccionDia=detalle.SubDetPrdBloques?.Sum(x => x.Peso) ?? 0,
                            IdCatBloque = detalle.IdCatBloque,
                            IdUsuarioCreacion = dto.IdUsuarioCreacion,
                            FechaCreacion = prdBloque.FechaCreacion
                        };

                        await _unitOfWork.DetPrdBloqueRepository.AddAsync(detPrdBloque);
                        await _unitOfWork.SaveChangesAsync();
                        var idDetPrdBloque = detPrdBloque.Id;

                        if (detalle.SubDetPrdBloques != null && detalle.SubDetPrdBloques.Any())
                        {
                            var subDetPrdBloques = detalle.SubDetPrdBloques.Select(subDetalle => new SubDetPrdBloque
                            {
                                DetPrdBloquesId = idDetPrdBloque,
                                IdArticulo = subDetalle.IdArticulo,
                                No = subDetalle.No,
                                Hora = subDetalle.Hora,
                                Silo = subDetalle.Silo,
                                IdDensidad = subDetalle.IdDensidad,
                                IdTipoBloque = subDetalle.IdTipoBloque,
                                Peso = subDetalle.Peso,
                                IdTipoFabricacion = subDetalle.IdTipoFabricacion,
                                NumeroPedido = subDetalle.NumeroPedido,
                                CodigoBloque = subDetalle.CodigoBloque,
                                Observaciones = subDetalle.Observaciones,
                                IdUsuarioCreacion = dto.IdUsuarioCreacion,
                                FechaCreacion = prdBloque.FechaCreacion
                            }).ToList();
                            
                            await _unitOfWork.SubDetPrdBloqueRepository.BulkInsertAndSaveAsync(subDetPrdBloques);
                          

                            foreach (var sub in subDetPrdBloques)
                                sub.CodigoBloque =  sub.Id.ToString()+"-"+sub.CodigoBloque;


                            await _unitOfWork.SubDetPrdBloqueRepository.BulkUpdateAsync(subDetPrdBloques);

                           
                        }
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

        public async Task<IEnumerable<ShowPrdBloqueDto>> GetAllAsync()
        {
            var productions = await _unitOfWork.PrdBloqueRepository.GetAllAsync(
                x => x.DetPrdBloques);

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
                return new ShowPrdBloqueDto
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

        public async Task<PrdBloqueDTO> GetByIdAsync(int id)
        {
            var lista = await _unitOfWork.PrdBloqueRepository.GetAllAsync(
                                        filter: b => b.Id == id,
                                        include: q => q
                                            .Include(b => b.DetPrdBloques)               // 1er nivel
                                              .ThenInclude(d => d.SubDetPrdBloques)      // 2� nivel
                                    );

            var prd = lista.SingleOrDefault();

            if (prd == null) return null!;

            var userIds = prd.IdUsuarios.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            return new PrdBloqueDTO
            {
                Id = prd.Id,
                IdUsuarios = userIds,
                Fecha = prd.Fecha,
                TiempoParo = prd.TiempoParo,
                IdUsuarioCreacion = prd.IdUsuarioCreacion,
                FechaCreacion = prd.FechaCreacion,
                IdUsuarioActualizacion = prd.IdUsuarioActualizacion,
                FechaActualizacion = prd.FechaActualizacion,
                AprobadoSupervisor = prd.AprobadoSupervisor,
                AprobadoGerencia = prd.AprobadoGerencia,
                IdAprobadoSupervisor = prd.IdAprobadoSupervisor,
                IdAprobadoGerencia = prd.IdAprobadoGerencia,
                DetPrdBloques = prd.DetPrdBloques?.Select(entity => new DetPrdBloqueDTO
                {
                    Id = entity.Id,
                    PrdBloqueId = entity.PrdBloqueId,
                    IdMaquina = entity.IdMaquina,
                    IdCatBloque = entity.IdCatBloque,
                    IdUsuarioCreacion = entity.IdUsuarioCreacion,
                    FechaCreacion = entity.FechaCreacion,
                    IdUsuarioActualizacion = entity.IdUsuarioActualizacion,
                    FechaActualizacion = entity.FechaActualizacion,
                    ProduccionDia = entity.ProduccionDia,
                    SubDetPrdBloques = entity.SubDetPrdBloques?.Select(subEntity => new SubDetPrdBloqueDTO
                    {
                        Id = subEntity.Id,
                        DetPrdBloquesId = subEntity.DetPrdBloquesId,
                        IdArticulo = subEntity.IdArticulo,
                        No = subEntity.No,
                        Hora = subEntity.Hora,
                        Silo = subEntity.Silo,
                        IdDensidad = subEntity.IdDensidad,
                        IdTipoBloque = subEntity.IdTipoBloque,
                        Peso = subEntity.Peso,
                        IdTipoFabricacion = subEntity.IdTipoFabricacion,
                        NumeroPedido = subEntity.NumeroPedido,
                        CodigoBloque = subEntity.CodigoBloque,
                        Observaciones = subEntity.Observaciones,
                       
                        IdUsuarioCreacion = subEntity.IdUsuarioCreacion,
                        FechaCreacion = subEntity.FechaCreacion,
                        IdUsuarioActualizacion = subEntity.IdUsuarioActualizacion,
                        FechaActualizacion = subEntity.FechaActualizacion
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<CrearPrdBloqueDto> GetCreateData()
        {
            var allowedMachines = _unitOfWork.catalogosPermitidosPorReporteRepository.FindAsync(cp =>
                                        cp.IdTipoReporte == 2 &&
                                        cp.Catalogo == "cp.Maquinas").Result.Select(x => x.IdCatalogo).ToList();
            var CatMaquina = allowedMachines.Any()
 ? (await _unitOfWork.CatMaquinaRepository.FindAsync(x => allowedMachines.Contains(x.Id))).ToList()
 : (await _unitOfWork.CatMaquinaRepository.FindAsync(x => true)).ToList();

            var dto = new CrearPrdBloqueDto
            {
                CatMaquina = _mapper.Map<List<MaquinaDto>>(CatMaquina.ToList()),
                CatalogoBloques = _mapper.Map<List<CatalogoBloqueDTO>>(
                    (await _unitOfWork.CatalogoBloqueRepository.FindAsync(x => x.Activo == true)).ToList()),
                CatalogoDensidad = (await _unitOfWork.MaestroCatalogoRepository.FindAsync(x => x.Activo == true && x.IdPadre == 6)).ToList(),
                CatalogoTipoBloque = (await _unitOfWork.MaestroCatalogoRepository.FindAsync(x => x.Activo == true && x.IdPadre == 7)).ToList(),
                CatTipoFabricacion = _mapper.Map<List<TipoFabricacionDto>>(
                    (await _unitOfWork.TipoFabricacionRepository.FindAsync(x => x.Activo == true)).ToList())
            };

            return dto;
        }

        public async Task<bool> ValidatePrdBloqueByIdAsync(int id, string userId)
        {
            var prdBloque = await _unitOfWork.PrdBloqueRepository.GetByIdAsync(id);
            if (prdBloque == null)
            {
                return false;
            }

            if (prdBloque.AprobadoSupervisor)
            {
                return false;
            }

            prdBloque.AprobadoSupervisor = true;
            prdBloque.IdAprobadoSupervisor = userId;
            prdBloque.IdUsuarioActualizacion = userId;
            prdBloque.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdBloqueRepository.Update(prdBloque);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AprovePrdBloqueByIdAsync(int id, string userId)
        {
            var prdBloque = await _unitOfWork.PrdBloqueRepository.GetByIdAsync(id);
            if (prdBloque == null)
            {
                return false;
            }

            if (prdBloque.AprobadoGerencia)
            {
                return false;
            }

            prdBloque.AprobadoGerencia = true;
            prdBloque.IdAprobadoGerencia = userId;
            prdBloque.IdUsuarioActualizacion = userId;
            prdBloque.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdBloqueRepository.Update(prdBloque);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId)
        {
            var prd = await _unitOfWork.PrdBloqueRepository.GetByIdAsync(id);
            if (prd == null)
            {
                return false;
            }

            prd.NotaSupervisor = notaSupervisor;
            prd.IdUsuarioActualizacion = userId;
            prd.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdBloqueRepository.Update(prd);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAsync(PrdBloqueDTO dto)
        {
            var prd = await _unitOfWork.PrdBloqueRepository.GetByIdAsync(dto.Id);
            if (prd != null)
            {
                prd.Fecha = dto.Fecha;
                prd.TiempoParo = dto.TiempoParo;
                prd.IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>());
                prd.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                prd.FechaActualizacion = DateTime.Now;
                _unitOfWork.PrdBloqueRepository.Update(prd);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetPrd(DetPrdBloqueDTO dto)
        {
            var det = await _unitOfWork.DetPrdBloqueRepository.GetByIdAsync(dto.Id);
            var subdet=await _unitOfWork.SubDetPrdBloqueRepository.FindAsync(x => x.DetPrdBloquesId == dto.Id);
            if (det != null)
            {
                det.IdMaquina = dto.IdMaquina;
                det.IdCatBloque = dto.IdCatBloque;
                det.ProduccionDia = subdet?.Sum(x => x.Peso) ?? 0;
                det.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                det.FechaActualizacion = DateTime.Now;
                _unitOfWork.DetPrdBloqueRepository.Update(det);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteDetPrd(DetPrdBloqueDTO dto)
        {
            var det = await _unitOfWork.DetPrdBloqueRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                _unitOfWork.DetPrdBloqueRepository.Remove(det);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateSubDetPrd(SubDetPrdBloqueDTO dto)
        {
            var subDet = await _unitOfWork.SubDetPrdBloqueRepository.GetByIdAsync(dto.Id);
            if (subDet != null)
            {
                subDet.DetPrdBloquesId = dto.DetPrdBloquesId;
                subDet.IdArticulo = dto.IdArticulo;
                subDet.No = dto.No;
                subDet.Hora = dto.Hora;
                subDet.Silo = dto.Silo;
                subDet.IdDensidad = dto.IdDensidad;
                subDet.IdTipoBloque = dto.IdTipoBloque;
                subDet.Peso = dto.Peso;
                subDet.IdTipoFabricacion = dto.IdTipoFabricacion;
                subDet.NumeroPedido = dto.NumeroPedido;
                subDet.CodigoBloque = dto.CodigoBloque;
                subDet.Observaciones = dto.Observaciones;
                
                subDet.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                subDet.FechaActualizacion = DateTime.Now;
                _unitOfWork.SubDetPrdBloqueRepository.Update(subDet);


                // 3) Recalcular ProduccionDia como suma de todos los pesos
                var totalPeso =  _unitOfWork.SubDetPrdBloqueRepository.FindAsync(x => x.DetPrdBloquesId == dto.DetPrdBloquesId).Result.Sum(x => x.Peso);

                var det = await _unitOfWork.DetPrdBloqueRepository.GetByIdAsync(dto.DetPrdBloquesId);
                if (det != null)
                {
                    det.ProduccionDia = totalPeso;
                    det.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                    det.FechaActualizacion = DateTime.Now;
                    _unitOfWork.DetPrdBloqueRepository.Update(det);
                }


                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteSubDetPrd(SubDetPrdBloqueDTO dto)
        {
            var subDet = await _unitOfWork.SubDetPrdBloqueRepository.GetByIdAsync(dto.Id);
            if (subDet != null)
            {
                _unitOfWork.SubDetPrdBloqueRepository.Remove(subDet);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public  Task<IEnumerable<PrdBloquesReporteDTO>> GetAllPrdBloqueWithDetailsAsync(DateTime start, DateTime end)
        {
             return _unitOfWork.ReportesDapperRepository.GetAllPrdBloqueWithDetailsAsync(start, end);
        }
    }
}