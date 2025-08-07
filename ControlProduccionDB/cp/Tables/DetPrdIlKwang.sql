CREATE TABLE [cp].[DetPrdIlKwang] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [PrdIlKwangId]           INT             NOT NULL,
    [Posicion]               INT             NOT NULL,
    [IdEspesor]              INT             NOT NULL,
    [Cantidad]               INT             NOT NULL,
    [Medida]                 DECIMAL (10, 2) NOT NULL,
    [MetrosCuadrados]        AS              (CONVERT([decimal](10,2),[Cantidad])*[Medida]) PERSISTED,
    [IdStatus]               INT             NOT NULL,
    [IdTipo]                 INT             NOT NULL,
    [IdUsuarioCreacion]      NVARCHAR (450)  NOT NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450)  NULL,
    [FechaActualizacion]     DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetPrdIlKwang_DetPrdIlKwang_status] FOREIGN KEY ([IdStatus]) REFERENCES [cp].[CatalogoStatus] ([Id]),
    CONSTRAINT [FK_DetPrdIlKwang_DetPrdIlKwang_tipo] FOREIGN KEY ([IdTipo]) REFERENCES [cp].[CatalogoTipo] ([Id]),
    CONSTRAINT [FK_PrdIlKwangDetalle_PrdIlKwang_Spesor] FOREIGN KEY ([IdEspesor]) REFERENCES [cp].[CatEspesor] ([Id])
);

