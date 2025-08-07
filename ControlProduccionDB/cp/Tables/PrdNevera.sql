CREATE TABLE [cp].[PrdNevera] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [TiempoParo]             DECIMAL (18, 1) NULL,
    [IdTipoReporte]          INT             NULL,
    [IdUsuarios]             NVARCHAR (MAX)  NOT NULL,
    [IdMaquina]              INT             NOT NULL,
    [Fecha]                  DATE            NOT NULL,
    [HoraInicio]             TIME (7)        NOT NULL,
    [HoraFin]                TIME (7)        NOT NULL,
    [Observaciones]          NVARCHAR (4000) NULL,
    [IdUsuarioCreacion]      NVARCHAR (450)  NOT NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450)  NULL,
    [FechaActualizacion]     DATETIME        NULL,
    [AprobadoSupervisor]     BIT             NOT NULL,
    [AprobadoGerencia]       BIT             NOT NULL,
    [IdAprobadoSupervisor]   NVARCHAR (450)  NULL,
    [IdAprobadoGerencia]     NVARCHAR (450)  NULL,
    CONSTRAINT [PK_PrdNevera] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PrdNevera_Maquinas] FOREIGN KEY ([IdMaquina]) REFERENCES [cp].[Maquinas] ([Id])
);

