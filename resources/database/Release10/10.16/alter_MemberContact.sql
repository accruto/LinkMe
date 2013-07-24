ALTER TABLE dbo.MemberContact
ADD partOfBulk BIT NULL
GO

-- Not ideal but for existing data set it to the current time.

UPDATE
	dbo.MemberContact
SET
	partOfBulk = 0
WHERE
	partOfBulk IS NULL
GO

-- Make the column not null

ALTER TABLE dbo.MemberContact
ALTER COLUMN partOfBulk BIT NOT NULL
GO

