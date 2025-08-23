CREATE TABLE [cp].[DetPrdpreExpansion] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [PrdpreExpansionId]      INT             NOT NULL,
    [Hora]                   TIME (7)        NOT NULL,
    [NoBatch]                INT             NOT NULL,
    [DensidadEsperada]       INT             NOT NULL,
    [PesoBatchGr]            DECIMAL (10, 1) NOT NULL,
    [Densidad]               DECIMAL (10, 2) NOT NULL,
    [KgPorBatch]             INT             NOT NULL,
    [PresionPSI]             INT             NOT NULL,
    [TiempoBatchSeg]         INT             NOT NULL,
    [TemperaturaC]           INT             NOT NULL,
    [Silo]                   INT             NOT NULL,
    [Paso]                   INT             NULL,
    [IdUsuarioCreacion]      NVARCHAR (450)  NOT NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450)  NULL,
    [FechaActualizacion]     DATETIME        NULL,
    CONSTRAINT [PK_DetPrdpreExpansion] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetPrdpreExpansion_PrdpreExpansion] FOREIGN KEY ([PrdpreExpansionId]) REFERENCES [cp].[PrdpreExpansion] ([Id]) ON DELETE CASCADE
);

