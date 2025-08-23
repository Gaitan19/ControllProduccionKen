CREATE TABLE [cp].[CatalogoPanelesPCH] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [FamiliaProducto]     VARCHAR (20)  NOT NULL,
    [DescripcionArticulo] VARCHAR (200) NOT NULL,
    [CodigoArticulo]      VARCHAR (50)  NOT NULL,
    [Activo]              BIT           CONSTRAINT [DF_CatalogoPanelesPCH_Activo] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CatalogoPanelesPCH] PRIMARY KEY CLUSTERED ([Id] ASC)
);

