CREATE TABLE [cp].[CatalogosPermitidosPorReporte] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [IdTipoReporte] INT            NOT NULL,
    [Catalogo]      NVARCHAR (150) NOT NULL,
    [IdCatalogo]    INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

