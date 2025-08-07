CREATE TABLE [cp].[DetPrdCerchaCovintec] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [IdCercha]               INT             NOT NULL,
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
    CONSTRAINT [PK_DetPrdCerchaCovintec] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetPrdCerchaCovintec_CatalogoCercha] FOREIGN KEY ([IdArticulo]) REFERENCES [cp].[CatalogoCercha] ([Id]),
    CONSTRAINT [FK_DetPrdCerchaCovintec_PrdCerchaCovintec] FOREIGN KEY ([IdCercha]) REFERENCES [cp].[PrdCerchaCovintec] ([Id]),
    CONSTRAINT [FK_DetPrdCerchaCovintec_TipoFabricacion] FOREIGN KEY ([IdTipoFabricacion]) REFERENCES [cp].[TipoFabricacion] ([Id])
);

