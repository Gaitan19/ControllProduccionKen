using Application.DTOs;
using AutoMapper;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Aquí defines todos tus mapeos
            CreateMap<TipoFabricacion, TipoFabricacionDto>().ReverseMap();
            // CreateMap<OtraEntidad, OtraEntidadDto>().ReverseMap();
            CreateMap<PrdPanelesCovintec, CrearPrdPanelesCovintecDto>().ReverseMap();
            CreateMap<Maquina, MaquinaDto>().ReverseMap();
            CreateMap<CatalogoPanelesCovintec, CatalogoPanelesCovintecDTO>().ReverseMap();
            CreateMap<ColoresBobina, ColoresBobinaDTO>().ReverseMap();
            CreateMap<AnchoBobina, AnchoBobinaDTO>().ReverseMap();
            CreateMap<CatalogoCercha,CatalogoCerchaDTO>().ReverseMap();
            CreateMap<CatalogoMallasCovintec, CatalogoMallasCovintecDTO>().ReverseMap();
            CreateMap<CatalogoStatus, CatalogoStatusDTO>().ReverseMap();
            CreateMap<CatalogoTipo, CatalogoTipoDTO>().ReverseMap();
            CreateMap<UbicacionBobina, UbicacionBobinaDTO>().ReverseMap();
         
            CreateMap<LineaProduccion, LineaProduccionDTO>().ReverseMap();
            CreateMap<CatProdTermoIsoPanel, CatProdTermoIsoPanelDTO>().ReverseMap();
            CreateMap<CatEspesor, CatEspesorDTO>().ReverseMap();
            CreateMap<CatalogoBloque, CatalogoBloqueDTO>().ReverseMap();

        }
    }
}
