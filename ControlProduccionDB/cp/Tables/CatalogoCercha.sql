CREATE TABLE [cp].[CatalogoCercha] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [IdLineaProduccion]   INT             NOT NULL,
    [CodigoArticulo]      VARCHAR (50)    NOT NULL,
    [DescripcionArticulo] VARCHAR (200)   NOT NULL,
    [LongitudMetros]      DECIMAL (18, 2) NOT NULL,
    [EspesorMetros]       DECIMAL (18, 4) NULL,
    [AreaM2]              DECIMAL (18, 6) NULL,
    [Activo]              BIT             CONSTRAINT [DF__CatalogoC__Activ__37A5467C] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK__Catalogo__3214EC07BB4888BA] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CatalogoCercha_LineaProduccion] FOREIGN KEY ([IdLineaProduccion]) REFERENCES [cp].[LineaProduccion] ([Id])
);

