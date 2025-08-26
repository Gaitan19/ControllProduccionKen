using Application.DTOs;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPrdCorteTService
    {
        Task<IEnumerable<ShowPrdCorteTDto>> GetAllAsync();
        Task<PrdCorteTDTO> GetByIdAsync(int id);
        Task CreateAsync(PrdCorteTDTO dto);
        Task UpdateAsync(PrdCorteTDTO dto);
        Task UpdateDetPrd(DetPrdCorteTDTO dto);
        Task DeleteDetPrd(DetPrdCorteTDTO dto);
        Task<CrearPrdCorteTDTO> GetCreateData();
        Task<bool> ValidatePrdCorteTByIdAsync(int id, string userId);
        Task<bool> AprovePrdCorteTByIdAsync(int id, string userId);
        Task UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId);
        Task<IEnumerable<PrdCorteTReporteDTO>> GetAllPrdCorteTWithDetailsAsync(DateTime start, DateTime end);
    }
}
