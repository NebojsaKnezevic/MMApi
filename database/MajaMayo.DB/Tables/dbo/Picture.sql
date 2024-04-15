CREATE TABLE [dbo].[Picture]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[Data] varbinary(max) not null,
	
    CONSTRAINT [PK_P] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
)
