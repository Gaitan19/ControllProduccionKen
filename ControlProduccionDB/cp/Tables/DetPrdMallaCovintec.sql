CREATE TABLE [cp].[DetPrdMallaCovintec] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [IdMalla]                INT             NOT NULL,
    [IdArticulo]             INT             NOT NULL,
    [CantidadProducida]      INT             NOT NULL,
    [CantidadNoConforme]     INT             NOT NULL,
    [IdTipoFabricacion]      INT             NOT NULL,
    [NumeroPedido]           INT             NULL,
    [ProduccionDia]          DECIMAL (18, 2) NOT NULL,
    [AprobadoSupervisor]     BIT             NOT NULL,
    [AprobadoGerencia]       BIT             NOT NULL,
    [IdAprobadoSupervisor]   NVARCHAR (450)  NULL,
    [IdAprobadoGerencia]     NVARCHAR (450)  NULL,
    [IdUsuarioCreacion]      NVARCHAR (450)  NOT NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450)  NULL,
    [FechaActualizacion]     DATETIME        NULL,
    CONSTRAINT [PK__DetPrdMa__3214EC07YYYYYYYY] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetPrdMallaCovintec_Catalogo] FOREIGN KEY ([IdArticulo]) REFERENCES [cp].[CatalogoPanelesCovintec] ([Id]),
    CONSTRAINT [FK_DetPrdMallaCovintec_Principal] FOREIGN KEY ([IdMalla]) REFERENCES [cp].[PrdMallaCovintec] ([Id]),
    CONSTRAINT [FK_DetPrdMallaCovintec_TipoFabricacion] FOREIGN KEY ([IdTipoFabricacion]) REFERENCES [cp].[TipoFabricacion] ([Id])
);

