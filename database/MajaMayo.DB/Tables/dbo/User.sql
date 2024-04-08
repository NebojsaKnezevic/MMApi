CREATE TABLE [dbo].[User]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[FirstName] nvarchar(100) not null,
	[LastName] nvarchar(100) not null,
	[Email] nvarchar(100) not null,
	[PhoneNumber] nvarchar(100) not null,
	[DateOfBirth] date not null,
	[RegisteredOn] datetime not null,
	--insurance policy number?
	--email validation?
	--phone number validation?
		
    CONSTRAINT [PK_U] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
)
