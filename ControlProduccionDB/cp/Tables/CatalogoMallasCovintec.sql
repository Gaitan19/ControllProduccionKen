CREATE TABLE [cp].[CatalogoMallasCovintec] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [IdLineaProduccion]   INT             NOT NULL,
    [CodigoArticulo]      VARCHAR (50)    NOT NULL,
    [DescripcionArticulo] VARCHAR (200)   NOT NULL,
    [LongitudCentimetros] DECIMAL (18, 2) NOT NULL,
    [Activo]              BIT             CONSTRAINT [DF__CatalogoM__Activ__3B75D760] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK__Catalogo__3214EC0761F9603A] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CatalogoMallasCovintec_LineaProduccion] FOREIGN KEY ([IdLineaProduccion]) REFERENCES [cp].[LineaProduccion] ([Id])
);

