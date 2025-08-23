CREATE TABLE [cp].[DetPrdOtro] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [PrdOtroId]              INT             NOT NULL,
    [Actividad]              NVARCHAR (4000) NOT NULL,
    [DescripcionProducto]    NVARCHAR (4000) NOT NULL,
    [IdTipoFabricacion]      INT             NOT NULL,
    [NumeroPedido]           INT             NULL,
    [Nota]                   NVARCHAR (4000) NOT NULL,
    [Merma]                  NVARCHAR (4000) NOT NULL,
    [Comentario]             NVARCHAR (4000) NOT NULL,
    [HoraInicio]             TIME (7)        NOT NULL,
    [HoraFin]                TIME (7)        NOT NULL,
    [IdUsuarioCreacion]      NVARCHAR (450)  NOT NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioActualizacion] NVARCHAR (450)  NULL,
    [FechaActualizacion]     DATETIME        NULL,
    CONSTRAINT [PK_DetPrdOtro] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetPrdOtro_PrdOtro] FOREIGN KEY ([PrdOtroId]) REFERENCES [cp].[PrdOtro] ([Id]),
    CONSTRAINT [FK_DetPrdOtro_TipoFabricacion] FOREIGN KEY ([IdTipoFabricacion]) REFERENCES [cp].[TipoFabricacion] ([Id])
);

