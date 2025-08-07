using Application.DTOs;
using Infrastructure.DTO;

namespace Application.Interfaces
{
    public interface IPrdCerchaCovintecService
    {
        Task<IEnumerable<ShowPrdPanelesCovintecDto>> GetAllAsync();
        Task<PrdCerchaCovintecDTO> GetByIdAsync(int id);
        Task CreateAsync(PrdCerchaCovintecDTO dto);
        Task UpdateAsync(PrdCerchaCovintecDTO dto);
        Task UpdateDetPrd(DetPrdCerchaCovintecDTO dto);
        Task UpdateDetAlambrePrd(DetAlambrePrdCerchaCovintecDTO dto);
        Task DeleteDetPrd(DetPrdCerchaCovintecDTO dto);
        Task<CrearPrdCerchaCovintecDto> GetCreateData();
        Task<bool> ValidateDetAlambrePrdCerchaCovintecByIdAsync(int id, string userId);
        Task<bool> ValidateDetPrdCerchaCovintecByIdAsync(int id, string userId);
        Task<bool> AproveDetAlambrePrdCerchaCovintecByIdAsync(int id, string userId);
        Task<bool> AproveDetPrdCerchaCovintecByIdAsync(int id, string userId);
        Task<bool> AprovePrdCerchaCovintecByIdAsync(int id, string userId);
        Task<bool> ValidatePrdCechaCovintecByIdAsync(int id, string userId);
        Task<IEnumerable<PrdCerchaCovintecReporteDTO>> GetAllCerchaProduccionReporteWithDetailsAsync(DateTime start, DateTime end);
    }
}
