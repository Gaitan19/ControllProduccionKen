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
    public class PrdPanelesCovintecService: IPrdPanelesCovintecService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; // Si usas AutoMapper
        private readonly UserManager<IdentityUser> _userManager;

        public PrdPanelesCovintecService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
        }
        public async Task CreateAsync(PrdPanelesCovintecDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {

                var panelesCovintec = await _unitOfWork.CatalogoPanelesCovintecRepository.GetAllAsync();

                await _unitOfWork.BeginTransactionAsync();

                // 1. Crear y agregar la entidad maestro (cp.PrdPanelesCovintec)
                var prdPanelesCovintec = new PrdPanelesCovintec
                {
                    IdMaquina = dto.IdMaquina,
                    Fecha = (DateTime)dto.Fecha,
                    Observaciones = dto.Observaciones,
                    MermaAlambre = dto.MermaAlambre,
                    TiempoParo = dto.TiempoParo,
                    IdUsuarioCreacion = dto.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    IdTipoReporte = 9,
                    IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>()),
                    AprobadoSupervisor = false,
                    AprobadoGerencia = false

                };

                await _unitOfWork.PrdPanelesCovintecRepository.AddAsync(prdPanelesCovintec);
                await _unitOfWork.SaveChangesAsync();

                int idPrdPanelesCovintec = prdPanelesCovintec.Id;

                // 2. Agregar los detalles de producción (cp.DetPrdPanelesCovintec)
                if (dto.DetPrdPanelesCovintecs != null)
                {
                    foreach (var detalle in dto.DetPrdPanelesCovintecs)
                    {
                        var detallePrdPanelesCovintec = new DetPrdPanelesCovintec
                        {
                            IdPanel = idPrdPanelesCovintec, // Relaciona con el maestro
                            IdArticulo = detalle.IdArticulo,
                            CantidadProducida = detalle.CantidadProducida,
                            CantidadNoConforme = detalle.CantidadNoConforme,
                            IdTipoFabricacion = detalle.IdTipoFabricacion,
                            // Si NumeroPedido es nulo, se asigna 0 (o se maneja según convenga)
                            NumeroPedido = detalle.NumeroPedido ?? 0,
                            ProduccionDia = (detalle.CantidadProducida - detalle.CantidadNoConforme) * panelesCovintec.Where(x => x.Id == detalle.IdArticulo).Select(c => c.Mts2PorPanel).FirstOrDefault(),
                            AprobadoGerencia = false,
                            AprobadoSupervisor = false,
                            IdUsuarioCreacion = dto.IdUsuarioCreacion,
                            FechaCreacion = prdPanelesCovintec.FechaCreacion
                        };
                        await _unitOfWork.DetPrdPanelesCovintecRepository.AddAsync(detallePrdPanelesCovintec);
                    }
                }


                // 3. Agregar los detalles de alambre (cp.DetAlambrePrdPanelesCovintec)
                if (dto.DetAlambrePrdPanelesCovintecs != null)
                {
                    foreach (var detAl in dto.DetAlambrePrdPanelesCovintecs)
                    {
                        var detAlambrePrdPanelesCovintec = new DetAlambrePrdPanelesCovintec
                        {
                            IdPanel = idPrdPanelesCovintec, // Relaciona con el maestro
                            NumeroAlambre = detAl.NumeroAlambre,
                            PesoAlambre = detAl.PesoAlambre,
                            AprobadoGerencia = false,
                            AprobadoSupervisor = false,
                            IdUsuarioCreacion = dto.IdUsuarioCreacion,
                            FechaCreacion = prdPanelesCovintec.FechaCreacion
                        };
                        await _unitOfWork.DetAlambrePrdPanelesCovintecRepository.AddAsync(detAlambrePrdPanelesCovintec);
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
   
        public async Task<IEnumerable<ShowPrdPanelesCovintecDto>> GetAllAsync()
        {
            var maquinas = await _unitOfWork.CatMaquinaRepository.GetAllAsync();

            //// 1. Obtener todas las producciones desde el repositorio
            //var productions = _unitOfWork.PrdPanelesCovintecRepository.GetAllAsync().Result;
            var productions = await _unitOfWork.PrdPanelesCovintecRepository.GetAllAsync(
                                            x => x.DetAlambrePrdPanelesCovintecs,
                                            x => x.DetPrdPanelesCovintecs
                                                );

            //// 2. Extraer los IDs de usuario (separados por coma) de todos los registros y obtener una lista única
            //var distinctUserIds = productions
            //    .SelectMany(p => p.IdUsuarios.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //    .Distinct()
            //    .ToList();
            var distinctUserIds = productions
                                  .SelectMany(p => p.IdUsuarios
                                  .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct())
                                  .ToList();




            //// 3. Consultar los usuarios de Identity para obtener sus nombres (username)
            var identityUsers = await _userManager.Users
                .Where(u => distinctUserIds.Contains(u.Id))
                .ToListAsync();

            //// Crear un diccionario para mapear id -> username
            var userDictionary = identityUsers.ToDictionary(u => u.Id, u => u.UserName);

            //// 4. Mapear cada registro de producción a su DTO, transformando IdUsuarios a los nombres correspondientes
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
                    Maquina = maquinas.Where(x=>x.Id==p.IdMaquina).FirstOrDefault().Nombre,
                    Fecha = p.Fecha,
                    AprobadoSupervisor = p.AprobadoSupervisor,
                    AprobadoGerencia = p.AprobadoGerencia

                };
            });
            var res=dtos.OrderByDescending(x => x.Id);

            return res;
        }
        public async Task<PrdPanelesCovintecDto> GetByIdAsync(int id)
        {
            var productions = await _unitOfWork.PrdPanelesCovintecRepository.GetByIdAsync(id);
           
            var distinctUserIds =  (productions.IdUsuarios ?? string.Empty)
                              .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                              .Select(idStr => idStr.Trim());

         

            var userIds = distinctUserIds.ToArray();

            var prd = await _unitOfWork.PrdPanelesCovintecRepository.GetByIdIncludeAsync(x => x.Id == id,
                x => x.DetAlambrePrdPanelesCovintecs,
                x => x.DetPrdPanelesCovintecs);
                
                var dto = new PrdPanelesCovintecDto
                {
                    Id = prd.Id,
                    IdUsuarios = userIds.ToList(),
                    IdMaquina = prd.IdMaquina,
                    Fecha = prd.Fecha,
                    Observaciones = prd.Observaciones,
                    MermaAlambre = (decimal)prd.MermaAlambre,
                    TiempoParo = prd.TiempoParo,
                    IdUsuarioCreacion = prd.IdUsuarioCreacion,
                    AprobadoSupervisor = prd.AprobadoSupervisor,
                    AprobadoGerencia = prd.AprobadoGerencia,
                    DetAlambrePrdPanelesCovintecs =  prd.DetAlambrePrdPanelesCovintecs
                                                        .Select(entity => new DetAlambrePrdPanelesCovintecDTO
                                                        {
                                                            Id = entity.Id,
                                                            IdPanel = entity.IdPanel,
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
                    DetPrdPanelesCovintecs = prd.DetPrdPanelesCovintecs
                                                .Select(entity => new DetPrdPanelesCovintecDTO
                                                {
                                                    Id = entity.Id,
                                                    IdPanel = entity.IdPanel,
                                                    IdArticulo = entity.IdArticulo,
                                                    CantidadProducida = entity.CantidadProducida,
                                                    CantidadNoConforme = entity.CantidadNoConforme,
                                                    IdTipoFabricacion = entity.IdTipoFabricacion,
                                                    NumeroPedido = entity.NumeroPedido,
                                                    ProduccionDia = entity.ProduccionDia,
                                                    AprobadoSupervisor = entity.AprobadoSupervisor,
                                                    AprobadoGerencia = entity.AprobadoGerencia,
                                                    IdAprobadoSupervisor = entity.IdAprobadoSupervisor,
                                                    IdAprobadoGerencia = entity.IdAprobadoGerencia,
                                                    IdUsuarioCreacion = entity.IdUsuarioCreacion,
                                                    FechaCreacion = entity.FechaCreacion,
                                                    IdUsuarioActualizacion = entity.IdUsuarioActualizacion,
                                                    FechaActualizacion = entity.FechaActualizacion
                                                })
                                                .ToList()
                };

            return await Task.FromResult(dto);
        }

        public async Task<CrearPrdPanelesCovintecDto> GetCreateData()
        {
             
            var allowedMachines =  (await _unitOfWork.catalogosPermitidosPorReporteRepository.FindAsync(cp =>
                                           cp.IdTipoReporte == 9 &&
                                           cp.Catalogo == "cp.Maquinas" )).Select(x=>x.IdCatalogo).ToList();

            var CatMaquina = allowedMachines.Any()
    ? (await _unitOfWork.CatMaquinaRepository.FindAsync(x => allowedMachines.Contains(x.Id))).ToList()
    : (await _unitOfWork.CatMaquinaRepository.FindAsync(x => true)).ToList();


            var dto = new CrearPrdPanelesCovintecDto
            {
                CatMaquina = _mapper.Map<List<MaquinaDto>>(CatMaquina.Where(x=>x.Activo==true).ToList()),
                CatalogoPaneles = _mapper.Map<List<CatalogoPanelesCovintecDTO>>(await _unitOfWork.CatalogoPanelesCovintecRepository.GetAllAsync()),
                CatTipoFabricacion = _mapper.Map<List<TipoFabricacionDto>>((await _unitOfWork.TipoFabricacionRepository.GetAllAsync()).Where(x => x.Activo == true).ToList()),

              
            };

            return dto;

            
        }

        public async Task<bool> ValidateDetAlambrePrdPanelesCovintecByIdAsync(int id,string userId)
        {
             var detAlambrePrdPanelesCovintec = await _unitOfWork.DetAlambrePrdPanelesCovintecRepository.GetByIdAsync(id);

            if (detAlambrePrdPanelesCovintec.AprobadoSupervisor!=true)
            {
                if (detAlambrePrdPanelesCovintec.AprobadoSupervisor)
                {
                    detAlambrePrdPanelesCovintec.AprobadoSupervisor = false;
                }
                else
                {
                    detAlambrePrdPanelesCovintec.AprobadoSupervisor = true;
                }

                detAlambrePrdPanelesCovintec.IdAprobadoSupervisor = userId;
                detAlambrePrdPanelesCovintec.FechaActualizacion = DateTime.Now;
                detAlambrePrdPanelesCovintec.IdUsuarioActualizacion = userId;
                _unitOfWork.DetAlambrePrdPanelesCovintecRepository.Update(detAlambrePrdPanelesCovintec);

                await _unitOfWork.SaveChangesAsync();
            }
           

            return detAlambrePrdPanelesCovintec.AprobadoSupervisor;

        }

        public async Task UpdateAsync(PrdPanelesCovintecDto dto)
        {
            var prd= await _unitOfWork.PrdPanelesCovintecRepository.GetByIdAsync(dto.Id);

            if (prd!=null)
            {
                prd.IdMaquina = dto.IdMaquina;
                prd.Fecha = (DateTime)dto.Fecha;
                prd.Observaciones = dto.Observaciones;
                prd.MermaAlambre = dto.MermaAlambre;
                prd.TiempoParo = dto.TiempoParo;
                prd.IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>());
                prd.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                prd.FechaActualizacion = DateTime.Now;

                _unitOfWork.PrdPanelesCovintecRepository.Update(prd);
                await _unitOfWork.SaveChangesAsync();
            }

           

          
        }

        public async Task<bool> ValidateDetPrdPanelesCovintecByIdAsync(int id, string userId)
        {
            var detPrdPanelesCovintec = await _unitOfWork.DetPrdPanelesCovintecRepository.GetByIdAsync(id);

            if (detPrdPanelesCovintec.AprobadoSupervisor != true)
            {
                if (detPrdPanelesCovintec.AprobadoSupervisor)
                {
                    detPrdPanelesCovintec.AprobadoSupervisor = false;
                }
                else
                {
                    detPrdPanelesCovintec.AprobadoSupervisor = true;
                }
                detPrdPanelesCovintec.IdAprobadoSupervisor = userId;
                detPrdPanelesCovintec.FechaActualizacion = DateTime.Now;
                detPrdPanelesCovintec.IdUsuarioActualizacion = userId;
                _unitOfWork.DetPrdPanelesCovintecRepository.Update(detPrdPanelesCovintec);
               await _unitOfWork.SaveChangesAsync();
            }

            return detPrdPanelesCovintec.AprobadoSupervisor;
        }

        public async Task<bool> AproveDetAlambrePrdPanelesCovintecByIdAsync(int id, string userId)
        {
            var detAlambrePrdPanelesCovintec = await _unitOfWork.DetAlambrePrdPanelesCovintecRepository.GetByIdAsync(id);

            if (detAlambrePrdPanelesCovintec.AprobadoGerencia != true)
            {
                if (detAlambrePrdPanelesCovintec.AprobadoGerencia)
                {
                    detAlambrePrdPanelesCovintec.AprobadoGerencia = false;
                }
                else
                {
                    detAlambrePrdPanelesCovintec.AprobadoGerencia = true;
                }

                detAlambrePrdPanelesCovintec.IdAprobadoGerencia = userId;
                detAlambrePrdPanelesCovintec.FechaActualizacion = DateTime.Now;
                detAlambrePrdPanelesCovintec.IdUsuarioActualizacion = userId;
                _unitOfWork.DetAlambrePrdPanelesCovintecRepository.Update(detAlambrePrdPanelesCovintec);

                await _unitOfWork.SaveChangesAsync();
            }


            return detAlambrePrdPanelesCovintec.AprobadoGerencia;
        }

        public async Task<bool> AproveDetPrdPanelesCovintecByIdAsync(int id, string userId)
        {
            var detPrdPanelesCovintec = await _unitOfWork.DetPrdPanelesCovintecRepository.GetByIdAsync(id);

            if (detPrdPanelesCovintec.AprobadoGerencia != true)
            {
                if (detPrdPanelesCovintec.AprobadoGerencia)
                {
                    detPrdPanelesCovintec.AprobadoGerencia = false;
                }
                else
                {
                    detPrdPanelesCovintec.AprobadoGerencia = true;
                }
                detPrdPanelesCovintec.IdAprobadoGerencia = userId;
                detPrdPanelesCovintec.FechaActualizacion = DateTime.Now;
                detPrdPanelesCovintec.IdUsuarioActualizacion = userId;
                _unitOfWork.DetPrdPanelesCovintecRepository.Update(detPrdPanelesCovintec);
                await _unitOfWork.SaveChangesAsync();
            }

            return detPrdPanelesCovintec.AprobadoGerencia;
        }

        public async Task UpdateDetPrd(DetPrdPanelesCovintecDTO dto)
        {
            var detPrd= await _unitOfWork.DetPrdPanelesCovintecRepository.GetByIdAsync(dto.Id);

            if (detPrd!=null)
            {
                // Actualización de propiedades básicas
                detPrd.IdArticulo = dto.IdArticulo;
                detPrd.CantidadProducida = dto.CantidadProducida;
                detPrd.CantidadNoConforme = dto.CantidadNoConforme;
                detPrd.IdTipoFabricacion = dto.IdTipoFabricacion;
                detPrd.NumeroPedido = dto.NumeroPedido;

                // Obtención de Mts2PorPanel desde el catálogo de paneles
                var mts2PorPanel = (await _unitOfWork.CatalogoPanelesCovintecRepository.GetByIdAsync(dto.IdArticulo))?.Mts2PorPanel ?? 0;

                detPrd.ProduccionDia = (dto.CantidadProducida - dto.CantidadNoConforme) * mts2PorPanel;

       

                // Actualización de metadatos
                detPrd.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                detPrd.FechaActualizacion = DateTime.Now;

                _unitOfWork.DetPrdPanelesCovintecRepository.Update(detPrd);
                await _unitOfWork.SaveChangesAsync();
            }

        }

        public async Task DeleteDetPrd(DetPrdPanelesCovintecDTO dto)
        {
            var detPrd = await _unitOfWork.DetPrdPanelesCovintecRepository.GetByIdAsync(dto.Id);
            if (detPrd != null)
            {
                _unitOfWork.DetPrdPanelesCovintecRepository.Remove(detPrd);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetAlambrePrd(DetAlambrePrdPanelesCovintecDTO dto)
        {
            var detAlambre = await _unitOfWork.DetAlambrePrdPanelesCovintecRepository.GetByIdAsync(dto.Id);

            if (detAlambre!=null)
            {
                detAlambre.Id = dto.Id;
                detAlambre.NumeroAlambre = dto.NumeroAlambre;
                detAlambre.PesoAlambre = dto.PesoAlambre;
               
                detAlambre.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                detAlambre.FechaActualizacion = DateTime.Now;

                _unitOfWork.DetAlambrePrdPanelesCovintecRepository.Update(detAlambre);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PrdPanelesCovintecReporteDTO>> GetAllPanelProduccionWithDetailsAsync(DateTime start, DateTime end)
        {
            var panelsProduccion = await _unitOfWork.ReportesDapperRepository.GetAllPanelProduccionWithDetailsAsync(start,end);

            return panelsProduccion;
        }

        public async Task<bool> AprovePrdPanelCovintecByIdAsync(int id, string userId)
        {
             var prdPanelesCovintec =await _unitOfWork.PrdPanelesCovintecRepository.GetByIdAsync(id);
            if (prdPanelesCovintec == null) return await Task.FromResult(false);
            if (prdPanelesCovintec.AprobadoGerencia != true)
                {
                if (prdPanelesCovintec.AprobadoGerencia)
                {
                    prdPanelesCovintec.AprobadoGerencia = false;
                }
                else
                {
                    prdPanelesCovintec.AprobadoGerencia = true;
                }
                prdPanelesCovintec.IdAprobadoGerencia = userId;
                prdPanelesCovintec.FechaActualizacion = DateTime.Now;
                prdPanelesCovintec.IdUsuarioActualizacion = userId;
                _unitOfWork.PrdPanelesCovintecRepository.Update(prdPanelesCovintec);
                await _unitOfWork.SaveChangesAsync();
              
            }
            return await Task.FromResult(prdPanelesCovintec.AprobadoGerencia);
        }

        public async Task<bool> ValidatePrdPanelCovintecByIdAsync(int id, string userId)
        {
            var prdPanelesCovintec = await _unitOfWork.PrdPanelesCovintecRepository.GetByIdAsync(id);
            if (prdPanelesCovintec == null) return await Task.FromResult(false);
            if (prdPanelesCovintec.AprobadoSupervisor != true)
            {
                if (prdPanelesCovintec.AprobadoSupervisor)
                {
                    prdPanelesCovintec.AprobadoSupervisor = false;
                }
                else
                {
                    prdPanelesCovintec.AprobadoSupervisor = true;
                }
                prdPanelesCovintec.IdAprobadoSupervisor = userId;
                prdPanelesCovintec.FechaActualizacion = DateTime.Now;
                prdPanelesCovintec.IdUsuarioActualizacion = userId;
                _unitOfWork.PrdPanelesCovintecRepository.Update(prdPanelesCovintec);
                await _unitOfWork.SaveChangesAsync();
            }

            return await Task.FromResult(prdPanelesCovintec.AprobadoSupervisor);
        }
    }
  
   
}
