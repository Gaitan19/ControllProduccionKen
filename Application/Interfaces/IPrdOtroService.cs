using Application.DTOs;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPrdOtroService
    {
        Task<IEnumerable<ShowPrdOtroDto>> GetAllAsync();
        Task<PrdOtroDto> GetByIdAsync(int id);
        Task CreateAsync(PrdOtroDto dto);
        Task UpdateAsync(PrdOtroDto dto);
        Task UpdateDetPrd(DetPrdOtroDto dto);
        Task DeleteDetPrd(DetPrdOtroDto dto);
        Task<CrearPrdOtroDto> GetCreateData();
        Task<bool> ValidatePrdOtroByIdAsync(int id, string userId);
        Task<bool> AprovePrdOtroByIdAsync(int id, string userId);
        Task<IEnumerable<PrdOtroReporteDTO>> GetAllPrdOtroWithDetailsAsync(DateTime start, DateTime end);
    }
}