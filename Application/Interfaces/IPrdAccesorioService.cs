using Application.DTOs;
using Infrastructure.DTO;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPrdAccesorioService
    {
        Task<IEnumerable<ShowPrdAccesorioDto>> GetAllAsync();
        Task<PrdAccesorioDto> GetByIdAsync(int id);
        Task<CrearPrdAccesorioDto> GetCreateDataAsync();
        Task CreateAsync(PrdAccesorioDto dto);
        Task UpdateAsync(PrdAccesorioDto dto);
        Task<bool> UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId);
        Task UpdateDetPrdAsync(DetPrdAccesorioDto dto);
        Task<bool> ValidatePrdAccesorioByIdAsync(int id, string userId);
        Task<bool> AprovePrdAccesorioByIdAsync(int id, string userId);
        Task<IEnumerable<PrdAccesorioReporteDTO>> GetAllWithDetailsAsync(DateTime start, DateTime end);
        Task<IEnumerable<CatalogoAccesorio>> GetCatalogoAccesoriosByTipoAsync(int idTipo);
    }
}