CREATE TABLE [cp].[DetPrdCorteT] (
    [Id]                        INT             IDENTITY (1, 1) NOT NULL,
    [PrdCorteTId]               INT             NOT NULL,
    [No]                        INT             NOT NULL,
    [IdArticulo]                INT             NOT NULL,
    [NumeroPedido]              INT             NULL,
    [IdTipoFabricacion]         INT             NOT NULL,
    [PrdCodigoBloque]           VARCHAR (200)   NOT NULL,
    [IdDensidad]                INT             NOT NULL,
    [IdTipoBloque]              INT             NOT NULL,
    [CantidadPiezasConformes]   DECIMAL (10, 1) NOT NULL,
    [CantidadPiezasNoConformes] DECIMAL (10, 1) NOT NULL,
    [Nota]                      VARCHAR (200)   NULL,
    [IdUsuarioCreacion]         NVARCHAR (450)  NOT NULL,
    [FechaCreacion]             DATETIME        NOT NULL,
    [IdUsuarioActualizacion]    NVARCHAR (450)  NULL,
    [FechaActualizacion]        DATETIME        NULL,
    CONSTRAINT [PK_DetPrdCorteT] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetPrdCorteT_CatalogoTipo] FOREIGN KEY ([IdTipoFabricacion]) REFERENCES [cp].[CatalogoTipo] ([Id]),
    CONSTRAINT [FK_DetPrdCorteT_CatLaminas] FOREIGN KEY ([Id]) REFERENCES [cp].[CatLaminas] ([Id]),
    CONSTRAINT [FK_DetPrdCorteT_DetPrdCorteT] FOREIGN KEY ([Id]) REFERENCES [cp].[DetPrdCorteT] ([Id]),
    CONSTRAINT [FK_DetPrdCorteT_PrdCorteT] FOREIGN KEY ([PrdCorteTId]) REFERENCES [cp].[PrdCorteT] ([Id])
);

