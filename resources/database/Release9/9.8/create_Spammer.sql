-- First rename this table.

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('BlogSpamText') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
	EXEC sp_rename 'dbo.BlogSpamText', 'SpamText'
GO

-- Create the spammer table.

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Spammer') AND type in (N'U'))
DROP TABLE dbo.Spammer
GO

CREATE TABLE dbo.Spammer
(
	firstName NVARCHAR(200) NOT NULL,
	lastName NVARCHAR(200) NOT NULL
)
GO

ALTER TABLE dbo.Spammer
ADD CONSTRAINT PK_Spammer PRIMARY KEY NONCLUSTERED
(
	firstName,
	lastName
)
GO

INSERT dbo.Spammer (firstName, lastName)
VALUES ('Mariam', 'Ture')
GO

INSERT dbo.Spammer (firstName, lastName)
VALUES ('Lilian', 'Dikko')
GO

INSERT dbo.Spammer (firstName, lastName)
VALUES ('Boris', 'Kone')
GO

INSERT dbo.Spammer (firstName, lastName)
VALUES ('Achi', 'Chantal')
GO

