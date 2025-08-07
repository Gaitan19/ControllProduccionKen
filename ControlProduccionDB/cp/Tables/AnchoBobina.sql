CREATE TABLE [cp].[AnchoBobina] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [Valor]  INT NOT NULL,
    [Activo] BIT DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

