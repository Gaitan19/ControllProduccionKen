using Application.DTOs;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPrdPanelesCovintecService
    {
        Task<IEnumerable<ShowPrdPanelesCovintecDto>> GetAllAsync();
        Task<PrdPanelesCovintecDto> GetByIdAsync(int id);
        Task CreateAsync(PrdPanelesCovintecDto dto);
        Task UpdateAsync(PrdPanelesCovintecDto dto);
        Task UpdateDetPrd(DetPrdPanelesCovintecDTO dto);
        Task UpdateDetAlambrePrd(DetAlambrePrdPanelesCovintecDTO dto);
        Task DeleteDetPrd(DetPrdPanelesCovintecDTO dto);
        Task <CrearPrdPanelesCovintecDto> GetCreateData();
        Task<bool> ValidateDetAlambrePrdPanelesCovintecByIdAsync(int id, string userId);
        Task<bool> ValidateDetPrdPanelesCovintecByIdAsync(int id, string userId);
        Task<bool> AproveDetAlambrePrdPanelesCovintecByIdAsync(int id, string userId);
        Task<bool> AproveDetPrdPanelesCovintecByIdAsync(int id, string userId);
        Task<bool> AprovePrdPanelCovintecByIdAsync(int id, string userId);
        Task<bool> ValidatePrdPanelCovintecByIdAsync(int id, string userId);
        Task<bool> UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId);
        Task<IEnumerable<PrdPanelesCovintecReporteDTO>> GetAllPanelProduccionWithDetailsAsync(DateTime start, DateTime end);
    }
}
