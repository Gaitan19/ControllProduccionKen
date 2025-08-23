using Application.DTOs;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPrdCortePService
    {
        Task<IEnumerable<ShowPrdCortePDto>> GetAllAsync();
        Task<PrdCortePDTO> GetByIdAsync(int id);
        Task CreateAsync(PrdCortePDTO dto);
        Task UpdateAsync(PrdCortePDTO dto);
        Task UpdateDetPrd(DetPrdCortePDTO dto);
        Task DeleteDetPrd(DetPrdCortePDTO dto);
        Task<CrearPrdCortePDTO> GetCreateData();
        Task<bool> ValidatePrdCortePByIdAsync(int id, string userId);
        Task<bool> AprovePrdCortePByIdAsync(int id, string userId);
        Task<IEnumerable<PrdCortePReporteDTO>> GetAllPrdCortePWithDetailsAsync(DateTime start, DateTime end);
    }
}