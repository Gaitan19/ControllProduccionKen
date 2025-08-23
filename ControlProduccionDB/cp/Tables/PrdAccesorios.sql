CREATE TABLE [cp].[PrdAccesorios] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [IdUsuarios]             NVARCHAR (MAX)  NOT NULL,
    [Fecha]                  DATE            NOT NULL,
    [IdMaquina]              INT             NOT NULL,
    [Observaciones]          NVARCHAR (4000) NULL,
    [AprobadoSupervisor]     BIT             NOT NULL,
    [AprobadoGerencia]       BIT             NOT NULL,
    [IdAprobadoSupervisor]   NVARCHAR (450)  NULL,
    [IdAprobadoGerencia]     NVARCHAR (450)  NULL,
    [IdUsuarioCreacion]      NVARCHAR (450)  NOT NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450)  NULL,
    [FechaActualizacion]     DATETIME        NULL,
    [MermaMallaCovintecKg]   DECIMAL (10, 1) NULL,
    [MermaMallaPchKg]        DECIMAL (10, 1) NULL,
    [MermaBobinasKg]         DECIMAL (10, 1) NULL,
    [MermaLitewallKg]        DECIMAL (10, 1) NULL,
    CONSTRAINT [PK_PrdAccesorios] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PrdAccesorios_Maquinas] FOREIGN KEY ([IdMaquina]) REFERENCES [cp].[Maquinas] ([Id])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_PrdAccesorios_IdMaquina]
    ON [cp].[PrdAccesorios]([IdMaquina] ASC);

