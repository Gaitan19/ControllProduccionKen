using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        // Aquí se exponen las propiedades de los repositorios
        // Por ejemplo:
        // IProductoRepository Productos { get; }

        // Si tienes más repositorios específicos, los declaras aquí.
        // IGenericRepository<OtraEntidad> OtrasEntidades { get; }

        IGenericRepository<TipoFabricacion> TipoFabricacionRepository { get; }
        IGenericRepository<Maquina> CatMaquinaRepository { get; }
        IGenericRepository<CatalogoPanelesCovintec> CatalogoPanelesCovintecRepository { get; }
        IGenericRepository<PrdPanelesCovintec> PrdPanelesCovintecRepository { get; }
        IGenericRepository<CatalogosPermitidosPorReporte> catalogosPermitidosPorReporteRepository { get; }
        IGenericRepository<DetPrdPanelesCovintec> DetPrdPanelesCovintecRepository { get; }
        IGenericRepository<DetAlambrePrdPanelesCovintec> DetAlambrePrdPanelesCovintecRepository { get; }
        IGenericRepository<ColoresBobina> ColoresBobinaRepository { get; }
        IGenericRepository<AnchoBobina> AnchoBobinaRepository { get; }
        IGenericRepository<CatalogoCercha> CatalogoCerchaRepository { get; }

        IGenericRepository<CatalogoStatus> CatalogoStatusRepository { get; }

        IGenericRepository<CatalogoMallasCovintec> CatalogoMallasCovintecRepository { get; }

        IGenericRepository<CatalogoTipo> CatalogoTipoRepository { get; }

        IGenericRepository<LineaProduccion> LineaProduccionRepository { get; }

        IGenericRepository<UbicacionBobina> UbicacionBobinaRepository { get; }
        IGenericRepository<PrdCerchaCovintec> PrdCerchaCovintecRepository { get; }
        IGenericRepository<DetAlambrePrdCerchaCovintec> DetAlambrePrdCerchaCovintecRepository { get; }
        IGenericRepository<DetPrdCerchaCovintec> DetPrdCerchaCovintecRepository { get; }
        IGenericRepository<PrdMallaCovintec> PrdMallaCovintecRepository { get; }
        IGenericRepository<DetAlambrePrdMallaCovintec> DetAlambrePrdMallaCovintecRepository { get; }
        IGenericRepository<DetPrdMallaCovintec> DetPrdMallaCovintecRepository { get; }
        IReportesDapperRepository ReportesDapperRepository { get; }
        IGenericRepository<PrdIlKwang> PrdIlKwangRepository { get; }
        IGenericRepository<DetPrdIlKwang> DetPrdIlKwangRepository { get; }
        IGenericRepository<CatEspesor> CatEspesorRepository { get; }
        IGenericRepository<CatProdTermoIsoPanel> CatProdTermoIsoPanelRepository { get; }
        IGenericRepository<MaestroCatalogo> MaestroCatalogoRepository { get; }
        IGenericRepository<PrdNevera> PrdNeveraRepository  { get; }

        IGenericRepository<DetPrdNevera> DetPrdNeveraRepository { get; }
        IGenericRepository<PrdOtro> PrdOtroRepository { get; }
        IGenericRepository<DetPrdOtro> DetPrdOtroRepository { get; }
        IGenericRepository<SubDetPrdBloque> SubDetPrdBloqueRepository { get; }
        IGenericRepository<PrdBloque> PrdBloqueRepository { get; }
        IGenericRepository<DetPrdBloque> DetPrdBloqueRepository { get; }
        IGenericRepository<CatalogoBloque> CatalogoBloqueRepository { get; }
        IGenericRepository<CatLamina> CatLaminaRepository { get; }
        IGenericRepository<PrdCorteT> PrdCorteTRepository { get; }
        IGenericRepository<DetPrdCorteT> DetPrdCorteTRepository { get; }
        IGenericRepository<PrdCorteP> PrdCortePRepository { get; }
        IGenericRepository<DetPrdCorteP> DetPrdCortePRepository { get; }
        IGenericRepository<CatPantografo> CatPantografoRepository { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        /// <summary>
        /// Revierte la transacción activa, si hay una.
        /// </summary>
        Task RollbackAsync();
    }
}
