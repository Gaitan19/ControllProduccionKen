using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Models;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GestionCatalogosService : IGestionCatalogosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; // Si usas AutoMapper

        public GestionCatalogosService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task CreateAnchoBobinaAsync(AnchoBobinaDTO dto)
        {
            var anchoBobina = _mapper.Map<AnchoBobina>(dto);

            await _unitOfWork.AnchoBobinaRepository.AddAsync(anchoBobina);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateAsync(ColoresBobinaDTO dto)
        {
            var colorBobina = _mapper.Map<ColoresBobina>(dto);
            await _unitOfWork.ColoresBobinaRepository.AddAsync(colorBobina);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<AnchoBobinaDTO>> GetAllAnchoBobinaAsync()
        {
            var anchosBobina = await _unitOfWork.AnchoBobinaRepository.GetAllAsync();
            var anchoBobinaDTO = _mapper.Map<IEnumerable<AnchoBobinaDTO>>(anchosBobina);
            return anchoBobinaDTO;
        }

        public async Task<IEnumerable<ColoresBobinaDTO>> GetAllColoresBobinaAsync()
        {
            var coloresBobina = await _unitOfWork.ColoresBobinaRepository.GetAllAsync();

            // Mapear de ColoresBobina a ColoresBobinaDTO
            var coloresBobinaDTO = _mapper.Map<IEnumerable<ColoresBobinaDTO>>(coloresBobina);

            return coloresBobinaDTO;
        }

        public async Task<AnchoBobinaDTO> GetByAnchoBobinaIdAsync(int id)
        {
            var anchoBobina = await _unitOfWork.AnchoBobinaRepository.GetByIdAsync(id);
            return _mapper.Map<AnchoBobinaDTO>(anchoBobina);
        }

        public async Task<ColoresBobinaDTO> GetByIdAsync(int id)
        {
            var colorBobina = await _unitOfWork.ColoresBobinaRepository.GetByIdAsync(id);
            return _mapper.Map<ColoresBobinaDTO>(colorBobina);
        }

        public Task UpdateAAnchoBobinasync(AnchoBobinaDTO dto)
        {
            var anchoBobina = _mapper.Map<AnchoBobina>(dto);
            _unitOfWork.AnchoBobinaRepository.Update(anchoBobina);
            return _unitOfWork.SaveChangesAsync();
        }

        public Task UpdateAsync(ColoresBobinaDTO dto)
        {
            var colorBobina = _mapper.Map<ColoresBobina>(dto);
            _unitOfWork.ColoresBobinaRepository.Update(colorBobina);
            return _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CatalogoCerchaDTO>> GetAllCatalogoCerchaAsync()
        {
            var catalogoCercha = await _unitOfWork.CatalogoCerchaRepository.GetAllAsync();

            var catalogoCerchaDTO = _mapper.Map<IEnumerable<CatalogoCerchaDTO>>(catalogoCercha);

            return catalogoCerchaDTO;
        }

        public async Task<CatalogoCerchaDTO> GetByCatalogoCerchaIdAsync(int id)
        {
            var catalogoCercha = await _unitOfWork.CatalogoCerchaRepository.GetByIdAsync(id);
            return _mapper.Map<CatalogoCerchaDTO>(catalogoCercha);
        }


        public async Task CreateCatalogoCerchaAsync(CatalogoCerchaDTO dto)
        {
            var catalgoCerchaexists = await _unitOfWork.CatalogoCerchaRepository
        .FindAsync(c => c.CodigoArticulo == dto.CodigoArticulo);

            if (catalgoCerchaexists.Any())
            {
                throw new InvalidOperationException($"Ya existe un catálogo con el código: {dto.CodigoArticulo}");
            }

            var catalogoCercha = _mapper.Map<CatalogoCercha>(dto);
            await _unitOfWork.CatalogoCerchaRepository.AddAsync(catalogoCercha);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task UpdateCatalogoCerchaAsync(CatalogoCerchaDTO dto)
        {
            var catalogoCercha = _mapper.Map<CatalogoCercha>(dto);
            _unitOfWork.CatalogoCerchaRepository.Update(catalogoCercha);
            return _unitOfWork.SaveChangesAsync();
        }

        //Catalogo Mallas Covintec

        public async Task<IEnumerable<CatalogoMallasCovintecDTO>> GetAllCatalogoMallasCovintecAsync()
        {
            var catalogoMallasCovintec = await _unitOfWork.CatalogoMallasCovintecRepository.GetAllAsync();

            var catalogoMallasCovintecDTO = _mapper.Map<IEnumerable<CatalogoMallasCovintecDTO>>(catalogoMallasCovintec);

            return catalogoMallasCovintecDTO;
        }

        public async Task<CatalogoMallasCovintecDTO> GetByCatalogoMallasCovintecIdAsync(int id)
        {
            var catalogoMallasCovintec = await _unitOfWork.CatalogoMallasCovintecRepository.GetByIdAsync(id);
            return _mapper.Map<CatalogoMallasCovintecDTO>(catalogoMallasCovintec);
        }

        public async Task CreateCatalogoMallasCovintecAsync(CatalogoMallasCovintecDTO dto)
        {

            var catalogoMallaexists = await _unitOfWork.CatalogoMallasCovintecRepository.FindAsync(c => c.CodigoArticulo == dto.CodigoArticulo);

            if (catalogoMallaexists.Any())
            {
                throw new InvalidOperationException($"Ya existe un catálogo con el código: {dto.CodigoArticulo}");
            }

            var catalogoMallasCovintec = _mapper.Map<CatalogoMallasCovintec>(dto);
            await _unitOfWork.CatalogoMallasCovintecRepository.AddAsync(catalogoMallasCovintec);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task UpdateCatalogoMallasCovintecAsync(CatalogoMallasCovintecDTO dto)
        {
            var catalogoMallasCovintec = _mapper.Map<CatalogoMallasCovintec>(dto);
            _unitOfWork.CatalogoMallasCovintecRepository.Update(catalogoMallasCovintec);
            return _unitOfWork.SaveChangesAsync();

        }

        // catalogo paneles covintec
        public async Task<IEnumerable<CatalogoPanelesCovintecDTO>> GetAllCatalogoPanelesCovintecAsync()
        {
            var catalogoPanelesCovintec = await _unitOfWork.CatalogoPanelesCovintecRepository.GetAllAsync();

            var catalogoPanelesCovintecDTO = _mapper.Map<IEnumerable<CatalogoPanelesCovintecDTO>>(catalogoPanelesCovintec);

            return catalogoPanelesCovintecDTO;
        }

        public async Task<CatalogoPanelesCovintecDTO> GetByCatalogoPanelesCovintecIdAsync(int id)
        {
            var catalogoPanelesCovintec = await _unitOfWork.CatalogoPanelesCovintecRepository.GetByIdAsync(id);
            return _mapper.Map<CatalogoPanelesCovintecDTO>(catalogoPanelesCovintec);
        }

        public async Task CreateCatalogoPanelesCovintecAsync(CatalogoPanelesCovintecDTO dto)
        {

            var catalogoPanelExist = await _unitOfWork.CatalogoPanelesCovintecRepository
                .FindAsync(c => c.CodigoArticulo == dto.CodigoArticulo);

            if (catalogoPanelExist.Any())
            {
                throw new InvalidOperationException($"Ya existe un catálogo con el código: {dto.CodigoArticulo}");
            }

            var catalogoPanelesCovintec = _mapper.Map<CatalogoPanelesCovintec>(dto);
            await _unitOfWork.CatalogoPanelesCovintecRepository.AddAsync(catalogoPanelesCovintec);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task UpdateCatalogoPanelesCovintecAsync(CatalogoPanelesCovintecDTO dto)
        {
            var catalogoPanelesCovintec = _mapper.Map<CatalogoPanelesCovintec>(dto);
            _unitOfWork.CatalogoPanelesCovintecRepository.Update(catalogoPanelesCovintec);
            return _unitOfWork.SaveChangesAsync();
        }

        // catalogo status

        public async Task<IEnumerable<CatalogoStatusDTO>> GetAllCatalogoStatusAsync()
        {
            var catalogoStatus = await _unitOfWork.CatalogoStatusRepository.GetAllAsync();

            var catalogoStatusDTO = _mapper.Map<IEnumerable<CatalogoStatusDTO>>(catalogoStatus);

            return catalogoStatusDTO;
        }

        public async Task<CatalogoStatusDTO> GetByCatalogoStatusIdAsync(int id)
        {
            var catalogoStatus = await _unitOfWork.CatalogoStatusRepository.GetByIdAsync(id);
            return _mapper.Map<CatalogoStatusDTO>(catalogoStatus);
        }

        public async Task CreateCatalogoStatusAsync(CatalogoStatusDTO dto)
        {
            var catalogoStatus = _mapper.Map<CatalogoStatus>(dto);
            await _unitOfWork.CatalogoStatusRepository.AddAsync(catalogoStatus);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task UpdateCatalogoStatusAsync(CatalogoStatusDTO dto)
        {
            var catalogoStatus = _mapper.Map<CatalogoStatus>(dto);
            _unitOfWork.CatalogoStatusRepository.Update(catalogoStatus);
            return _unitOfWork.SaveChangesAsync();
        }

        // catalogo tipo
        public async Task<IEnumerable<CatalogoTipoDTO>> GetAllCatalogoTipoAsync()
        {
            var catalogoTipo = await _unitOfWork.CatalogoTipoRepository.GetAllAsync();

            var catalogoTipoDTO = _mapper.Map<IEnumerable<CatalogoTipoDTO>>(catalogoTipo);

            return catalogoTipoDTO;
        }

        public async Task<CatalogoTipoDTO> GetByCatalogoTipoIdAsync(int id)
        {
            var catalogoTipo = await _unitOfWork.CatalogoTipoRepository.GetByIdAsync(id);
            return _mapper.Map<CatalogoTipoDTO>(catalogoTipo);
        }

        public async Task CreateCatalogoTipoAsync(CatalogoTipoDTO dto)
        {
            var catalogoTipo = _mapper.Map<CatalogoTipo>(dto);
            await _unitOfWork.CatalogoTipoRepository.AddAsync(catalogoTipo);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task UpdateCatalogoTipoAsync(CatalogoTipoDTO dto)
        {
            var catalogoTipo = _mapper.Map<CatalogoTipo>(dto);
            _unitOfWork.CatalogoTipoRepository.Update(catalogoTipo);
            return _unitOfWork.SaveChangesAsync();
        }

        // linea produccion
        public async Task<IEnumerable<LineaProduccionDTO>> GetAllLineaProduccionAsync()
        {
            var lineaProduccion = await _unitOfWork.LineaProduccionRepository.GetAllAsync();

            var lineaProduccionDTO = _mapper.Map<IEnumerable<LineaProduccionDTO>>(lineaProduccion);

            return lineaProduccionDTO;
        }

        public async Task<LineaProduccionDTO> GetByLineaProduccionIdAsync(int id)
        {
            var lineaProduccion = await _unitOfWork.LineaProduccionRepository.GetByIdAsync(id);
            return _mapper.Map<LineaProduccionDTO>(lineaProduccion);
        }

        public async Task CreateLineaProduccionAsync(LineaProduccionDTO dto)
        {
            var lineaProduccion = _mapper.Map<LineaProduccion>(dto);
            await _unitOfWork.LineaProduccionRepository.AddAsync(lineaProduccion);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task UpdateLineaProduccionAsync(LineaProduccionDTO dto)
        {
            var lineaProduccion = _mapper.Map<LineaProduccion>(dto);
            _unitOfWork.LineaProduccionRepository.Update(lineaProduccion);
            return _unitOfWork.SaveChangesAsync();
        }

        // maquina
        public async Task<IEnumerable<MaquinaDto>> GetAllMaquinaAsync()
        {
            var maquinas = await _unitOfWork.CatMaquinaRepository.GetAllAsync();
            var maquinasDTO = _mapper.Map<IEnumerable<MaquinaDto>>(maquinas);
            return maquinasDTO;
        }

        public async Task<MaquinaDto> GetByMaquinaIdAsync(int id)
        {
            var maquina = await _unitOfWork.CatMaquinaRepository.GetByIdAsync(id);
            return _mapper.Map<MaquinaDto>(maquina);
        }

        public async Task CreateMaquinaAsync(MaquinaDto dto)
        {
            var maquina = _mapper.Map<Maquina>(dto);
            await _unitOfWork.CatMaquinaRepository.AddAsync(maquina);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task UpdateMaquinaAsync(MaquinaDto dto)
        {
            var maquina = _mapper.Map<Maquina>(dto);
            _unitOfWork.CatMaquinaRepository.Update(maquina);
            return _unitOfWork.SaveChangesAsync();
        }

        // tipo fabricacion
        public async Task<IEnumerable<TipoFabricacionDto>> GetAllTipoFabricacionAsync()
        {
            var tipoFabricacion = await _unitOfWork.TipoFabricacionRepository.GetAllAsync();
            var tipoFabricacionDTO = _mapper.Map<IEnumerable<TipoFabricacionDto>>(tipoFabricacion);
            return tipoFabricacionDTO;
        }

        public async Task<TipoFabricacionDto> GetByTipoFabricacionIdAsync(int id)
        {
            var tipoFabricacion = await _unitOfWork.TipoFabricacionRepository.GetByIdAsync(id);
            return _mapper.Map<TipoFabricacionDto>(tipoFabricacion);
        }

        public async Task CreateTipoFabricacionAsync(TipoFabricacionDto dto)
        {
            var tipoFabricacion = _mapper.Map<TipoFabricacion>(dto);
            await _unitOfWork.TipoFabricacionRepository.AddAsync(tipoFabricacion);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task UpdateTipoFabricacionAsync(TipoFabricacionDto dto)
        {
            var tipoFabricacion = _mapper.Map<TipoFabricacion>(dto);
            _unitOfWork.TipoFabricacionRepository.Update(tipoFabricacion);
            return _unitOfWork.SaveChangesAsync();
        }

        // ubicacion bobina
        public async Task<IEnumerable<UbicacionBobinaDTO>> GetAllUbicacionBobinaAsync()
        {
            var ubicacionBobina = await _unitOfWork.UbicacionBobinaRepository.GetAllAsync();
            var ubicacionBobinaDTO = _mapper.Map<IEnumerable<UbicacionBobinaDTO>>(ubicacionBobina);
            return ubicacionBobinaDTO;
        }

        public async Task<UbicacionBobinaDTO> GetByUbicacionBobinaIdAsync(int id)
        {
            var ubicacionBobina = await _unitOfWork.UbicacionBobinaRepository.GetByIdAsync(id);
            return _mapper.Map<UbicacionBobinaDTO>(ubicacionBobina);
        }

        public async Task CreateUbicacionBobinaAsync(UbicacionBobinaDTO dto)
        {
            var ubicacionBobina = _mapper.Map<UbicacionBobina>(dto);
            await _unitOfWork.UbicacionBobinaRepository.AddAsync(ubicacionBobina);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task UpdateUbicacionBobinaAsync(UbicacionBobinaDTO dto)
        {
            var ubicacionBobina = _mapper.Map<UbicacionBobina>(dto);
            _unitOfWork.UbicacionBobinaRepository.Update(ubicacionBobina);
            return _unitOfWork.SaveChangesAsync();
        }


    }
}
