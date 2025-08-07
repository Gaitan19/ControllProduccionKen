CREATE TABLE [cp].[Reporte] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]            VARCHAR (200) NOT NULL,
    [TablaReporte]      VARCHAR (200) NULL,
    [IdLineaProduccion] INT           NOT NULL,
    [Activo]            BIT           CONSTRAINT [DF__Reporte__Activo__300424B4] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK__Reporte__3214EC077329D102] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Reporte_LineaProduccion] FOREIGN KEY ([IdLineaProduccion]) REFERENCES [cp].[LineaProduccion] ([Id])
);

