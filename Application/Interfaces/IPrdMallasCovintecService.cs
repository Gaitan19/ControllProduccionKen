using Application.DTOs;
using Infrastructure.DTO;

namespace Application.Interfaces
{
    public interface IPrdMallasCovintecService
    {
        Task<IEnumerable<ShowPrdMallasCovintecDto>> GetAllAsync();
        Task<PrdMallaCovintecDto> GetByIdAsync(int id);
        Task CreateAsync(PrdMallaCovintecDto dto);
        Task UpdateAsync(PrdMallaCovintecDto dto);
        Task UpdateDetPrd(DetPrdMallaCovintecDTO dto);
        Task UpdateDetAlambrePrd(DetAlambrePrdMallaCovintecDTO dto);
        Task DeleteDetPrd(DetPrdMallaCovintecDTO dto);
        Task<CrearPrdMallaCovintecDto> GetCreateData();
        Task<bool> ValidateDetAlambrePrdMallaCovintecByIdAsync(int id, string userId);
        Task<bool> ValidateDetPrdMallaCovintecByIdAsync(int id, string userId);
        Task<bool> AproveDetAlambrePrdMallaCovintecByIdAsync(int id, string userId);
        Task<bool> AproveDetPrdMallaCovintecByIdAsync(int id, string userId);
        Task<bool> AprovePrdMallaCovintecByIdAsync(int id, string userId);
        Task<bool> ValidatePrdMallaCovintecByIdAsync(int id, string userId);
        Task<bool> UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId);
        Task<IEnumerable<PrdMallasCovintecReporteDTO>> GetAllMallasProduccionWithDetailsAsync(DateTime start, DateTime end);
    }
}
