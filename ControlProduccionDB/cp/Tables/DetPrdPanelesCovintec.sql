CREATE TABLE [cp].[DetPrdPanelesCovintec] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [IdPanel]                INT             NOT NULL,
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
    CONSTRAINT [PK__DetPrdPa__3214EC07BC260943] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetPrdPanelesCovintec_CatalogoPanelesCovintec] FOREIGN KEY ([IdArticulo]) REFERENCES [cp].[CatalogoPanelesCovintec] ([Id]),
    CONSTRAINT [FK_DetPrdPanelesCovintec_Principal] FOREIGN KEY ([IdPanel]) REFERENCES [cp].[PrdPanelesCovintec] ([Id]),
    CONSTRAINT [FK_DetPrdPanelesCovintec_TipoFabricacion] FOREIGN KEY ([IdTipoFabricacion]) REFERENCES [cp].[TipoFabricacion] ([Id])
);

