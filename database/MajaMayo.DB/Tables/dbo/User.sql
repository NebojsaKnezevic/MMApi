CREATE TABLE [dbo].[User]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[FirstName] nvarchar(100) not null,
	[LastName] nvarchar(100) not null,
	[Email] nvarchar(100) not null,
	[PhoneNumber] nvarchar(100) not null,
	[DateOfBirth] date not null,
	[Gender] nvarchar(100) not null, -- constraint values
	[Password] nvarchar(100) not null, --hashirano proveri lenght
	[RegisteredOn] datetime not null,
	[PolicyNumber] nvarchar(100) null,
	[JMBG] nvarchar(13) null,
	[PassportNumber] nvarchar(20) null,
	--email validation?
	--phone number validation?
		
    CONSTRAINT [PK_U] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
)
