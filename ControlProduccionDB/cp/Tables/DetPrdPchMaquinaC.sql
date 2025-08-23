CREATE TABLE [cp].[DetPrdPchMaquinaC] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [PrdMallaPchId]          INT             NOT NULL,
    [IdMaquina]              INT             NOT NULL,
    [IdTipoMalla]            INT             NOT NULL,
    [MermaMallasKg]          DECIMAL (18, 1) NOT NULL,
    [IdTipoFabricacion]      INT             NOT NULL,
    [NumeroPedido]           INT             NULL,
    [Longitud]               DECIMAL (18, 2) NOT NULL,
    [Cantidad]               INT             NOT NULL,
    [Produccion]             DECIMAL (18, 2) NOT NULL,
    [IdUsuarioCreacion]      NVARCHAR (450)  NOT NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450)  NULL,
    [FechaActualizacion]     DATETIME        NULL,
    CONSTRAINT [PK_DetPrdPchMaquinaC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetPrdPchMaquinaC_CatTipoMalla] FOREIGN KEY ([IdTipoMalla]) REFERENCES [cp].[CatTipoMalla] ([Id]),
    CONSTRAINT [FK_DetPrdPchMaquinaC_Maquinas] FOREIGN KEY ([IdMaquina]) REFERENCES [cp].[Maquinas] ([Id]),
    CONSTRAINT [FK_DetPrdPchMaquinaC_PrdMallaPch] FOREIGN KEY ([PrdMallaPchId]) REFERENCES [cp].[PrdMallaPch] ([Id]),
    CONSTRAINT [FK_DetPrdPchMaquinaC_TipoFabricacion] FOREIGN KEY ([IdTipoFabricacion]) REFERENCES [cp].[TipoFabricacion] ([Id])
);

