CREATE TABLE [cp].[CatEspesor] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Valor]  VARCHAR (15) NOT NULL,
    [Activo] BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

