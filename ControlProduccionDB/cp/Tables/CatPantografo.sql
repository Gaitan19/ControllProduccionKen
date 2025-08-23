CREATE TABLE [cp].[CatPantografo] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [IdLineaProduccion]   INT           NULL,
    [CodigoArticulo]      VARCHAR (50)  NOT NULL,
    [DescripcionArticulo] VARCHAR (200) NOT NULL,
    [Activo]              BIT           NULL,
    CONSTRAINT [PK_CatPantografo] PRIMARY KEY CLUSTERED ([Id] ASC)
);

