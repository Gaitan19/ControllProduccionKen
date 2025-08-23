CREATE TABLE [cp].[PrdOtro] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [IdUsuarios]             NVARCHAR (MAX) NOT NULL,
    [Fecha]                  DATE           NOT NULL,
    [FechaCreacion]          DATETIME       NOT NULL,
    [IdUsuarioCreacion]      NVARCHAR (450) NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450) NULL,
    [FechaActualizacion]     DATETIME       NULL,
    [AprobadoSupervisor]     BIT            NOT NULL,
    [AprobadoGerencia]       BIT            NOT NULL,
    [IdAprobadoSupervisor]   NVARCHAR (450) NULL,
    [IdAprobadoGerencia]     NVARCHAR (450) NULL,
    [IdTipoReporte]          INT            NULL,
    CONSTRAINT [PK_PrdOtros] PRIMARY KEY CLUSTERED ([Id] ASC)
);

