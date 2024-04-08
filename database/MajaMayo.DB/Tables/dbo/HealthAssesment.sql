CREATE TABLE [dbo].[HealthAssesment]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[UserId] int not null,
	[StartedOn] datetime not null,
	[CompletedOn] datetime null
	
    CONSTRAINT [PK_HA] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_HA_U] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
)
