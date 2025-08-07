using Application.DTOs;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPrdBloquesService
    {
        Task<IEnumerable<ShowPrdBloqueDto>> GetAllAsync();
        Task<PrdBloqueDTO> GetByIdAsync(int id);
        Task CreateAsync(PrdBloqueDTO dto);
        Task UpdateAsync(PrdBloqueDTO dto);
        Task UpdateDetPrd(DetPrdBloqueDTO dto);
        Task DeleteDetPrd(DetPrdBloqueDTO dto);
        Task UpdateSubDetPrd(SubDetPrdBloqueDTO dto);
        Task DeleteSubDetPrd(SubDetPrdBloqueDTO dto);
        Task<CrearPrdBloqueDto> GetCreateData();
        Task<bool> ValidatePrdBloqueByIdAsync(int id, string userId);
        Task<bool> AprovePrdBloqueByIdAsync(int id, string userId);
        Task<IEnumerable<PrdBloquesReporteDTO>> GetAllPrdBloqueWithDetailsAsync(DateTime start, DateTime end);
    }
}