ALTER TABLE dbo.Vertical
ADD enabled BIT NULL
GO

UPDATE
	dbo.Vertical
SET
	enabled = 1
GO

-- Make the column not null

ALTER TABLE dbo.Vertical
ALTER COLUMN enabled BIT NOT NULL
GO

