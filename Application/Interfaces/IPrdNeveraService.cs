using Application.DTOs;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPrdNeveraService
    {
        Task<IEnumerable<ShowPrdNeveraDto>> GetAllAsync();
        Task<PrdNeveraDto> GetByIdAsync(int id);
        Task CreateAsync(PrdNeveraDto dto);
        Task UpdateAsync(PrdNeveraDto dto);
        Task UpdateDetPrd(DetPrdNeveraDTO dto);
     
        Task DeleteDetPrd(DetPrdNeveraDTO dto);
        Task<CrearPrdNeveraDto> GetCreateData();
        Task<bool> ValidatePrdNeveraByIdAsync(int id, string userId);
       
        Task<bool> AprovePrdNeveraByIdAsync(int id, string userId);

        Task<bool> UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId);
       
      
        Task<IEnumerable<PrdNeveraReporteDTO>> GetAllPrdNeveraWithDetailsAsync(DateTime start, DateTime end);
    }
}
