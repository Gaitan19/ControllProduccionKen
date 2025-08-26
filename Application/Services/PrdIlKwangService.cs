using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.DTO;
using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
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
    public class PrdIlKwangService:IPrdIlKwangService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
       

        public PrdIlKwangService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<IdentityUser> userManager
            )
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
           
        }

        public async Task<bool> AprovePrdIlKwangByIdAsync(int id, string userId)
        {
            var prdIlKwang = await _unitOfWork.PrdIlKwangRepository.GetByIdAsync(id);
            if (prdIlKwang == null)
            {
                return false;
            }

            if (prdIlKwang.AprobadoGerencia)
            {
                return false;
            }

            prdIlKwang.AprobadoGerencia = true;
            prdIlKwang.IdAprobadoGerencia = userId;
            _unitOfWork.PrdIlKwangRepository.Update(prdIlKwang);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task CreateAsync(PrdIlKwangDTO dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var tieneDetalles = dto.DetPrdIlKwangs != null && dto.DetPrdIlKwangs.Any();

                // Cálculos previos
                decimal? totalMetros = 0, metrosDefecto = 0, metrosMerma = 0, metrosAdicionales = 0;
                decimal? porcentajeMerma = 0, porcentajeDefecto = 0;

                if (tieneDetalles)
                {
                    totalMetros = dto.DetPrdIlKwangs.Sum(x => x.MetrosCuadrados);
                    metrosDefecto = dto.DetPrdIlKwangs.Where(x => x.IdStatus == 2).Sum(x => x.MetrosCuadrados);
                    metrosMerma = dto.DetPrdIlKwangs.Where(x => x.IdTipo == 6).Sum(x => x.MetrosCuadrados);
                    metrosAdicionales = dto.DetPrdIlKwangs.Where(x => x.IdTipo == 2).Sum(x => x.MetrosCuadrados);

                    if (totalMetros > 0)
                    {
                        porcentajeMerma = 100*(metrosMerma / totalMetros);
                        porcentajeDefecto = 100*(metrosDefecto / totalMetros);
                    }
                }

                var prdIlKwans = new PrdIlKwang
                {
                    IdMaquina = dto.IdMaquina,
                    IdTipoReporte = 6,
                    IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>()),
                    HoraInicio = dto.HoraInicio,
                    HoraFin = dto.HoraFin,
                    Fecha = dto.Fecha,
                    TiempoParo = dto.TiempoParo,
                    IdArticulo = string.Join(",", dto.IdArticulo ?? new List<string>()),
                    IdTipoFabricacion = dto.IdTipoFabricacion,
                    Cliente = dto.Cliente,
                    NumeroPedido = dto.NumeroPedido,
                    VelocidadMaquina = dto.VelocidadMaquina,
                    IdUbicacionBobinaA = dto.IdUbicacionBobinaA,
                    IdAnchoBobinaA = dto.IdAnchoBobinaA,
                    FabricanteBobinaA = dto.FabricanteBobinaA,
                    CodigoBobinaA = dto.CodigoBobinaA,
                    CalibreMmA = dto.CalibreMmA,
                    IdColorBobinaA = dto.IdColorBobinaA,
                    AnchoMmA = dto.AnchoMmA,
                    PesoInicialKgA = dto.PesoInicialKgA,
                    PesoFinalKgA = dto.PesoFinalKgA,
                    MetrosInicialA = dto.MetrosInicialA,
                    MetrosFinalA = dto.MetrosFinalA,
                    EspesorInicialCmA = dto.EspesorInicialCmA,
                    EspesorFinalCmA = dto.EspesorFinalCmA,
                    ConsumoBobinaKgA = dto.PesoInicialKgA - dto.PesoFinalKgA,
                    IdUbicacionBobinaB = dto.IdUbicacionBobinaB,
                    IdAnchoBobinaB = dto.IdAnchoBobinaB,
                    FabricanteBobinaB = dto.FabricanteBobinaB,
                    CodigoBobinaB = dto.CodigoBobinaB,
                    CalibreMmB = dto.CalibreMmB,
                    IdColorBobinaB = dto.IdColorBobinaB,
                    AnchoMmB = dto.AnchoMmB,
                    PesoInicialKgB = dto.PesoInicialKgB,
                    PesoFinalKgB = dto.PesoFinalKgB,
                    MetrosInicialB = dto.MetrosInicialB,
                    MetrosFinalB = dto.MetrosFinalB,
                    EspesorInicialCmB = dto.EspesorInicialCmB,
                    EspesorFinalCmB = dto.EspesorFinalCmB,
                    ConsumoBobinaKgB = dto.PesoInicialKgB - dto.PesoFinalKgB,
                    PesoInicialA = dto.PesoInicialA,
                    CantidadUtilizadaA = dto.CantidadUtilizadaA,
                    PesoFinalA = dto.PesoFinalA,
                    VelocidadSuperiorA = dto.VelocidadSuperiorA,
                    VelocidadInferiorA = dto.VelocidadInferiorA,
                    LoteA = dto.LoteA,
                    VencimientoA = dto.VencimientoA,
                    PesoInicialB = dto.PesoInicialB,
                    CantidadUtilizadaB = dto.CantidadUtilizadaB,
                    PesoFinalB = dto.PesoFinalB,
                    VelocidadSuperiorB = dto.VelocidadSuperiorB,
                    VelocidadInferiorB = dto.VelocidadInferiorB,
                    LoteB = dto.LoteB,
                    VencimientoB = dto.VencimientoB,

                    TotalProduccion = totalMetros,
                    MetrosConDefecto = metrosDefecto,
                    MermaM = metrosMerma,
                    MetrosAdicionales = metrosAdicionales,
                    PorcentajeMerma = porcentajeMerma,
                    PorcentajeDefecto = porcentajeDefecto,

                    Observaciones = dto.Observaciones,
                    CantidadArranques = dto.CantidadArranques,
                    IdUsuarioCreacion = dto.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    AprobadoGerencia = false,
                    AprobadoSupervisor = false,
                    MetroInicialIsocianato = dto.MetroInicialIsocianato,
                    MetroInicialPoliol = dto.MetroInicialPoliol
                };

                await _unitOfWork.PrdIlKwangRepository.AddAsync(prdIlKwans);
                await _unitOfWork.SaveChangesAsync();
                var prdIlKwangId = prdIlKwans.Id;

                if (tieneDetalles)
                {
                    
                    var detPrdIlKwangs = dto.DetPrdIlKwangs.Select(det => new DetPrdIlKwang
                    {
                        PrdIlKwangId = prdIlKwangId,
                        Posicion = det.Posicion,
                        IdEspesor = det.IdEspesor,
                        Cantidad = det.Cantidad,
                        Medida = det.Medida,
                        MetrosCuadrados = det.MetrosCuadrados,
                        IdStatus = det.IdStatus,
                        IdTipo = det.IdTipo,
                        IdUsuarioCreacion = dto.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    });

                    await _unitOfWork.DetPrdIlKwangRepository.BulkInsertAsync(detPrdIlKwangs);
                }

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw;
            }
        }


        public Task DeleteDetPrd(DetPrdIlKwangDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ShowPrdIlKwangDTO>> GetAllAsync()
        {
            var maquinas = await _unitOfWork.CatMaquinaRepository.GetAllAsync();
            var productions = await _unitOfWork.PrdIlKwangRepository.GetAllAsync();

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
                return new ShowPrdIlKwangDTO
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

        public async Task<PrdIlKwangDTO?> GetByIdAsync(int id)
        {
            // Obtener la entidad principal
            var prd = await _unitOfWork.PrdIlKwangRepository.GetByIdAsync(id);
            if (prd == null) return null;

            // Obtener los detalles relacionados
            var detalles = await _unitOfWork.DetPrdIlKwangRepository.FindAsync(x => x.PrdIlKwangId == prd.Id);

            // Convertir cadenas separadas por coma a listas
            var userIds = prd.IdUsuarios?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>();
            var articuloIds = prd.IdArticulo?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>();

            // Mapeo manual en lugar de usar AutoMapper
            var dto = new PrdIlKwangDTO
            {
                Id = prd.Id,
                IdTipoReporte = prd.IdTipoReporte,
                IdUsuarios = userIds,
                IdMaquina = prd.IdMaquina,
                HoraInicio = prd.HoraInicio,
                HoraFin = prd.HoraFin,
                Fecha = prd.Fecha,
                TiempoParo = prd.TiempoParo,
                IdArticulo = articuloIds,
                IdTipoFabricacion = prd.IdTipoFabricacion,
                Cliente = prd.Cliente,
                NumeroPedido = prd.NumeroPedido,
                VelocidadMaquina = prd.VelocidadMaquina,
                IdUbicacionBobinaA = prd.IdUbicacionBobinaA,
                IdAnchoBobinaA = prd.IdAnchoBobinaA,
                FabricanteBobinaA = prd.FabricanteBobinaA,
                CodigoBobinaA = prd.CodigoBobinaA,
                CalibreMmA = prd.CalibreMmA,
                IdColorBobinaA = prd.IdColorBobinaA,
                AnchoMmA = prd.AnchoMmA,
                PesoInicialKgA = prd.PesoInicialKgA,
                PesoFinalKgA = prd.PesoFinalKgA,
                MetrosInicialA = prd.MetrosInicialA,
                MetrosFinalA = prd.MetrosFinalA,
                EspesorInicialCmA = prd.EspesorInicialCmA,
                EspesorFinalCmA = prd.EspesorFinalCmA,
                ConsumoBobinaKgA = prd.ConsumoBobinaKgA,
                IdUbicacionBobinaB = prd.IdUbicacionBobinaB,
                IdAnchoBobinaB = prd.IdAnchoBobinaB,
                FabricanteBobinaB = prd.FabricanteBobinaB,
                CodigoBobinaB = prd.CodigoBobinaB,
                CalibreMmB = prd.CalibreMmB,
                IdColorBobinaB = prd.IdColorBobinaB,
                AnchoMmB = prd.AnchoMmB,
                PesoInicialKgB = prd.PesoInicialKgB,
                PesoFinalKgB = prd.PesoFinalKgB,
                MetrosInicialB = prd.MetrosInicialB,
                MetrosFinalB = prd.MetrosFinalB,
                EspesorInicialCmB = prd.EspesorInicialCmB,
                EspesorFinalCmB = prd.EspesorFinalCmB,
                ConsumoBobinaKgB = prd.ConsumoBobinaKgB,
                PesoInicialA = prd.PesoInicialA,
                CantidadUtilizadaA = prd.CantidadUtilizadaA,
                PesoFinalA = prd.PesoFinalA,
                VelocidadSuperiorA = prd.VelocidadSuperiorA,
                VelocidadInferiorA = prd.VelocidadInferiorA,
                LoteA = prd.LoteA,
                VencimientoA = prd.VencimientoA,
                PesoInicialB = prd.PesoInicialB,
                CantidadUtilizadaB = prd.CantidadUtilizadaB,
                PesoFinalB = prd.PesoFinalB,
                VelocidadSuperiorB = prd.VelocidadSuperiorB,
                VelocidadInferiorB = prd.VelocidadInferiorB,
                LoteB = prd.LoteB,
                VencimientoB = prd.VencimientoB,
                TotalProduccion = prd.TotalProduccion,
                MetrosConDefecto = prd.MetrosConDefecto,
                MermaM = prd.MermaM,
                MetrosAdicionales = prd.MetrosAdicionales,
                PorcentajeMerma = prd.PorcentajeMerma,
                PorcentajeDefecto = prd.PorcentajeDefecto,
                Observaciones = prd.Observaciones,
                CantidadArranques = prd.CantidadArranques,
                IdUsuarioCreacion = prd.IdUsuarioCreacion,
                FechaCreacion = prd.FechaCreacion,
                IdUsuarioActualizacion = prd.IdUsuarioActualizacion,
                FechaActualizacion = prd.FechaActualizacion,
                AprobadoSupervisor = prd.AprobadoSupervisor,
                AprobadoGerencia = prd.AprobadoGerencia,
                IdAprobadoSupervisor = prd.IdAprobadoSupervisor,
                IdAprobadoGerencia = prd.IdAprobadoGerencia,
                NotaSupervisor = prd.NotaSupervisor,
                MetroInicialIsocianato = prd.MetroInicialIsocianato,
                MetroInicialPoliol = prd.MetroInicialPoliol
            };

            // Mapeo manual de los detalles
            if (detalles != null)
            {
                dto.DetPrdIlKwangs = detalles.Select(x => new DetPrdIlKwangDTO
                {
                    Id = x.Id,
                    PrdIlKwangId = x.PrdIlKwangId,
                    Posicion = x.Posicion,
                    IdEspesor = x.IdEspesor,
                    Cantidad = x.Cantidad,
                    Medida = x.Medida,
                    MetrosCuadrados = x.MetrosCuadrados,
                    IdStatus = x.IdStatus,
                    IdTipo = x.IdTipo,
                    IdUsuarioCreacion = x.IdUsuarioCreacion,
                    FechaCreacion = x.FechaCreacion,
                    IdUsuarioActualizacion = x.IdUsuarioActualizacion,
                    FechaActualizacion = x.FechaActualizacion
                }).ToList();
            }
            else
            {
                dto.DetPrdIlKwangs = new List<DetPrdIlKwangDTO>();
            }


            return dto;
        }

        public async Task<CrearPrdIlKwangDTO> GetCreateData()
        {
            var catMaquina = await _unitOfWork.CatMaquinaRepository.FindAsync(x => x.Activo == true);
            var catTipoFabricacion = await _unitOfWork.TipoFabricacionRepository.FindAsync(x => x.Activo == true);
            var catArticulo = await _unitOfWork.CatProdTermoIsoPanelRepository.GetAllAsync();
            var catAchoBobina = await _unitOfWork.AnchoBobinaRepository.FindAsync(x => x.Activo == true);
            var catColoresBobina = await _unitOfWork.ColoresBobinaRepository.FindAsync(x => x.Activo == true);
            var catUbicacionBobina = await _unitOfWork.UbicacionBobinaRepository.FindAsync(x => x.Activo == true);
            var catEspesor = await _unitOfWork.CatEspesorRepository.FindAsync(x => x.Activo == true);
            var catStatus = await _unitOfWork.CatalogoStatusRepository.FindAsync(x => x.Activo == true);
            var catTipo = await _unitOfWork.CatalogoTipoRepository.FindAsync(x => x.Activo == true);

            var dto = new CrearPrdIlKwangDTO
            {
                CatMaquina = _mapper.Map<List<MaquinaDto>>(catMaquina),
                CatTipoFabricacion = _mapper.Map<List<TipoFabricacionDto>>(catTipoFabricacion),
                CatTermoIsoPanel = _mapper.Map<List<CatProdTermoIsoPanelDTO>>(catArticulo),
                CatAnchoBobina = _mapper.Map<List<AnchoBobinaDTO>>(catAchoBobina),
                CatColoresBobina = _mapper.Map<List<ColoresBobinaDTO>>(catColoresBobina),
                CatUbicacionBobina = _mapper.Map<List<UbicacionBobinaDTO>>(catUbicacionBobina),
                CatEspesor = _mapper.Map<List<CatEspesorDTO>>(catEspesor),
                CatStatus = _mapper.Map<List<CatalogoStatusDTO>>(catStatus),
                CatTipo = _mapper.Map<List<CatalogoTipoDTO>>(catTipo)
            };

            return dto;
        }

        public async Task UpdateAsync(PrdIlKwangDTO dto)
        {
            var prd = await _unitOfWork.PrdIlKwangRepository.GetByIdAsync(dto.Id);
            if (prd == null)
            {
                throw new KeyNotFoundException($"PrdIlKwang with ID {dto.Id} not found.");
            }

            //calculo de campos calculados segun los detalles
            var detalles = await _unitOfWork.DetPrdIlKwangRepository.FindAsync(x => x.PrdIlKwangId == dto.Id);
            var tieneDetalles = detalles != null && detalles.Any();

            // Cálculos previos
            decimal? totalMetros = 0, metrosDefecto = 0, metrosMerma = 0, metrosAdicionales = 0;
            decimal? porcentajeMerma = 0, porcentajeDefecto = 0;

            if (tieneDetalles)
            {
                totalMetros = detalles.Sum(x => x.MetrosCuadrados);
                metrosDefecto = detalles.Where(x => x.IdStatus == 2).Sum(x => x.MetrosCuadrados);
                metrosMerma = detalles.Where(x => x.IdTipo == 6).Sum(x => x.MetrosCuadrados);
                metrosAdicionales = detalles.Where(x => x.IdTipo == 2).Sum(x => x.MetrosCuadrados);

                if (totalMetros > 0)
                {
                    porcentajeMerma = 100 * (metrosMerma / totalMetros);
                    porcentajeDefecto = 100 * (metrosDefecto / totalMetros);
                }
            }

            // Update entity properties
            prd.IdMaquina = dto.IdMaquina;
            prd.IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>());
            prd.HoraInicio = dto.HoraInicio;
            prd.HoraFin = dto.HoraFin;
            prd.Fecha = dto.Fecha;
            prd.TiempoParo = dto.TiempoParo;
            prd.IdArticulo = string.Join(",", dto.IdArticulo ?? new List<string>());
            prd.IdTipoFabricacion = dto.IdTipoFabricacion;
            prd.Cliente = dto.Cliente;
            prd.NumeroPedido = dto.NumeroPedido;
            prd.VelocidadMaquina = dto.VelocidadMaquina;
            prd.IdUbicacionBobinaA = dto.IdUbicacionBobinaA;
            prd.IdAnchoBobinaA = dto.IdAnchoBobinaA;
            prd.FabricanteBobinaA = dto.FabricanteBobinaA;
            prd.CodigoBobinaA = dto.CodigoBobinaA;
            prd.CalibreMmA = dto.CalibreMmA;
            prd.IdColorBobinaA = dto.IdColorBobinaA;
            prd.AnchoMmA = dto.AnchoMmA;
            prd.PesoInicialKgA = dto.PesoInicialKgA;
            prd.PesoFinalKgA = dto.PesoFinalKgA;
            prd.MetrosInicialA = dto.MetrosInicialA;
            prd.MetrosFinalA = dto.MetrosFinalA;
            prd.EspesorInicialCmA = dto.EspesorInicialCmA;
            prd.EspesorFinalCmA = dto.EspesorFinalCmA;
            prd.ConsumoBobinaKgA = dto.PesoInicialKgA - dto.PesoFinalKgA;
            prd.IdUbicacionBobinaB = dto.IdUbicacionBobinaB;
            prd.IdAnchoBobinaB = dto.IdAnchoBobinaB;
            prd.FabricanteBobinaB = dto.FabricanteBobinaB;
            prd.CodigoBobinaB = dto.CodigoBobinaB;
            prd.CalibreMmB = dto.CalibreMmB;
            prd.IdColorBobinaB = dto.IdColorBobinaB;
            prd.AnchoMmB = dto.AnchoMmB;
            prd.PesoInicialKgB = dto.PesoInicialKgB;
            prd.PesoFinalKgB = dto.PesoFinalKgB;
            prd.MetrosInicialB = dto.MetrosInicialB;
            prd.MetrosFinalB = dto.MetrosFinalB;
            prd.EspesorInicialCmB = dto.EspesorInicialCmB;
            prd.EspesorFinalCmB = dto.EspesorFinalCmB;
            prd.ConsumoBobinaKgB = dto.PesoInicialKgB - dto.PesoFinalKgB;
            prd.PesoInicialA = dto.PesoInicialA;
            prd.CantidadUtilizadaA = dto.CantidadUtilizadaA;
            prd.PesoFinalA = dto.PesoFinalA;
            prd.VelocidadSuperiorA = dto.VelocidadSuperiorA;
            prd.VelocidadInferiorA = dto.VelocidadInferiorA;
            prd.LoteA = dto.LoteA;
            prd.VencimientoA = dto.VencimientoA;
            prd.PesoInicialB = dto.PesoInicialB;
            prd.CantidadUtilizadaB = dto.CantidadUtilizadaB;
            prd.PesoFinalB = dto.PesoFinalB;
            prd.VelocidadSuperiorB = dto.VelocidadSuperiorB;
            prd.VelocidadInferiorB = dto.VelocidadInferiorB;
            prd.LoteB = dto.LoteB;
            prd.VencimientoB = dto.VencimientoB;
            prd.Observaciones = dto.Observaciones;
            prd.NotaSupervisor = dto.NotaSupervisor;
            prd.CantidadArranques = dto.CantidadArranques;
            prd.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
            prd.FechaActualizacion = DateTime.Now;

            prd.TotalProduccion = totalMetros;
            prd.MetrosConDefecto = metrosDefecto;
            prd.MermaM = metrosMerma;
            prd.MetrosAdicionales = metrosAdicionales;
            prd.PorcentajeMerma = porcentajeMerma;
            prd.PorcentajeDefecto = porcentajeDefecto;
            prd.MetroInicialIsocianato = dto.MetroInicialIsocianato;
            prd.MetroInicialPoliol = dto.MetroInicialPoliol;

            _unitOfWork.PrdIlKwangRepository.Update(prd);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateDetPrd(DetPrdIlKwangDTO dto)
        {
            var det = await _unitOfWork.DetPrdIlKwangRepository.GetByIdAsync(dto.Id);
            if (det == null)
            {
                throw new KeyNotFoundException($"DetPrdIlKwang with ID {dto.Id} not found.");
            }

            det.IdEspesor = dto.IdEspesor;
            det.Cantidad = dto.Cantidad;
            det.Medida = dto.Medida;
            det.MetrosCuadrados = dto.Cantidad * dto.Medida;
            det.IdStatus = dto.IdStatus;
            det.IdTipo = dto.IdTipo;
            det.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
            det.FechaActualizacion = DateTime.Now;

            _unitOfWork.DetPrdIlKwangRepository.Update(det);
            await _unitOfWork.SaveChangesAsync();
            return true; 
        }

        public async Task<bool> ValidatePrdIlKwangByIdAsync(int id, string userId)
        {
            var prdIlKwang = await _unitOfWork.PrdIlKwangRepository.GetByIdAsync(id);
            if (prdIlKwang == null)
            {
                return false;
            }
           
            if (prdIlKwang.AprobadoSupervisor)
            {
                return false;
            }

            prdIlKwang.AprobadoSupervisor = true;
            prdIlKwang.IdAprobadoSupervisor = userId;
            _unitOfWork.PrdIlKwangRepository.Update(prdIlKwang);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> GuardarNotaSupervisorAsync(int id, string nota, string userId)
        {
            var prdIlKwang = await _unitOfWork.PrdIlKwangRepository.GetByIdAsync(id);
            if (prdIlKwang == null)
            {
                return false;
            }

            prdIlKwang.NotaSupervisor = nota;
            prdIlKwang.IdUsuarioActualizacion = userId;
            prdIlKwang.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdIlKwangRepository.Update(prdIlKwang);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PrdIlKwangReporteDTO>> GetAllPrdIlKwangWithDetailsAsync(DateTime start, DateTime end)
        {
            return await _unitOfWork.ReportesDapperRepository.GetAllPrdIlKwangWithDetailsAsync(start, end);
        }
    }
}
