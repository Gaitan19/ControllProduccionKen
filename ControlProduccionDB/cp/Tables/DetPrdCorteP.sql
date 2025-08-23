CREATE TABLE [cp].[DetPrdCorteP] (
    [Id]                        INT             IDENTITY (1, 1) NOT NULL,
    [PrdCortePId]               INT             NOT NULL,
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
    CONSTRAINT [PK_DetPrdCorteP] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetPrdCorteP_CatalogoTipo] FOREIGN KEY ([IdTipoFabricacion]) REFERENCES [cp].[CatalogoTipo] ([Id]),
    CONSTRAINT [FK_DetPrdCorteP_CatPantografo] FOREIGN KEY ([IdArticulo]) REFERENCES [cp].[CatPantografo] ([Id]),
    CONSTRAINT [FK_DetPrdCorteP_PrdCorteP] FOREIGN KEY ([PrdCortePId]) REFERENCES [cp].[PrdCorteP] ([Id])
);

