CREATE TABLE [cp].[DetAlambrePrdCerchaCovintec] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [IdCercha]               INT             NOT NULL,
    [NumeroAlambre]          INT             NOT NULL,
    [PesoAlambre]            DECIMAL (18, 1) NOT NULL,
    [AprobadoSupervisor]     BIT             NOT NULL,
    [AprobadoGerencia]       BIT             NOT NULL,
    [IdAprobadoSupervisor]   NVARCHAR (450)  NULL,
    [IdAprobadoGerencia]     NVARCHAR (450)  NULL,
    [IdUsuarioCreacion]      NVARCHAR (450)  NOT NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450)  NULL,
    [FechaActualizacion]     DATETIME        NULL,
    CONSTRAINT [PK_DetAlambrePrdCerchaCovintec] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetAlambrePrdCerchaCovintec_PrdCerchaCovintec] FOREIGN KEY ([IdCercha]) REFERENCES [cp].[PrdCerchaCovintec] ([Id])
);

