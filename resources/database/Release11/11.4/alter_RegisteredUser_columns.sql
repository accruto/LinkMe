ALTER TABLE dbo.RegisteredUser
ADD emailAddressVerified BIT NULL
GO

UPDATE
	dbo.RegisteredUser
SET
	emailAddressVerified = 1
GO

UPDATE
	dbo.RegisteredUser
SET
	emailAddressVerified = 0
WHERE
	(flags & 8) = 8
GO

ALTER TABLE dbo.RegisteredUser
ALTER COLUMN emailAddressVerified BIT NOT NULL
GO

ALTER TABLE dbo.RegisteredUser
ADD secondaryEmailAddress EmailAddress NULL
GO

ALTER TABLE dbo.RegisteredUser
ADD secondaryEmailAddressVerified BIT NULL
GO

