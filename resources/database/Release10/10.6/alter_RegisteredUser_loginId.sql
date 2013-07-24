ALTER TABLE dbo.RegisteredUser
ALTER COLUMN loginId EmailAddress NULL
GO

ALTER TABLE dbo.RegisteredUser
ALTER COLUMN passwordHash PasswordHash NULL
GO

-- Create a view to maintain the loginId uniqueness

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RegisteredUser]') AND name = N'UQ_RegisteredUser_loginId')
DROP INDEX UQ_RegisteredUser_loginId ON dbo.RegisteredUser
GO

CREATE VIEW RegisteredUserLoginId WITH SCHEMABINDING
AS
SELECT loginId FROM dbo.RegisteredUser
WHERE loginId IS NOT NULL
GO

CREATE UNIQUE CLUSTERED INDEX UQ_RegisteredUser_loginId
ON RegisteredUserLoginId (loginId)
GO

-- Create a view to maintain email address uniqueness for members

CREATE VIEW RegisteredUserMemberEmailAddress WITH SCHEMABINDING
AS
SELECT emailAddress FROM dbo.RegisteredUser AS U
INNER JOIN dbo.Member AS M ON M.id = U.id
GO

CREATE UNIQUE CLUSTERED INDEX UQ_RegisteredUser_Member_emailAddress
ON RegisteredUserMemberEmailAddress (emailAddress)
GO

