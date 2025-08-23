CREATE TABLE [cp].[CatalogoAccesorios] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [FamiliaProductos]    VARCHAR (50)  NOT NULL,
    [DescripcionArticulo] VARCHAR (200) NOT NULL,
    [CodigoArticulo]      VARCHAR (50)  NOT NULL,
    [IdTipo]              INT           NOT NULL,
    [Activo]              BIT           CONSTRAINT [DF_CatalogoAccesorios_Activo] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CatalogoAccesorios] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_CatalogoAccesorios_IdTipo] CHECK ([IdTipo]=(28) OR [IdTipo]=(27) OR [IdTipo]=(26) OR [IdTipo]=(25)),
    CONSTRAINT [FK_CatalogoAccesorios_MaestroCatalogo_IdTipo] FOREIGN KEY ([IdTipo]) REFERENCES [cp].[MaestroCatalogo] ([Id])
);

