CREATE TABLE [cp].[PrdBloques] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [IdUsuarios]             NVARCHAR (MAX) NOT NULL,
    [Fecha]                  DATE           NOT NULL,
    [AprobadoSupervisor]     BIT            NOT NULL,
    [AprobadoGerencia]       BIT            NOT NULL,
    [IdAprobadoSupervisor]   NVARCHAR (450) NULL,
    [IdAprobadoGerencia]     NVARCHAR (450) NULL,
    [IdUsuarioCreacion]      NVARCHAR (450) NOT NULL,
    [FechaCreacion]          DATETIME       NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450) NULL,
    [FechaActualizacion]     DATETIME       NULL,
    CONSTRAINT [PK_PrdBloques] PRIMARY KEY CLUSTERED ([Id] ASC)
);

