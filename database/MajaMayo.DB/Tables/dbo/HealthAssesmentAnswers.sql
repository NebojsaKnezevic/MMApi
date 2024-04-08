CREATE TABLE [dbo].[HealthAssesmentAnswers]
(
	-- mozda mozemo da napravimo ovu tabelu kao simple many-to-many, ako je lakse za EntityFramework
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[QuestionId] int not null, -- redudantno, imamo vec u Answer
	[HealthAssesmentId] int not null,
	[AnswerId] int not null,
	[AdditinalComment] nvarchar(max) null,
	--timestamp?
	
    CONSTRAINT [PK_HAA] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_HAA_HA] FOREIGN KEY ([HealthAssesmentId]) REFERENCES [dbo].[HealthAssesment] ([Id]),
    CONSTRAINT [FK_HAA_Q] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question] ([Id]),
    CONSTRAINT [FK_HAA_A] FOREIGN KEY ([AnswerId]) REFERENCES [dbo].[Answer] ([Id]),
)
