CREATE TABLE [dbo].[Answer]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[QuestionId] int not null,
	[OrderNo] int not null,
	[Text] nvarchar(max) not null,
	[IsActive] bit not null,
	[CreatedOn] datetime not null,
	
    CONSTRAINT [PK_A] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_A_Q] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question] ([Id]),
)
