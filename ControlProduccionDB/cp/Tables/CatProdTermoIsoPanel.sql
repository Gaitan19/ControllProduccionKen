CREATE TABLE [cp].[CatProdTermoIsoPanel] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [IdLineaProduccion]   INT           NOT NULL,
    [CodigoArticulo]      VARCHAR (50)  NOT NULL,
    [DescripcionArticulo] VARCHAR (200) NOT NULL,
    [Activo]              BIT           CONSTRAINT [DF_CatalogoPanelesCovintec_Activo] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CatalogoPanelesCovintec] PRIMARY KEY CLUSTERED ([Id] ASC)
);

