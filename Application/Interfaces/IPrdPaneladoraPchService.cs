using Application.DTOs;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPrdPaneladoraPchService
    {
        Task<IEnumerable<ShowPrdPaneladoraPchDTO>> GetAllAsync();
        Task<PrdPaneladoraPchDTO> GetByIdAsync(int id);
        Task CreateAsync(PrdPaneladoraPchDTO dto);
        Task UpdateAsync(PrdPaneladoraPchDTO dto);
        Task UpdateDetPrd(DetPrdPaneladoraPchDTO dto);
        Task DeleteDetPrd(DetPrdPaneladoraPchDTO dto);
        Task<CrearPrdPaneladoraPchDTO> GetCreateData();
        Task<bool> ValidatePrdPaneladoraPchByIdAsync(int id, string userId);
        Task<bool> AprovePrdPaneladoraPchByIdAsync(int id, string userId);
        Task<bool> UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId);
        Task<IEnumerable<PrdPaneladoraPchReporteDTO>> GetAllPrdPaneladoraPchWithDetailsAsync(DateTime start, DateTime end);
    }
}