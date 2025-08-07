CREATE TABLE [cp].[DetAlambrePrdMallaCovintec] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [IdMalla]                INT             NOT NULL,
    [NumeroAlambre]          INT             NOT NULL,
    [PesoAlambre]            DECIMAL (18, 1) NOT NULL,
    [IdUsuarioCreacion]      NVARCHAR (450)  NOT NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450)  NULL,
    [FechaActualizacion]     DATETIME        NULL,
    [AprobadoSupervisor]     BIT             NOT NULL,
    [AprobadoGerencia]       BIT             NOT NULL,
    [IdAprobadoSupervisor]   NVARCHAR (450)  NULL,
    [IdAprobadoGerencia]     NVARCHAR (450)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetAlambrePrdMallaCovintec_Principal] FOREIGN KEY ([IdMalla]) REFERENCES [cp].[PrdMallaCovintec] ([Id])
);

