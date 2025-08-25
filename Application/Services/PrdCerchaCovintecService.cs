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
    public class PrdCerchaCovintecService : IPrdCerchaCovintecService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; // Si usas AutoMapper
        private readonly UserManager<IdentityUser> _userManager;
        public PrdCerchaCovintecService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
        }

        public async Task<bool> AproveDetAlambrePrdCerchaCovintecByIdAsync(int id, string userId)
        {
            var detAlambre =await _unitOfWork.DetAlambrePrdCerchaCovintecRepository.GetByIdAsync(id);
            if (detAlambre == null)
            {
                throw new ArgumentNullException(nameof(detAlambre));
            }
           

            if(detAlambre.AprobadoGerencia!=true)
            {
                if (detAlambre.AprobadoGerencia)
                {
                    detAlambre.AprobadoGerencia = false;
                }
                else
                {
                    detAlambre.AprobadoGerencia = true;
                }

                detAlambre.IdAprobadoGerencia = userId;
                detAlambre.FechaActualizacion = DateTime.Now;
                detAlambre.IdUsuarioActualizacion = userId;
                _unitOfWork.DetAlambrePrdCerchaCovintecRepository.Update(detAlambre);

                await _unitOfWork.SaveChangesAsync();
            }

            return detAlambre.AprobadoGerencia;
        }

        public async Task<bool> AproveDetPrdCerchaCovintecByIdAsync(int id, string userId)
        {
           var detPrd = await _unitOfWork.DetPrdCerchaCovintecRepository.GetByIdAsync(id);
            if (detPrd == null)
            {
                throw new ArgumentNullException(nameof(detPrd));
            }
            if (detPrd.AprobadoGerencia != true)
            {
                if (detPrd.AprobadoGerencia)
                {
                    detPrd.AprobadoGerencia = false;
                }
                else
                {
                    detPrd.AprobadoGerencia = true;
                }
                detPrd.IdAprobadoGerencia = userId;
                detPrd.FechaActualizacion = DateTime.Now;
                detPrd.IdUsuarioActualizacion = userId;
                _unitOfWork.DetPrdCerchaCovintecRepository.Update(detPrd);
                await _unitOfWork.SaveChangesAsync();
            }

            return detPrd.AprobadoGerencia;
        }

        public async Task CreateAsync(PrdCerchaCovintecDTO dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var catCercha = await _unitOfWork.CatalogoCerchaRepository.GetAllAsync();

                // 1. Crear y agregar la entidad maestro (cp.PrdCerchaCovintec)
                var prdCerchaCovintec = new PrdCerchaCovintec
                {
                    IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>()),
                    IdTipoReporte = dto.IdTipoReporte,
                    IdMaquina = dto.IdMaquina,
                    ConteoInicial = dto.ConteoInicial,
                    ConteoFinal = dto.ConteoFinal,
                    Fecha = (DateTime)dto.Fecha,
                    Observaciones = dto.Observaciones,
                    IdUsuarioCreacion = dto.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    MermaAlambre = dto.MermaAlambre,
                    TiempoParo = dto.TiempoParo,
                    AprobadoGerencia = false,
                    AprobadoSupervisor = false
                };

                await _unitOfWork.PrdCerchaCovintecRepository.AddAsync(prdCerchaCovintec);
                await _unitOfWork.SaveChangesAsync();

                // 2. Crear y agregar los detalles (cp.DetPrdCerchaCovintec)
                if (dto.DetPrdCerchaCovintecs != null)
                {
                    foreach (var detPrd in dto.DetPrdCerchaCovintecs)
                    {
                        var detPrdEntity = new DetPrdCerchaCovintec
                        {
                            IdCercha = prdCerchaCovintec.Id,
                            IdArticulo = detPrd.IdArticulo,
                            CantidadProducida = detPrd.CantidadProducida,
                            CantidadNoConforme = detPrd.CantidadNoConforme,
                            IdTipoFabricacion = detPrd.IdTipoFabricacion,
                            // Si NumeroPedido es nulo, se asigna 0 (o se maneja según convenga)
                            NumeroPedido = detPrd.NumeroPedido ?? 0,
                            ProduccionDia = (detPrd.CantidadProducida - detPrd.CantidadNoConforme) * catCercha.Where(x => x.Id == detPrd.IdArticulo).Select(c => c.LongitudMetros).FirstOrDefault(),
                            AprobadoGerencia = false,
                            AprobadoSupervisor = false,
                            IdUsuarioCreacion = dto.IdUsuarioCreacion,
                            FechaCreacion = prdCerchaCovintec.FechaCreacion
                        };
                        await _unitOfWork.DetPrdCerchaCovintecRepository.AddAsync(detPrdEntity);
                    }
                }

                // 3. Crear y agregar los detalles de alambre (cp.DetAlambrePrdCerchaCovintec)
                if (dto.DetAlambrePrdCerchaCovintecs != null)
                {
                    foreach (var detAl in dto.DetAlambrePrdCerchaCovintecs)
                    {
                        var detAlambrePrdCerchaCovintec = new DetAlambrePrdCerchaCovintec
                        {
                            IdCercha = prdCerchaCovintec.Id,
                            NumeroAlambre = detAl.NumeroAlambre,
                            PesoAlambre = detAl.PesoAlambre,
                            AprobadoGerencia = false,
                            AprobadoSupervisor = false,
                            IdUsuarioCreacion = dto.IdUsuarioCreacion,
                            FechaCreacion = prdCerchaCovintec.FechaCreacion
                        };

                        await _unitOfWork.DetAlambrePrdCerchaCovintecRepository.AddAsync(detAlambrePrdCerchaCovintec);
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

        public async Task DeleteDetPrd(DetPrdCerchaCovintecDTO dto)
        {
           
            var detPrdCercha = await _unitOfWork.DetPrdCerchaCovintecRepository.GetByIdAsync(dto.Id);
            if (detPrdCercha != null)
            {
                _unitOfWork.DetPrdCerchaCovintecRepository.Remove(detPrdCercha);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ShowPrdPanelesCovintecDto>> GetAllAsync()
        {
            var maquinas = _unitOfWork.CatMaquinaRepository.GetAllAsync().Result;
            //// 1. Obtener todas las producciones desde el repositorio
            var productions = await _unitOfWork.PrdCerchaCovintecRepository.GetAllAsync(
                                           x => x.DetAlambrePrdCerchaCovintecs,
                                           x => x.DetPrdCerchaCovintecs
                                               );

            //// 2. Extraer los IDs de usuario (separados por coma) de todos los registros y obtener una lista única
            var distinctUserIds = productions
                                  .SelectMany(p => p.IdUsuarios
                                  .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct())
                                  .ToList();

            // 3. Consultar los usuarios de Identity para obtener sus nombres (username)
            var identityUsers = _userManager.Users
                .Where(u => distinctUserIds.Contains(u.Id))
                .ToListAsync().Result;

            // Crear un diccionario para mapear id -> username
            var userDictionary = identityUsers.ToDictionary(u => u.Id, u => u.UserName);

            // 4. Mapear cada registro de producción a su DTO, transformando IdUsuarios a los nombres correspondientes
            var dtos = productions.Select(p =>
            {
                var userIds = p.IdUsuarios
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();
                var userNames = userIds.Select(id => userDictionary.ContainsKey(id) ? userDictionary[id] : id);
                var userNamesString = string.Join(", ", userNames);

                return new ShowPrdPanelesCovintecDto
                {
                    Id = p.Id,
                    IdTipoReporte = p.IdTipoReporte,
                    // Aquí se asigna la cadena con los nombres de usuario en lugar de los IDs
                    Operarios = userNamesString,
                    Maquina = maquinas.Where(x => x.Id == p.IdMaquina).FirstOrDefault().Nombre,
                    Fecha = p.Fecha,
                    AprobadoSupervisor = p.AprobadoSupervisor,
                    AprobadoGerencia = p.AprobadoGerencia

                };
            });
            var res = dtos.OrderByDescending(x => x.Id);

            return res;
        }

        public async Task<IEnumerable<PrdCerchaCovintecReporteDTO>> GetAllCerchaProduccionReporteWithDetailsAsync(DateTime start, DateTime end)
        {
            var cerchaProduccion = await _unitOfWork.ReportesDapperRepository.GetAllCerchaProduccionWithDetailsAsync(start, end);

            return cerchaProduccion;
        }

        public async Task<PrdCerchaCovintecDTO> GetByIdAsync(int id)
        {
            var productions = await _unitOfWork.PrdCerchaCovintecRepository.GetByIdAsync(id);

            var distinctUserIds = (productions.IdUsuarios ?? string.Empty)
                             .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(idStr => idStr.Trim());

            var userIds = distinctUserIds.ToArray();

            var prd = _unitOfWork.PrdCerchaCovintecRepository.GetByIdIncludeAsync(x => x.Id == id,
               x => x.DetAlambrePrdCerchaCovintecs,
               x => x.DetPrdCerchaCovintecs).Result;


            var dto = new PrdCerchaCovintecDTO
            {
                Id = prd.Id,
                IdTipoReporte = prd.IdTipoReporte,
                IdUsuarios = userIds.ToList(),
                IdMaquina = prd.IdMaquina,
                ConteoInicial = prd.ConteoInicial,
                ConteoFinal = prd.ConteoFinal,
                Fecha = prd.Fecha,
                Observaciones = prd.Observaciones,
                MermaAlambre = (decimal)prd.MermaAlambre,
                TiempoParo = prd.TiempoParo,
                AprobadoSupervisor = prd.AprobadoSupervisor,
                AprobadoGerencia = prd.AprobadoGerencia,
                DetAlambrePrdCerchaCovintecs = prd.DetAlambrePrdCerchaCovintecs
                                                        .Select(entity => new DetAlambrePrdCerchaCovintecDTO
                                                        {
                                                            Id = entity.Id,
                                                            IdCercha = entity.IdCercha,
                                                            NumeroAlambre = entity.NumeroAlambre,
                                                            PesoAlambre = entity.PesoAlambre,
                                                            IdUsuarioCreacion = entity.IdUsuarioCreacion,
                                                            FechaCreacion = entity.FechaCreacion,
                                                            IdUsuarioActualizacion = entity.IdUsuarioActualizacion,
                                                            FechaActualizacion = entity.FechaActualizacion,
                                                            AprobadoSupervisor = entity.AprobadoSupervisor,
                                                            AprobadoGerencia = entity.AprobadoGerencia,
                                                            IdAprobadoSupervisor = entity.IdAprobadoSupervisor,
                                                            IdAprobadoGerencia = entity.IdAprobadoGerencia
                                                        })
                                                        .ToList(),
                DetPrdCerchaCovintecs = prd.DetPrdCerchaCovintecs
                                        .Select(entity => new DetPrdCerchaCovintecDTO
                                        {
                                            Id = entity.Id,
                                            IdCercha = entity.IdCercha,
                                            IdArticulo = entity.IdArticulo,
                                            CantidadProducida = entity.CantidadProducida,
                                            CantidadNoConforme = entity.CantidadNoConforme,
                                            IdTipoFabricacion = entity.IdTipoFabricacion,
                                            NumeroPedido = entity.NumeroPedido,
                                            ProduccionDia = entity.ProduccionDia,
                                            AprobadoSupervisor = entity.AprobadoSupervisor,
                                            AprobadoGerencia = entity.AprobadoGerencia,
                                            IdAprobadoSupervisor = entity.IdAprobadoSupervisor,
                                            IdAprobadoGerencia = entity.IdAprobadoGerencia
                                        }).ToList()

            };

            return await Task.FromResult(dto);
        }

        public async Task<CrearPrdCerchaCovintecDto> GetCreateData()
        {
            var allowedMachines = _unitOfWork.catalogosPermitidosPorReporteRepository.FindAsync(cp =>
                                          cp.IdTipoReporte == 7 &&
                                          cp.Catalogo == "cp.Maquinas").Result.Select(x => x.IdCatalogo).ToList();

            var CatMaquina = allowedMachines.Any()
 ? (await _unitOfWork.CatMaquinaRepository.FindAsync(x => allowedMachines.Contains(x.Id))).ToList()
 : (await _unitOfWork.CatMaquinaRepository.FindAsync(x => true)).ToList();


            var dto = new CrearPrdCerchaCovintecDto
            {
                CatMaquina = _mapper.Map<List<MaquinaDto>>(CatMaquina.Where(x => x.Activo == true).ToList()),
                CatalogoCercha = _mapper.Map<List<CatalogoCerchaDTO>>(_unitOfWork.CatalogoCerchaRepository.GetAllAsync().Result),
                CatTipoFabricacion = _mapper.Map<List<TipoFabricacionDto>>(_unitOfWork.TipoFabricacionRepository.GetAllAsync().Result.Where(x => x.Activo == true).ToList()),


            };

            return dto;
        }

        public async Task UpdateAsync(PrdCerchaCovintecDTO dto)
        {
           var prd = await _unitOfWork.PrdCerchaCovintecRepository.GetByIdAsync(dto.Id);

            if (prd != null)
            {
               
                prd.IdMaquina = dto.IdMaquina;
                prd.ConteoInicial = dto.ConteoInicial;
                prd.ConteoFinal = dto.ConteoFinal;
                prd.Fecha = (DateTime)dto.Fecha;
                prd.Observaciones = dto.Observaciones;
                prd.MermaAlambre = dto.MermaAlambre;
                prd.TiempoParo = dto.TiempoParo;
                prd.IdUsuarioActualizacion = dto.IdUsuarioCreacion;
                prd.FechaActualizacion = DateTime.Now;
                _unitOfWork.PrdCerchaCovintecRepository.Update(prd);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetAlambrePrd(DetAlambrePrdCerchaCovintecDTO dto)
        {
            var detAlambre = await _unitOfWork.DetAlambrePrdCerchaCovintecRepository.GetByIdAsync(dto.Id);
            if (detAlambre != null)
            {
                detAlambre.NumeroAlambre = dto.NumeroAlambre;
                detAlambre.PesoAlambre = dto.PesoAlambre;


                detAlambre.IdUsuarioActualizacion = dto.IdUsuarioCreacion;
                detAlambre.FechaActualizacion = DateTime.Now;
                _unitOfWork.DetAlambrePrdCerchaCovintecRepository.Update(detAlambre);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetPrd(DetPrdCerchaCovintecDTO dto)
        {
           var detPrd=await _unitOfWork.DetPrdCerchaCovintecRepository.GetByIdAsync(dto.Id);
            if (detPrd != null)
            {
                detPrd.IdArticulo = dto.IdArticulo;
                detPrd.CantidadProducida = dto.CantidadProducida;
                detPrd.CantidadNoConforme = dto.CantidadNoConforme;
                detPrd.IdTipoFabricacion = dto.IdTipoFabricacion;
                detPrd.NumeroPedido = dto.NumeroPedido;

                var longitud = (await _unitOfWork.CatalogoCerchaRepository.GetByIdAsync(dto.IdArticulo))?.LongitudMetros ?? 0;


                detPrd.ProduccionDia  = (dto.CantidadProducida - dto.CantidadNoConforme) * longitud;
                detPrd.IdUsuarioActualizacion = dto.IdUsuarioCreacion;
                detPrd.FechaActualizacion = DateTime.Now;
                _unitOfWork.DetPrdCerchaCovintecRepository.Update(detPrd);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<bool> ValidateDetAlambrePrdCerchaCovintecByIdAsync(int id, string userId)
        {
            var detAlambrePrdCerchaCovintec = await _unitOfWork.DetAlambrePrdCerchaCovintecRepository.GetByIdAsync(id);

            if (detAlambrePrdCerchaCovintec.AprobadoSupervisor != true)
            {
                if (detAlambrePrdCerchaCovintec.AprobadoSupervisor)
                {
                    detAlambrePrdCerchaCovintec.AprobadoSupervisor = false;
                }
                else
                {
                    detAlambrePrdCerchaCovintec.AprobadoSupervisor = true;
                }

                detAlambrePrdCerchaCovintec.IdAprobadoSupervisor = userId;
                detAlambrePrdCerchaCovintec.FechaActualizacion = DateTime.Now;
                detAlambrePrdCerchaCovintec.IdUsuarioActualizacion = userId;
                _unitOfWork.DetAlambrePrdCerchaCovintecRepository.Update(detAlambrePrdCerchaCovintec);

                await _unitOfWork.SaveChangesAsync();
            }


            return detAlambrePrdCerchaCovintec.AprobadoSupervisor;
        }

        public async Task<bool> ValidateDetPrdCerchaCovintecByIdAsync(int id, string userId)
        {
            var detPrdCerchaCovintec = await _unitOfWork.DetPrdCerchaCovintecRepository.GetByIdAsync(id);

            if (detPrdCerchaCovintec.AprobadoSupervisor != true)
            {
                if (detPrdCerchaCovintec.AprobadoSupervisor)
                {
                    detPrdCerchaCovintec.AprobadoSupervisor = false;
                }
                else
                {
                    detPrdCerchaCovintec.AprobadoSupervisor = true;
                }
                detPrdCerchaCovintec.IdAprobadoSupervisor = userId;
                detPrdCerchaCovintec.FechaActualizacion = DateTime.Now;
                detPrdCerchaCovintec.IdUsuarioActualizacion = userId;
                _unitOfWork.DetPrdCerchaCovintecRepository.Update(detPrdCerchaCovintec);
                await _unitOfWork.SaveChangesAsync();
            }

            return detPrdCerchaCovintec.AprobadoSupervisor;
        }

        public async Task<bool> AprovePrdCerchaCovintecByIdAsync(int id, string userId)
        {
            var prdCerchaCovintec = await _unitOfWork.PrdCerchaCovintecRepository.GetByIdAsync(id);
            if (prdCerchaCovintec == null)
            {
                return await Task.FromResult(false);
            }
            if (prdCerchaCovintec.AprobadoGerencia != true)
            {
                if (prdCerchaCovintec.AprobadoGerencia)
                {
                    prdCerchaCovintec.AprobadoGerencia = false;
                }
                else
                {
                    prdCerchaCovintec.AprobadoGerencia = true;
                }
                prdCerchaCovintec.IdAprobadoGerencia = userId;
                prdCerchaCovintec.FechaActualizacion = DateTime.Now;
                prdCerchaCovintec.IdUsuarioActualizacion = userId;
                _unitOfWork.PrdCerchaCovintecRepository.Update(prdCerchaCovintec);
                await _unitOfWork.SaveChangesAsync();
            }
            return prdCerchaCovintec.AprobadoGerencia;
        }

        public async Task<bool> ValidatePrdCechaCovintecByIdAsync(int id, string userId)
        {
            var prdCerchaCovintec = await _unitOfWork.PrdCerchaCovintecRepository.GetByIdAsync(id);
            if (prdCerchaCovintec.AprobadoSupervisor != true)
            {
                if (prdCerchaCovintec.AprobadoSupervisor)
                {
                    prdCerchaCovintec.AprobadoSupervisor = false;
                }
                else
                {
                    prdCerchaCovintec.AprobadoSupervisor = true;
                }
                prdCerchaCovintec.IdAprobadoSupervisor = userId;
                prdCerchaCovintec.FechaActualizacion = DateTime.Now;
                prdCerchaCovintec.IdUsuarioActualizacion = userId;
                _unitOfWork.PrdCerchaCovintecRepository.Update(prdCerchaCovintec);
                await _unitOfWork.SaveChangesAsync();
            }
            return prdCerchaCovintec.AprobadoSupervisor;
        }
    }
}
