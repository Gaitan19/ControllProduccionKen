CREATE TABLE [cp].[CatalogoBloques] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [Bloque]     VARCHAR (20)    NULL,
    [CubicajeM3] DECIMAL (10, 2) NULL,
    [Medidas]    VARCHAR (50)    NULL,
    [Activo]     BIT             DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

