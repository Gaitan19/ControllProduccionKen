CREATE TABLE [cp].[PrdPaneladoraPch] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [IdUsuarios]             NVARCHAR (MAX)  NOT NULL,
    [IdMaquina]              INT             NOT NULL,
    [Fecha]                  DATE            NOT NULL,
    [Observaciones]          NVARCHAR (4000) NULL,
    [ProduccionDia]          DECIMAL (10, 1) NULL,
    [TiempoParo]             DECIMAL (10, 1) NULL,
    [IdUsuarioCreacion]      NVARCHAR (450)  NOT NULL,
    [FechaCreacion]          DATETIME        CONSTRAINT [DF_cp_PrdPaneladoraPch_FechaCreacion] DEFAULT (getdate()) NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450)  NULL,
    [FechaActualizacion]     DATETIME        NULL,
    [AprobadoSupervisor]     BIT             CONSTRAINT [DF_cp_PrdPaneladoraPch_AprobSup] DEFAULT ((0)) NOT NULL,
    [AprobadoGerencia]       BIT             CONSTRAINT [DF_cp_PrdPaneladoraPch_AprobGer] DEFAULT ((0)) NOT NULL,
    [IdAprobadoSupervisor]   NVARCHAR (450)  NULL,
    [IdAprobadoGerencia]     NVARCHAR (450)  NULL,
    CONSTRAINT [PK_cp_PrdPaneladoraPch] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_cp_PrdPaneladoraPch_NoNeg] CHECK (([ProduccionDia] IS NULL OR [ProduccionDia]>=(0)) AND ([TiempoParo] IS NULL OR [TiempoParo]>=(0))),
    CONSTRAINT [FK_cp_PrdPaneladoraPch_Maquinas] FOREIGN KEY ([IdMaquina]) REFERENCES [cp].[Maquinas] ([Id])
);

