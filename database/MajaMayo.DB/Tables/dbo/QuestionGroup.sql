CREATE TABLE [dbo].[QuestionGroup]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[Name] nvarchar(max) not null,
	[ApplicableAgeLow] int not null,
	[ApplicableAgeHigh] int not null,
	[ApplicableMale] bit not null,
	[ApplicableFemale] bit not null,
	[IsActive] bit not null,
	[InLevel] int not null,
	[ParentId] int null,
	[CreatedOn] datetime not null
	
    CONSTRAINT [PK_QG] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_QG_QG] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[QuestionGroup] ([Id]),
)
