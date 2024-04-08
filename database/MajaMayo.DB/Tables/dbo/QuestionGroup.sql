CREATE TABLE [dbo].[QuestionGroup]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[Name] nvarchar(max) not null,
	[IsActive] bit not null,
	[CreatedOn] datetime not null
	
    CONSTRAINT [PK_QG] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
)
