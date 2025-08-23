using Application.DTOs;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPrdPreExpansionService
    {
        Task<IEnumerable<ShowPrdPreExpansionDto>> GetAllAsync();
        Task<PrdpreExpansionDto> GetByIdAsync(int id);
        Task<int> CreateAsync(PrdpreExpansionDto prdPreExpansion);
        Task UpdateAsync(PrdpreExpansionDto prdPreExpansion);
        Task UpdateDetPrd(DetPrdpreExpansionDTO dto);
        Task DeleteAsync(int id);
        Task<CrearPrdPreExpansionDto> GetCatalogosAsync();
        Task<bool> ApprobarSupervisorAsync(int id, string userId);
        Task<bool> AprobarGerenciaAsync(int id, string userId);
        Task<IEnumerable<PrdPreExpansionReporteDTO>> GetAllPrdPreExpansionWithDetailsAsync(DateTime start, DateTime end);
    }
}
