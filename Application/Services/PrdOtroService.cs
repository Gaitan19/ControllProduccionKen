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
    public class PrdOtroService : IPrdOtroService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public PrdOtroService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
        }

        public async Task CreateAsync(PrdOtroDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var prdOtro = new PrdOtro
                {
                    Fecha = dto.Fecha,
                    IdUsuarioCreacion = dto.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    IdTipoReporte = 8, // Default tipo reporte for otro
                    IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>()),
                    AprobadoSupervisor = false,
                    AprobadoGerencia = false,
                    TiempoParo = dto.TiempoParo
                };

                await _unitOfWork.PrdOtroRepository.AddAsync(prdOtro);
                await _unitOfWork.SaveChangesAsync();
                var idPrdOtro = prdOtro.Id;

                if (dto.DetPrdOtros != null && dto.DetPrdOtros.Any())
                {
                    var detPrdOtros = dto.DetPrdOtros.Select(detalle => new DetPrdOtro
                    {
                        PrdOtroId = idPrdOtro,
                        Actividad = detalle.Actividad,
                        DescripcionProducto = detalle.DescripcionProducto,
                        IdTipoFabricacion = detalle.IdTipoFabricacion,
                        NumeroPedido = detalle.NumeroPedido,
                        Nota = detalle.Nota,
                        Merma = detalle.Merma,
                        Comentario = detalle.Comentario,
                        HoraInicio = detalle.HoraInicio,
                        HoraFin = detalle.HoraFin,
                        Cantidad = detalle.Cantidad,
                        UnidadMedida = detalle.UnidadMedida,
                        IdUsuarioCreacion = dto.IdUsuarioCreacion,
                        FechaCreacion = prdOtro.FechaCreacion
                    });

                    await _unitOfWork.DetPrdOtroRepository.BulkInsertAsync(detPrdOtros);
                }

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ShowPrdOtroDto>> GetAllAsync()
        {
            var productions = await _unitOfWork.PrdOtroRepository.GetAllAsync(
                x => x.DetPrdOtros);

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
                return new ShowPrdOtroDto
                {
                    Id = p.Id,
                    IdTipoReporte = p.IdTipoReporte,
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

        public async Task<PrdOtroDto> GetByIdAsync(int id)
        {
            var prd = await _unitOfWork.PrdOtroRepository
                .GetByIdIncludeAsync(x => x.Id == id,
                    x => x.DetPrdOtros);
            if (prd == null) return null;

            var userIds = prd.IdUsuarios.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            return new PrdOtroDto
            {
                Id = prd.Id,
                IdUsuarios = userIds,
                Fecha = prd.Fecha,
                IdUsuarioCreacion = prd.IdUsuarioCreacion,
                FechaCreacion = prd.FechaCreacion,
                IdUsuarioActualizacion = prd.IdUsuarioActualizacion,
                FechaActualizacion = prd.FechaActualizacion,
                IdTipoReporte = prd.IdTipoReporte,
                TiempoParo = prd.TiempoParo,
                AprobadoSupervisor = prd.AprobadoSupervisor,
                AprobadoGerencia = prd.AprobadoGerencia,
                IdAprobadoSupervisor = prd.IdAprobadoSupervisor,
                IdAprobadoGerencia = prd.IdAprobadoGerencia,
                NotaSupervisor = prd.NotaSupervisor,
                DetPrdOtros = prd.DetPrdOtros.Select(entity => new DetPrdOtroDTO
                {
                    Id = entity.Id,
                    PrdOtroId = entity.PrdOtroId,
                    Actividad = entity.Actividad,
                    DescripcionProducto = entity.DescripcionProducto,
                    IdTipoFabricacion = entity.IdTipoFabricacion,
                    NumeroPedido = entity.NumeroPedido,
                    Nota = entity.Nota,
                    Merma = entity.Merma,
                    Comentario = entity.Comentario,
                    HoraInicio = entity.HoraInicio,
                    HoraFin = entity.HoraFin,
                    Cantidad = entity.Cantidad,
                    UnidadMedida = entity.UnidadMedida,
                    IdUsuarioCreacion = entity.IdUsuarioCreacion,
                    FechaCreacion = entity.FechaCreacion,
                    IdUsuarioActualizacion = entity.IdUsuarioActualizacion,
                    FechaActualizacion = entity.FechaActualizacion
                }).ToList()
            };
        }

        public async Task<CrearPrdOtroDto> GetCreateData()
        {
            var dto = new CrearPrdOtroDto
            {
                CatTipoFabricacion = _mapper.Map<List<TipoFabricacionDto>>(
                    (await _unitOfWork.TipoFabricacionRepository.FindAsync(x => x.Activo == true)).ToList())
            };

            return dto;
        }

        public async Task<bool> ValidatePrdOtroByIdAsync(int id, string userId)
        {
            var prdOtro = await _unitOfWork.PrdOtroRepository.GetByIdAsync(id);
            if (prdOtro == null)
            {
                return false;
            }

            if (prdOtro.AprobadoSupervisor)
            {
                return false;
            }

            prdOtro.AprobadoSupervisor = true;
            prdOtro.IdAprobadoSupervisor = userId;
            prdOtro.IdUsuarioActualizacion = userId;
            prdOtro.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdOtroRepository.Update(prdOtro);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AprovePrdOtroByIdAsync(int id, string userId)
        {
            var prdOtro = await _unitOfWork.PrdOtroRepository.GetByIdAsync(id);
            if (prdOtro == null)
            {
                return false;
            }

            if (prdOtro.AprobadoGerencia)
            {
                return false;
            }

            prdOtro.AprobadoGerencia = true;
            prdOtro.IdAprobadoGerencia = userId;
            prdOtro.IdUsuarioActualizacion = userId;
            prdOtro.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdOtroRepository.Update(prdOtro);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId)
        {
            var prd = await _unitOfWork.PrdOtroRepository.GetByIdAsync(id);
            if (prd == null)
            {
                return false;
            }

            prd.NotaSupervisor = notaSupervisor;
            prd.IdUsuarioActualizacion = userId;
            prd.FechaActualizacion = DateTime.Now;

            _unitOfWork.PrdOtroRepository.Update(prd);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAsync(PrdOtroDto dto)
        {
            var prd = await _unitOfWork.PrdOtroRepository.GetByIdAsync(dto.Id);
            if (prd != null)
            {
                prd.Fecha = dto.Fecha;
                prd.IdUsuarios = string.Join(",", dto.IdUsuarios ?? new List<string>());
                prd.TiempoParo = dto.TiempoParo;
                prd.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                prd.FechaActualizacion = DateTime.Now;
                _unitOfWork.PrdOtroRepository.Update(prd);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetPrd(DetPrdOtroDTO dto)
        {
            var det = await _unitOfWork.DetPrdOtroRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                det.Actividad = dto.Actividad;
                det.DescripcionProducto = dto.DescripcionProducto;
                det.IdTipoFabricacion = dto.IdTipoFabricacion;
                det.NumeroPedido = dto.NumeroPedido;
                det.Nota = dto.Nota;
                det.Merma = dto.Merma;
                det.Comentario = dto.Comentario;
                det.HoraInicio = dto.HoraInicio;
                det.HoraFin = dto.HoraFin;
                det.Cantidad = dto.Cantidad;
                det.UnidadMedida = dto.UnidadMedida;
                det.IdUsuarioActualizacion = dto.IdUsuarioActualizacion;
                det.FechaActualizacion = DateTime.Now;
                _unitOfWork.DetPrdOtroRepository.Update(det);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteDetPrd(DetPrdOtroDTO dto)
        {
            var det = await _unitOfWork.DetPrdOtroRepository.GetByIdAsync(dto.Id);
            if (det != null)
            {
                _unitOfWork.DetPrdOtroRepository.Remove(det);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<PrdOtroReporteDTO>> GetAllPrdOtroWithDetailsAsync(DateTime start, DateTime end)
        {
            return _unitOfWork.ReportesDapperRepository.GetAllPrdOtroWithDetailsAsync(start, end);
        }
    }
}