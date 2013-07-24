ALTER TABLE dbo.ParsedResume
ADD phoneNumberType TINYINT NULL
GO

ALTER TABLE dbo.ParsedResume
ADD secondaryPhoneNumber NVARCHAR(20) NULL
GO

ALTER TABLE dbo.ParsedResume
ADD secondaryPhoneNumberType TINYINT NULL
GO

UPDATE
	dbo.ParsedResume
SET
	phoneNumberType = 3
WHERE
	NOT phoneNumber IS NULL
GO

ALTER TABLE dbo.ParsedResume
ADD secondaryEmailAddress NVARCHAR(320) NULL
GO

