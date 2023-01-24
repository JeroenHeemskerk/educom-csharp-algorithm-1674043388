CREATE TABLE [dbo].[move] (
    [Id]          INT           NOT NULL IDENTITY(1,1),
    [Name]        VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (400) NOT NULL,
    [SweatRate]   INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

