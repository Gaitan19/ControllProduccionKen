CREATE TABLE [cp].[MaestroCatalogo] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [IdPadre]     INT           NOT NULL,
    [Codigo]      VARCHAR (150) NULL,
    [Descripcion] VARCHAR (250) NOT NULL,
    [Activo]      BIT           NOT NULL,
    CONSTRAINT [PK_MaestroCatalogo] PRIMARY KEY CLUSTERED ([Id] ASC)
);

