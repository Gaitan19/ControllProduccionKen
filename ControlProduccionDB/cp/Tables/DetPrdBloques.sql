CREATE TABLE [cp].[DetPrdBloques] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [PrdBloqueId]            INT             NOT NULL,
    [IdMaquina]              INT             NOT NULL,
    [IdCatBloque]            INT             NOT NULL,
    [ProduccionDia]          DECIMAL (10, 1) NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioCreacion]      NVARCHAR (450)  NOT NULL,
    [FechaActualizacion]     DATETIME        NULL,
    [IdUsuarioActualizacion] NVARCHAR (450)  NULL,
    CONSTRAINT [PK_DetPrdBloques] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetPrdBloques_CatalogoBloques] FOREIGN KEY ([IdCatBloque]) REFERENCES [cp].[CatalogoBloques] ([Id]),
    CONSTRAINT [FK_DetPrdBloques_Maquinas] FOREIGN KEY ([IdMaquina]) REFERENCES [cp].[Maquinas] ([Id]),
    CONSTRAINT [FK_DetPrdBloques_PrdBloques] FOREIGN KEY ([PrdBloqueId]) REFERENCES [cp].[PrdBloques] ([Id])
);

