ALTER TABLE dbo.Member
ADD dateOfBirthParts TINYINT NULL
GO

UPDATE
	dbo.Member
SET
	dateOfBirthParts = 3
WHERE
	NOT dateOfBirth IS NULL
GO

ALTER TABLE dbo.Member
ADD primaryPhoneNumber PhoneNumber NULL
GO

ALTER TABLE dbo.Member
ADD primaryPhoneNumberType TINYINT NULL
GO

ALTER TABLE dbo.Member
ADD secondaryPhoneNumber PhoneNumber NULL
GO

ALTER TABLE dbo.Member
ADD secondaryPhoneNumberType TINYINT NULL
GO

ALTER TABLE dbo.Member
ADD tertiaryPhoneNumber PhoneNumber NULL
GO

ALTER TABLE dbo.Member
ADD tertiaryPhoneNumberType TINYINT NULL
GO

-- Clean up some data first

UPDATE
	dbo.Member
SET
	mobilePhoneNumber = NULL
WHERE
	mobilePhoneNumber = ''

UPDATE
	dbo.Member
SET
	homePhoneNumber = NULL
WHERE
	homePhoneNumber = ''

UPDATE
	dbo.Member
SET
	workPhoneNumber = NULL
WHERE
	workPhoneNumber = ''

-- Set the primary phone number.

UPDATE
	dbo.Member
SET
	primaryPhoneNumber = mobilePhoneNumber,
	primaryPhoneNumberType = 3
WHERE
	NOT mobilePhoneNumber IS NULL

UPDATE
	dbo.Member
SET
	primaryPhoneNumber = homePhoneNumber,
	primaryPhoneNumberType = 1
WHERE
	mobilePhoneNumber IS NULL
	AND NOT homePhoneNumber IS NULL

UPDATE
	dbo.Member
SET
	primaryPhoneNumber = workPhoneNumber,
	primaryPhoneNumberType = 2
WHERE
	mobilePhoneNumber IS NULL
	AND homePhoneNumber IS NULL
	AND NOT workPhoneNumber IS NULL

-- Set the secondary phone number.

UPDATE
	dbo.Member
SET
	secondaryPhoneNumber = homePhoneNumber,
	secondaryPhoneNumberType = 1
WHERE
	NOT mobilePhoneNumber IS NULL
	AND NOT homePhoneNumber IS NULL

UPDATE
	dbo.Member
SET
	secondaryPhoneNumber = workPhoneNumber,
	secondaryPhoneNumberType = 2
WHERE
	NOT mobilePhoneNumber IS NULL
	AND homePhoneNumber IS NULL
	AND NOT workPhoneNumber IS NULL

UPDATE
	dbo.Member
SET
	secondaryPhoneNumber = workPhoneNumber,
	secondaryPhoneNumberType = 2
WHERE
	mobilePhoneNumber IS NULL
	AND NOT homePhoneNumber IS NULL
	AND NOT workPhoneNumber IS NULL

-- Set the tertiary phone number

UPDATE
	dbo.Member
SET
	tertiaryPhoneNumber = workPhoneNumber,
	tertiaryPhoneNumberType = 2
WHERE
	NOT mobilePhoneNumber IS NULL
	AND NOT homePhoneNumber IS NULL
	AND NOT workPhoneNumber IS NULL

