CREATE TABLE [dbo].[QuestionSubgroup]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[Name] nvarchar(max) not null,
	[GroupId] int not null,
	[IsActive] bit not null,
	[CreatedOn] datetime not null
	
    CONSTRAINT [PK_QS] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_QS_QG] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[QuestionGroup] ([Id]),
)
