using Application.DTOs;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPrdIlKwangService
    {
        Task<IEnumerable<ShowPrdIlKwangDTO>> GetAllAsync();
        Task<PrdIlKwangDTO> GetByIdAsync(int id);
        Task CreateAsync(PrdIlKwangDTO dto);
        Task UpdateAsync(PrdIlKwangDTO dto);
        Task<bool> UpdateDetPrd(DetPrdIlKwangDTO dto);
        Task DeleteDetPrd(DetPrdIlKwangDTO dto);
        Task<CrearPrdIlKwangDTO> GetCreateData();
        Task<bool> ValidatePrdIlKwangByIdAsync(int id, string userId);
        Task<bool> AprovePrdIlKwangByIdAsync(int id, string userId);
        Task<IEnumerable<PrdIlKwangReporteDTO>> GetAllPrdIlKwangWithDetailsAsync(DateTime start, DateTime end);
    }
}
