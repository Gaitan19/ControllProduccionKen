#nullable disable
using Infrastructure.Data;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;

        // Campos privados para repositorios especificos
        // private IProductoRepository _productoRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        //public IProductoRepository Productos
        //{
        //    get
        //    {
        //        // Inicializa el repositorio si es null
        //        return _productoRepository ??= new ProductoRepository(_context);
        //    }
        //}


        // Ejemplo de repositorio genérico para otra entidad
        // private IGenericRepository<OtraEntidad> _otraEntidadRepository;
        // public IGenericRepository<OtraEntidad> OtrasEntidades
        // {
        //     get
        //     {
        //         return _otraEntidadRepository ??= new GenericRepository<OtraEntidad>(_context);
        //     }
        // }
        private IGenericRepository<TipoFabricacion> _tipoFabricacionRepository;
        private IGenericRepository<Maquina> _catMaquinaRepository;
        private IGenericRepository<CatalogoPanelesCovintec> _catalogoPanelesCovintecRepository;
        private IGenericRepository<PrdPanelesCovintec> _prdPanelesCovintecRepository;
        private IGenericRepository<CatalogosPermitidosPorReporte> _catalogosPermitidosPorReporteRepository;
        private IGenericRepository<DetPrdPanelesCovintec> _detPrdPanelesCovintecRepository;
        private IGenericRepository<DetAlambrePrdPanelesCovintec> _detAlambrePrdPanelesCovintecRepository;
        private IGenericRepository<ColoresBobina> _coloresBobinaRepository;
        private IGenericRepository<AnchoBobina> _anchoBobinaRepository;
        private IGenericRepository<CatalogoCercha> _catalogoCerchaRepository;
        private IGenericRepository<CatalogoMallasCovintec> _catalogoMallasCovintecRepository;
        private IGenericRepository<CatalogoStatus> _catalogoStatusRepository;
        private IGenericRepository<CatalogoTipo> _catalogoTipoRepository;
        private IGenericRepository<LineaProduccion> _lineaProduccionRepository;
        private IGenericRepository<UbicacionBobina> _ubicacionBobinaRepository;
        private IGenericRepository<PrdCerchaCovintec> _prdCerchaCovintecRepository;
        private IGenericRepository<DetAlambrePrdCerchaCovintec> _detAlambrePrdCerchaCovintecRepository;
        private IGenericRepository<DetPrdCerchaCovintec> _detPrdCerchaCovintecRepository;
        private IReportesDapperRepository _reportesDapperRepository;
        private IGenericRepository<PrdMallaCovintec> _prdMallaCovintecRepository;
        private IGenericRepository<DetAlambrePrdMallaCovintec> _detAlambrePrdMallaCovintecRepository;
        private IGenericRepository<DetPrdMallaCovintec> _detPrdMallaCovintecRepository;
        private IGenericRepository<PrdIlKwang> _prdIlKwangRepository;
        private IGenericRepository<DetPrdIlKwang> _detPrdIlKwangRepository;
        private IGenericRepository<CatEspesor> _catEspesorRepository;
        private IGenericRepository<CatProdTermoIsoPanel> _catProdTermoIsoPanelRepository;
        private IGenericRepository<MaestroCatalogo> _maestroCatalogoRepository;
        private IGenericRepository<PrdNevera> _prdNeveraRepository;
        private IGenericRepository<DetPrdNevera> _detPrdNeveraRepository;
        private IGenericRepository<PrdOtro> _prdOtroRepository;
        private IGenericRepository<DetPrdOtro> _detPrdOtroRepository;
        private IGenericRepository<SubDetPrdBloque> _subDetPrdBloqueRepository;
        private IGenericRepository<PrdBloque> _prdBloqueRepository;
        private IGenericRepository<DetPrdBloque> _detPrdBloqueRepository;
        private IGenericRepository<CatalogoBloque> _catalogoBloqueRepository;
        private IGenericRepository<CatLamina> _catLaminaRepository;
        private IGenericRepository<PrdCorteT> _prdCorteTRepository;
        private IGenericRepository<DetPrdCorteT> _detPrdCorteTRepository;
        private IGenericRepository<PrdCorteP> _prdCortePRepository;
        private IGenericRepository<DetPrdCorteP> _detPrdCortePRepository;
        private IGenericRepository<PrdpreExpansion> _prdpreExpansionRepository;
        private IGenericRepository<DetPrdpreExpansion> _detPrdpreExpansionRepository;
        private IGenericRepository<CatPantografo> _catPantografoRepository;
        private IGenericRepository<CatTipoMalla> _catTipoMallaRepository;
        private IGenericRepository<PrdMallaPch> _prdMallaPchRepository;
        private IGenericRepository<DetPrdPchMaquinaA> _detPrdPchMaquinaARepository;
        private IGenericRepository<DetPrdPchMaquinaB> _detPrdPchMaquinaBRepository;
        private IGenericRepository<DetPrdPchMaquinaC> _detPrdPchMaquinaCRepository;
        private IGenericRepository<CatalogoPanelesPch> _catalogoPanelesPchRepository;
        private IGenericRepository<PrdPaneladoraPch> _prdPaneladoraPchRepository;
        private IGenericRepository<DetPrdPaneladoraPch> _detPrdPaneladoraPchRepository;
        private IGenericRepository<CatalogoAccesorio> _catalogoAccesorioRepository;
        private IGenericRepository<ErrorLog> _errorLogRepository;
        private IGenericRepository<PrdAccesorio> _prdAccesorioRepository;
        private IGenericRepository<DetPrdAccesorio> _detPrdAccesorioRepository;
        public IReportesDapperRepository ReportesDapperRepository
        {
            get
            {
                return _reportesDapperRepository ??= new ReportesDapperRepository(_context);
            }
        }
        public IGenericRepository<UbicacionBobina> UbicacionBobinaRepository
        {
            get
            {
                return _ubicacionBobinaRepository
                    ??= new GenericRepository<UbicacionBobina>(_context);
            }
        }

        public IGenericRepository<LineaProduccion> LineaProduccionRepository
        {
            get
            {
                return _lineaProduccionRepository
                    ??= new GenericRepository<LineaProduccion>(_context);
            }
        }

        public IGenericRepository<CatalogoTipo> CatalogoTipoRepository
        {
            get
            {
                return _catalogoTipoRepository
                    ??= new GenericRepository<CatalogoTipo>(_context);
            }
        }

        public IGenericRepository<CatalogoStatus> CatalogoStatusRepository
        {
            get
            {
                return _catalogoStatusRepository
                    ??= new GenericRepository<CatalogoStatus>(_context);
            }
        }

        public IGenericRepository<CatalogoMallasCovintec> CatalogoMallasCovintecRepository
        {
            get
            {
                return _catalogoMallasCovintecRepository
                    ??= new GenericRepository<CatalogoMallasCovintec>(_context);
            }
        }

        public IGenericRepository<CatalogoCercha> CatalogoCerchaRepository
        {
            get
            {
                return _catalogoCerchaRepository
                    ??= new GenericRepository<CatalogoCercha>(_context);
            }
        }


        public IGenericRepository<AnchoBobina> AnchoBobinaRepository
        {
            get
            {
                return _anchoBobinaRepository
                    ??= new GenericRepository<AnchoBobina>(_context);
            }
        }

        public IGenericRepository<ColoresBobina> ColoresBobinaRepository
        {
            get
            {
                return _coloresBobinaRepository
                    ??= new GenericRepository<ColoresBobina>(_context);
            }
        }

        public IGenericRepository<DetAlambrePrdPanelesCovintec> DetAlambrePrdPanelesCovintecRepository
        {
            get
            {
                return _detAlambrePrdPanelesCovintecRepository
                    ??= new GenericRepository<DetAlambrePrdPanelesCovintec>(_context);
            }
        }

        public IGenericRepository<DetPrdPanelesCovintec> DetPrdPanelesCovintecRepository
        {
            get
            {
                return _detPrdPanelesCovintecRepository
                    ??= new GenericRepository<DetPrdPanelesCovintec>(_context);
            }
        }
        public IGenericRepository<CatalogosPermitidosPorReporte> catalogosPermitidosPorReporteRepository
        {
            get
            {
                return _catalogosPermitidosPorReporteRepository
                    ??= new GenericRepository<CatalogosPermitidosPorReporte>(_context);
            }
        }
        public IGenericRepository<PrdPanelesCovintec> PrdPanelesCovintecRepository
        {
            get
            {
                return _prdPanelesCovintecRepository
                    ??= new GenericRepository<PrdPanelesCovintec>(_context);
            }
        }
        public IGenericRepository<CatalogoPanelesCovintec> CatalogoPanelesCovintecRepository
        {
            get
            {
                return _catalogoPanelesCovintecRepository
                    ??= new GenericRepository<CatalogoPanelesCovintec>(_context);
            }
        }


        public IGenericRepository<TipoFabricacion> TipoFabricacionRepository
        {
            get
            {
                return _tipoFabricacionRepository
                    ??= new GenericRepository<TipoFabricacion>(_context);
            }
        }

        public IGenericRepository<Maquina> CatMaquinaRepository
        {
            get
            {
                return _catMaquinaRepository
                    ??= new GenericRepository<Maquina>(_context);
            }
        }

        public IGenericRepository<PrdCerchaCovintec> PrdCerchaCovintecRepository
        {
            get
            {
                return _prdCerchaCovintecRepository
                    ??= new GenericRepository<PrdCerchaCovintec>(_context);
            }
        }

        public IGenericRepository<DetAlambrePrdCerchaCovintec> DetAlambrePrdCerchaCovintecRepository
        {
            get
            {
                return _detAlambrePrdCerchaCovintecRepository
                    ??= new GenericRepository<DetAlambrePrdCerchaCovintec>(_context);
            }
        }
        public IGenericRepository<DetPrdCerchaCovintec> DetPrdCerchaCovintecRepository
        {
            get
            {
                return _detPrdCerchaCovintecRepository
                    ??= new GenericRepository<DetPrdCerchaCovintec>(_context);
            }
        }

     
     public IGenericRepository<PrdMallaCovintec> PrdMallaCovintecRepository
        {
            get
            {
                return _prdMallaCovintecRepository
                    ??= new GenericRepository<PrdMallaCovintec>(_context);
            }
        }

        public IGenericRepository<DetPrdMallaCovintec> DetPrdMallaCovintecRepository
        {
            get
            {
                return _detPrdMallaCovintecRepository
                    ??= new GenericRepository<DetPrdMallaCovintec>(_context);
            }
        }

        public IGenericRepository<DetAlambrePrdMallaCovintec> DetAlambrePrdMallaCovintecRepository
        {
            get
            {
                return _detAlambrePrdMallaCovintecRepository
                    ??= new GenericRepository<DetAlambrePrdMallaCovintec>(_context);
            }
        }

        public IGenericRepository<PrdIlKwang> PrdIlKwangRepository
        {
            get
            {
                return _prdIlKwangRepository
                    ??= new GenericRepository<PrdIlKwang>(_context);
            }
        }

        public IGenericRepository<DetPrdIlKwang> DetPrdIlKwangRepository
        {
            get
            {
                return _detPrdIlKwangRepository
                    ??= new GenericRepository<DetPrdIlKwang>(_context);
            }
        }

        public IGenericRepository<CatEspesor> CatEspesorRepository
        {
            get
            {
                return _catEspesorRepository
                    ??= new GenericRepository<CatEspesor>(_context);
            }
        }

        public IGenericRepository<CatProdTermoIsoPanel> CatProdTermoIsoPanelRepository
        {
            get
            {
                return _catProdTermoIsoPanelRepository
                    ??= new GenericRepository<CatProdTermoIsoPanel>(_context);
            }
        }

        public IGenericRepository<MaestroCatalogo> MaestroCatalogoRepository
        {
            get
            {
                return _maestroCatalogoRepository
                    ??= new GenericRepository<MaestroCatalogo>(_context);
            }
        }

        public IGenericRepository<PrdNevera> PrdNeveraRepository
        {
            get
            {
                return _prdNeveraRepository
                    ??= new GenericRepository<PrdNevera>(_context);
            }
        }

        public IGenericRepository<DetPrdNevera> DetPrdNeveraRepository
        {
            get
            {
                return _detPrdNeveraRepository
                    ??= new GenericRepository<DetPrdNevera>(_context);
            }
        }

        public IGenericRepository<PrdOtro> PrdOtroRepository
        {
            get
            {
                return _prdOtroRepository
                    ??= new GenericRepository<PrdOtro>(_context);
            }
        }

        public IGenericRepository<DetPrdOtro> DetPrdOtroRepository
        {
            get
            {
                return _detPrdOtroRepository
                    ??= new GenericRepository<DetPrdOtro>(_context);
            }
        }

        public IGenericRepository<SubDetPrdBloque> SubDetPrdBloqueRepository
        {
            get
            {
                return _subDetPrdBloqueRepository ??= new GenericRepository<SubDetPrdBloque>(_context);
            }
        }
        public IGenericRepository<PrdBloque> PrdBloqueRepository
        {
            get
            {
                return _prdBloqueRepository ??= new GenericRepository<PrdBloque>(_context);
            }
        }
        public IGenericRepository<DetPrdBloque> DetPrdBloqueRepository
        {
            get
            {
                return _detPrdBloqueRepository ??= new GenericRepository<DetPrdBloque>(_context);
            }
        }
        public IGenericRepository<CatalogoBloque> CatalogoBloqueRepository
        {
            get
            {
                return _catalogoBloqueRepository ??= new GenericRepository<CatalogoBloque>(_context);
            }
        }

        //implementar las propiedades que faltan de manera completa
        public IGenericRepository<CatLamina> CatLaminaRepository
        {
            get
            {
                return _catLaminaRepository ??= new GenericRepository<CatLamina>(_context);
            }
        }
        public IGenericRepository<PrdCorteT> PrdCorteTRepository
        {
            get
            {
                return _prdCorteTRepository ??= new GenericRepository<PrdCorteT>(_context);
            }
        }
        public IGenericRepository<DetPrdCorteT> DetPrdCorteTRepository
        {
            get
            {
                return _detPrdCorteTRepository ??= new GenericRepository<DetPrdCorteT>(_context);
            }
        }
        public IGenericRepository<PrdCorteP> PrdCortePRepository
        {
            get
            {
                return _prdCortePRepository ??= new GenericRepository<PrdCorteP>(_context);
            }
        }
        public IGenericRepository<DetPrdCorteP> DetPrdCortePRepository
        {
            get
            {
                return _detPrdCortePRepository ??= new GenericRepository<DetPrdCorteP>(_context);
            }
        }

        public IGenericRepository<PrdpreExpansion> PrdpreExpansionRepository
        {
            get
            {
                return _prdpreExpansionRepository ??= new GenericRepository<PrdpreExpansion>(_context);
            }
        }

        public IGenericRepository<DetPrdpreExpansion> DetPrdpreExpansionRepository
        {
            get
            {
                return _detPrdpreExpansionRepository ??= new GenericRepository<DetPrdpreExpansion>(_context);
            }
        }
        public IGenericRepository<CatPantografo> CatPantografoRepository
        {
            get
            {
                return _catPantografoRepository ??= new GenericRepository<CatPantografo>(_context);
            }
        }
        public IGenericRepository<CatTipoMalla> CatTipoMallaRepository
        {
            get
            {
                return _catTipoMallaRepository ??= new GenericRepository<CatTipoMalla>(_context);
            }
        }
        public IGenericRepository<PrdMallaPch> PrdMallaPchRepository
        {
            get
            {
                return _prdMallaPchRepository ??= new GenericRepository<PrdMallaPch>(_context);
            }
        }
        public IGenericRepository<DetPrdPchMaquinaA> DetPrdPchMaquinaARepository
        {
            get
            {
                return _detPrdPchMaquinaARepository ??= new GenericRepository<DetPrdPchMaquinaA>(_context);
            }
        }
        public IGenericRepository<DetPrdPchMaquinaB> DetPrdPchMaquinaBRepository
        {
            get
            {
                return _detPrdPchMaquinaBRepository ??= new GenericRepository<DetPrdPchMaquinaB>(_context);
            }
        }
        public IGenericRepository<DetPrdPchMaquinaC> DetPrdPchMaquinaCRepository
        {
            get
            {
                return _detPrdPchMaquinaCRepository ??= new GenericRepository<DetPrdPchMaquinaC>(_context);
            }
        }
        public IGenericRepository<CatalogoPanelesPch> CatalogoPanelesPchRepository
        {
            get
            {
                return _catalogoPanelesPchRepository ??= new GenericRepository<CatalogoPanelesPch>(_context);
            }
        }
        public IGenericRepository<PrdPaneladoraPch> PrdPaneladoraPchRepository
        {
            get
            {
                return _prdPaneladoraPchRepository ??= new GenericRepository<PrdPaneladoraPch>(_context);
            }
        }
        public IGenericRepository<DetPrdPaneladoraPch> DetPrdPaneladoraPchRepository
        {
            get
            {
                return _detPrdPaneladoraPchRepository ??= new GenericRepository<DetPrdPaneladoraPch>(_context);
            }
        }
        public IGenericRepository<CatalogoAccesorio> CatalogoAccesorioRepository
        {
            get
            {
                return _catalogoAccesorioRepository ??= new GenericRepository<CatalogoAccesorio>(_context);
            }
        }
        public IGenericRepository<PrdAccesorio> PrdAccesorioRepository
        {
            get
            {
                return _prdAccesorioRepository ??= new GenericRepository<PrdAccesorio>(_context);
            }
        }
        public IGenericRepository<DetPrdAccesorio> DetPrdAccesorioRepository
        {
            get
            {
                return _detPrdAccesorioRepository ??= new GenericRepository<DetPrdAccesorio>(_context);
            }
        }
        public IGenericRepository<ErrorLog> ErrorLogRepository
        {
            get
            {
                return _errorLogRepository ??= new GenericRepository<ErrorLog>(_context);
            }
        }

        // Inicia una transacción explícita
        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
        }

        // Guarda todos los cambios dentro de la transacción, o revierte en caso de error
        public async Task CommitAsync()
        {
            try
            {
                // Guarda todos los cambios pendientes en el contexto
                await _context.SaveChangesAsync();

                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
            catch (Exception)
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
                throw; // Propaga la excepción para manejarla en capas superiores si es necesario
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
