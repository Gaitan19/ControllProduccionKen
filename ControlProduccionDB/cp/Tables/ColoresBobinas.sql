CREATE TABLE [cp].[ColoresBobinas] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Color]  VARCHAR (50) NOT NULL,
    [RAL]    VARCHAR (10) NULL,
    [Activo] BIT          CONSTRAINT [DF_ColoresBobinas_Activo] DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

