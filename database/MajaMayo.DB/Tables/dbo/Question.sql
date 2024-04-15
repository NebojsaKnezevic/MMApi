CREATE TABLE [dbo].[Question]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[OrderNo] int not null,
	[Text] nvarchar(max) not null,
	[AdditionalComment] bit not null,
	[QuestionGroupId] int not null,
	[ApplicableAgeLow] int not null,
	[ApplicableAgeHigh] int not null,
	[ApplicableMale] bit not null,
	[ApplicableFemale] bit not null,
	[IsActive] bit not null,
	[CreatedOn] datetime not null,
	[PictureId] int null,
	
    CONSTRAINT [PK_Q] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_Q_QG] FOREIGN KEY ([QuestionGroupId]) REFERENCES [dbo].[QuestionGroup] ([Id]),
    CONSTRAINT [FK_Q_P] FOREIGN KEY ([PictureId]) REFERENCES [dbo].[Picture] ([Id]),
)
