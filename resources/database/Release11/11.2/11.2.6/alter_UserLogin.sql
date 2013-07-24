ALTER TABLE dbo.UserLogin
ADD sessionId NVARCHAR(100) NULL
GO

ALTER TABLE dbo.UserLogin
ADD activityType INT NULL
GO

-- Set all to 0 to signify login.

UPDATE
	dbo.UserLogin
SET
	activityType = 0

-- Make not null.

ALTER TABLE dbo.UserLogin
ALTER COLUMN activityType INT NOT NULL
GO

ALTER TABLE dbo.UserLogin
ADD authenticationStatus INT NULL
GO

UPDATE
	dbo.UserLogin
SET
	authenticationStatus = 1
WHERE
	admin = 0

UPDATE
	dbo.UserLogin
SET
	authenticationStatus = 3
WHERE
	admin = 1

GO

ALTER TABLE dbo.UserLogin
DROP COLUMN admin