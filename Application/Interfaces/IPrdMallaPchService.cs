using Application.DTOs;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPrdMallaPchService
    {
        Task<IEnumerable<ShowPrdMallaPchDTO>> GetAllAsync();
        Task<PrdMallaPchDTO> GetByIdAsync(int id);
        Task CreateAsync(PrdMallaPchDTO dto);
        Task UpdateAsync(PrdMallaPchDTO dto);

        // Detalles (sobrecarga de metodo, siguiendo el estilo de Update)
        Task UpdateDetPrd(DetPrdPchMaquinaADTO dto);
        Task UpdateDetPrd(DetPrdPchMaquinaBDTO dto);
        Task UpdateDetPrd(DetPrdPchMaquinaCDTO dto);

        // Delete methods following established patterns
        Task DeleteDetPrd(DetPrdPchMaquinaADTO dto);
        Task DeleteDetPrd(DetPrdPchMaquinaBDTO dto);
        Task DeleteDetPrd(DetPrdPchMaquinaCDTO dto);

        Task<CrearPrdMallaPchDTO> GetCreateData();

        Task<bool> ValidatePrdMallaPchByIdAsync(int id, string userId);
        Task<bool> AprovePrdMallaPchByIdAsync(int id, string userId);
        Task<bool> UpdateNotaSupervisorAsync(int id, string notaSupervisor, string userId);
        Task<IEnumerable<PrdMallaPCHReporteDTO>> GetAllPrdMallaPCHWithDetailsAsync(DateTime start, DateTime end);
    }
}
