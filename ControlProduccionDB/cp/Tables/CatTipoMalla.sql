CREATE TABLE [cp].[CatTipoMalla] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Cuadricula]  NVARCHAR (100) NOT NULL,
    [PesoPorMts2] NVARCHAR (50)  NULL,
    [Activo]      BIT            CONSTRAINT [DF_CatTipoMalla_Activo] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CatTipoMalla] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_CatTipoMalla_Cuadricula] UNIQUE NONCLUSTERED ([Cuadricula] ASC)
);

