CREATE TABLE [dbo].[HealthAssesmentAnswers]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[HealthAssesmentId] int not null,
	[AnswerId] int not null,
	[AdditinalComment] nvarchar(max) null,
	[Timestamp] datetime not null,
	
    CONSTRAINT [PK_HAA] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_HAA_HA] FOREIGN KEY ([HealthAssesmentId]) REFERENCES [dbo].[HealthAssesment] ([Id]),
    CONSTRAINT [FK_HAA_A] FOREIGN KEY ([AnswerId]) REFERENCES [dbo].[Answer] ([Id]),
)
