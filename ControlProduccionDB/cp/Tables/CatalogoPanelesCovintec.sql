CREATE TABLE [cp].[CatalogoPanelesCovintec] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [IdLineaProduccion]   INT             NOT NULL,
    [DescripcionArticulo] VARCHAR (200)   NOT NULL,
    [CodigoArticulo]      VARCHAR (50)    NOT NULL,
    [Mts2PorPanel]        DECIMAL (10, 4) NOT NULL,
    [Activo]              BIT             DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CatalogoPanelesCovintec_LineaProduccion] FOREIGN KEY ([IdLineaProduccion]) REFERENCES [cp].[LineaProduccion] ([Id])
);

