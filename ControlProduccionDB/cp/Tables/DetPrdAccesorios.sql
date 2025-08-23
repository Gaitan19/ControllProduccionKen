CREATE TABLE [cp].[DetPrdAccesorios] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [PrdAccesoriosId]        INT             NOT NULL,
    [IdTipoArticulo]         INT             NOT NULL,
    [IdArticulo]             INT             NOT NULL,
    [IdTipoFabricacion]      INT             NOT NULL,
    [NumeroPedido]           INT             NULL,
    [IdMallaCovintec]        INT             NULL,
    [CantidadMallaUn]        INT             NULL,
    [IdTipoMallaPCH]         INT             NULL,
    [CantidadPchKg]          DECIMAL (10, 1) NULL,
    [IdAnchoBobina]          INT             NULL,
    [CantidadBobinaKg]       DECIMAL (10, 1) NULL,
    [IdCalibre]              INT             NULL,
    [CantidadCalibreKg]      DECIMAL (10, 1) NULL,
    [IdUsuarioCreacion]      NVARCHAR (450)  NOT NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450)  NULL,
    [FechaActualizacion]     DATETIME        NULL,
    CONSTRAINT [PK_DetPrdAccesorios] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetPrdAccesorios_AnchoBobina] FOREIGN KEY ([IdAnchoBobina]) REFERENCES [cp].[AnchoBobina] ([Id]),
    CONSTRAINT [FK_DetPrdAccesorios_CatalogoAccesorios] FOREIGN KEY ([IdArticulo]) REFERENCES [cp].[CatalogoAccesorios] ([Id]),
    CONSTRAINT [FK_DetPrdAccesorios_CatalogoMallasCovintec] FOREIGN KEY ([IdMallaCovintec]) REFERENCES [cp].[CatalogoMallasCovintec] ([Id]),
    CONSTRAINT [FK_DetPrdAccesorios_CatTipoMalla] FOREIGN KEY ([IdTipoMallaPCH]) REFERENCES [cp].[CatTipoMalla] ([Id]),
    CONSTRAINT [FK_DetPrdAccesorios_MaestroCatalogo] FOREIGN KEY ([IdTipoArticulo]) REFERENCES [cp].[MaestroCatalogo] ([Id]),
    CONSTRAINT [FK_DetPrdAccesorios_MaestroCatalogo1] FOREIGN KEY ([IdCalibre]) REFERENCES [cp].[MaestroCatalogo] ([Id]),
    CONSTRAINT [FK_DetPrdAccesorios_TipoFabricacion] FOREIGN KEY ([IdTipoFabricacion]) REFERENCES [cp].[TipoFabricacion] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_DetPrdAcc_IdCalibre]
    ON [cp].[DetPrdAccesorios]([IdCalibre] ASC) WHERE ([IdCalibre] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_DetPrdAcc_IdAnchoBobina]
    ON [cp].[DetPrdAccesorios]([IdAnchoBobina] ASC) WHERE ([IdAnchoBobina] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_DetPrdAcc_IdTipoMallaPCH]
    ON [cp].[DetPrdAccesorios]([IdTipoMallaPCH] ASC) WHERE ([IdTipoMallaPCH] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_DetPrdAcc_IdMallaCovintec]
    ON [cp].[DetPrdAccesorios]([IdMallaCovintec] ASC) WHERE ([IdMallaCovintec] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_DetPrdAcc_PrdAccesoriosId]
    ON [cp].[DetPrdAccesorios]([PrdAccesoriosId] ASC);

