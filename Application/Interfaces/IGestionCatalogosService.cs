using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGestionCatalogosService
    {
        //Catalogo Colores Bonina
        Task<IEnumerable<ColoresBobinaDTO>> GetAllColoresBobinaAsync();
        Task<ColoresBobinaDTO> GetByIdAsync(int id);
        Task CreateAsync(ColoresBobinaDTO dto);
        Task UpdateAsync( ColoresBobinaDTO dto);
        //catalogo ancho bobina
        Task<IEnumerable<AnchoBobinaDTO>> GetAllAnchoBobinaAsync();
        Task<AnchoBobinaDTO> GetByAnchoBobinaIdAsync(int id);
        Task CreateAnchoBobinaAsync(AnchoBobinaDTO dto);
        Task UpdateAAnchoBobinasync(AnchoBobinaDTO dto);

        // Catalogo Cercha
        Task<IEnumerable<CatalogoCerchaDTO>> GetAllCatalogoCerchaAsync();
        Task<CatalogoCerchaDTO> GetByCatalogoCerchaIdAsync(int id);
        Task CreateCatalogoCerchaAsync(CatalogoCerchaDTO dto);
        Task UpdateCatalogoCerchaAsync(CatalogoCerchaDTO dto);

        //Catalogo Mallas Covintec
        Task<IEnumerable<CatalogoMallasCovintecDTO>> GetAllCatalogoMallasCovintecAsync();
        Task<CatalogoMallasCovintecDTO> GetByCatalogoMallasCovintecIdAsync(int id);
        Task CreateCatalogoMallasCovintecAsync(CatalogoMallasCovintecDTO dto);
        Task UpdateCatalogoMallasCovintecAsync(CatalogoMallasCovintecDTO dto);

        // Catalogo Paneles Covintec
        Task<IEnumerable<CatalogoPanelesCovintecDTO>> GetAllCatalogoPanelesCovintecAsync();
        Task<CatalogoPanelesCovintecDTO> GetByCatalogoPanelesCovintecIdAsync(int id);
        Task CreateCatalogoPanelesCovintecAsync(CatalogoPanelesCovintecDTO dto);
        Task UpdateCatalogoPanelesCovintecAsync(CatalogoPanelesCovintecDTO dto);

        // Catalogo Status
        Task<IEnumerable<CatalogoStatusDTO>> GetAllCatalogoStatusAsync();
        Task<CatalogoStatusDTO> GetByCatalogoStatusIdAsync(int id);
        Task CreateCatalogoStatusAsync(CatalogoStatusDTO dto);
        Task UpdateCatalogoStatusAsync(CatalogoStatusDTO dto);

        // catalogo Tipo
        Task<IEnumerable<CatalogoTipoDTO>> GetAllCatalogoTipoAsync();
        Task<CatalogoTipoDTO> GetByCatalogoTipoIdAsync(int id);
        Task CreateCatalogoTipoAsync(CatalogoTipoDTO dto);
        Task UpdateCatalogoTipoAsync(CatalogoTipoDTO dto);

        // linea Produccion
        Task<IEnumerable<LineaProduccionDTO>> GetAllLineaProduccionAsync();
        Task<LineaProduccionDTO> GetByLineaProduccionIdAsync(int id);
        Task CreateLineaProduccionAsync(LineaProduccionDTO dto);
        Task UpdateLineaProduccionAsync(LineaProduccionDTO dto);

        // maquina
        Task<IEnumerable<MaquinaDto>> GetAllMaquinaAsync();
        Task<MaquinaDto> GetByMaquinaIdAsync(int id);
        Task CreateMaquinaAsync(MaquinaDto dto);
        Task UpdateMaquinaAsync(MaquinaDto dto);

        // tipo fabricacion
        Task<IEnumerable<TipoFabricacionDto>> GetAllTipoFabricacionAsync();
        Task<TipoFabricacionDto> GetByTipoFabricacionIdAsync(int id);
        Task CreateTipoFabricacionAsync(TipoFabricacionDto dto);
        Task UpdateTipoFabricacionAsync(TipoFabricacionDto dto);

        // ubicacion bobina
        Task<IEnumerable<UbicacionBobinaDTO>> GetAllUbicacionBobinaAsync();
        Task<UbicacionBobinaDTO> GetByUbicacionBobinaIdAsync(int id);
        Task CreateUbicacionBobinaAsync(UbicacionBobinaDTO dto);
        Task UpdateUbicacionBobinaAsync(UbicacionBobinaDTO dto);

    }
}
