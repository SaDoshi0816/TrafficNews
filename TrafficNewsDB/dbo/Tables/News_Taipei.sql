CREATE TABLE [dbo].[News_Taipei] (
    [ID]         DECIMAL (18)    IDENTITY (1, 1) NOT NULL,
    [chtmessage] NVARCHAR (100)  NULL,
    [engmessage] NVARCHAR (100)  NULL,
    [starttime]  VARCHAR (14)    NULL,
    [endtime]    VARCHAR (14)    NULL,
    [updatetime] VARCHAR (14)    NULL,
    [content]    NVARCHAR (1000) NULL,
    [url]        NVARCHAR (100)  NULL
);

