using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IReportesDapperRepository
    {
        Task<IEnumerable<PrdPanelesCovintecReporteDTO>> GetAllPanelProduccionWithDetailsAsync(DateTime start, DateTime end);
        Task<IEnumerable<PrdCerchaCovintecReporteDTO>> GetAllCerchaProduccionWithDetailsAsync(DateTime start, DateTime end);
        Task<IEnumerable<PrdMallasCovintecReporteDTO>> GetAllMallaProduccionWithDetailsAsync(DateTime start, DateTime end);
        Task<IEnumerable<PrdIlKwangReporteDTO>> GetAllPrdIlKwangWithDetailsAsync(DateTime start, DateTime end);
        Task<IEnumerable<PrdNeveraReporteDTO>> GetAllPrdNeveraWithDetailsAsync(DateTime start, DateTime end);
        Task<IEnumerable<PrdOtroReporteDTO>> GetAllPrdOtroWithDetailsAsync(DateTime start, DateTime end);
        Task<IEnumerable<PrdBloquesReporteDTO>> GetAllPrdBloqueWithDetailsAsync(DateTime start, DateTime end);
        Task<IEnumerable<PrdCorteTReporteDTO>> GetAllPrdCorteTWithDetailsAsync(DateTime start, DateTime end);
    }
}
