ALTER TABLE dbo.Member
ADD lastEditedTime DATETIME NULL
GO

UPDATE
	dbo.Member
SET
	lastEditedTime = u.createdTime
FROM
	dbo.Member AS m
INNER JOIN
	dbo.RegisteredUser AS u ON u.id = m.id
WHERE
	lastEditedTime IS NULL
GO

ALTER TABLE dbo.Member
ALTER COLUMN lastEditedTime DATETIME NOT NULL
GO
