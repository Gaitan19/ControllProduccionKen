CREATE TABLE [cp].[ErrorLogs] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [TimeUtc]       DATETIME        CONSTRAINT [DF_ErrorLog_TimeUtc] DEFAULT (getdate()) NOT NULL,
    [Level]         NVARCHAR (32)   NULL,
    [Message]       NVARCHAR (4000) NULL,
    [Exception]     NVARCHAR (MAX)  NULL,
    [StackTrace]    NVARCHAR (MAX)  NULL,
    [RequestPath]   NVARCHAR (2048) NULL,
    [RequestMethod] NVARCHAR (16)   NULL,
    [QueryString]   NVARCHAR (2048) NULL,
    [RequestBody]   NVARCHAR (MAX)  NULL,
    [UserId]        NVARCHAR (450)  NULL,
    [IPAddress]     NVARCHAR (45)   NULL,
    [UserAgent]     NVARCHAR (512)  NULL,
    [CorrelationId] NVARCHAR (100)  NULL,
    CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

