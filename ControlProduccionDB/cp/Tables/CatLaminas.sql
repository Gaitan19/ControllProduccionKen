CREATE TABLE [cp].[CatLaminas] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [IdLineaProduccion]   INT           NULL,
    [CodigoArticulo]      VARCHAR (50)  NOT NULL,
    [DescripcionArticulo] VARCHAR (200) NOT NULL,
    [Activo]              BIT           NOT NULL,
    CONSTRAINT [PK_CatLaminas] PRIMARY KEY CLUSTERED ([Id] ASC)
);

