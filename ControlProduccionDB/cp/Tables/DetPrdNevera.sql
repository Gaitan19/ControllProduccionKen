CREATE TABLE [cp].[DetPrdNevera] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [PrdNeveraId]            INT             NOT NULL,
    [Posicion]               INT             NOT NULL,
    [IdArticulo]             INT             NOT NULL,
    [CantidadConforme]       DECIMAL (10, 1) NOT NULL,
    [CantidadNoConforme]     DECIMAL (10, 1) NOT NULL,
    [IdTipoFabricacion]      INT             NOT NULL,
    [NumeroPedido]           INT             NULL,
    [IdUsuarioCreacion]      NVARCHAR (450)  NOT NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450)  NULL,
    [FechaActualizacion]     DATETIME        NULL,
    CONSTRAINT [PK_DetPrdNevera] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetPrdNevera_MaestroCatalogo] FOREIGN KEY ([IdArticulo]) REFERENCES [cp].[MaestroCatalogo] ([Id]),
    CONSTRAINT [FK_DetPrdNevera_PrdNevera] FOREIGN KEY ([PrdNeveraId]) REFERENCES [cp].[PrdNevera] ([Id]),
    CONSTRAINT [FK_DetPrdNevera_TipoFabricacion] FOREIGN KEY ([IdTipoFabricacion]) REFERENCES [cp].[TipoFabricacion] ([Id])
);

