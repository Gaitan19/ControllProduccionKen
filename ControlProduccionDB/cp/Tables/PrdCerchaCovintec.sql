CREATE TABLE [cp].[PrdCerchaCovintec] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [TiempoParo]             DECIMAL (18, 1) NULL,
    [IdTipoReporte]          INT             CONSTRAINT [DF_PrdCerchaCovintec_IdTipoReporte] DEFAULT ((7)) NULL,
    [IdUsuarios]             NVARCHAR (MAX)  NOT NULL,
    [IdMaquina]              INT             NOT NULL,
    [ConteoInicial]          INT             NOT NULL,
    [ConteoFinal]            INT             NOT NULL,
    [Fecha]                  DATE            NOT NULL,
    [MermaAlambre]           DECIMAL (18, 1) NULL,
    [Observaciones]          NVARCHAR (4000) NULL,
    [IdUsuarioCreacion]      NVARCHAR (450)  NOT NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450)  NULL,
    [FechaActualizacion]     DATETIME        NULL,
    [AprobadoSupervisor]     BIT             CONSTRAINT [DF_PrdCerchaCovintec_AprobadoSupervisor] DEFAULT ((0)) NOT NULL,
    [AprobadoGerencia]       BIT             CONSTRAINT [DF_PrdCerchaCovintec_AprobadoGerencia] DEFAULT ((0)) NOT NULL,
    [IdAprobadoSupervisor]   NVARCHAR (450)  NULL,
    [IdAprobadoGerencia]     NVARCHAR (450)  NULL,
    CONSTRAINT [PK_PrdCerchaCovintec] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PrdCerchaCovintec_Maquinas] FOREIGN KEY ([IdMaquina]) REFERENCES [cp].[Maquinas] ([Id])
);

