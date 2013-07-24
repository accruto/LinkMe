-- Create a view to maintain the loginId uniqueness

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[RegisteredUserLoginId]'))
DROP VIEW [dbo].[RegisteredUserLoginId]
GO

CREATE VIEW RegisteredUserLoginId WITH SCHEMABINDING
AS
SELECT id, loginId FROM dbo.RegisteredUser
WHERE loginId IS NOT NULL
GO

CREATE UNIQUE CLUSTERED INDEX UQ_RegisteredUser_loginId
ON RegisteredUserLoginId (loginId)
GO

-- Create a view to maintain email address uniqueness for members

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[RegisteredUserMemberEmailAddress]'))
DROP VIEW [dbo].[RegisteredUserMemberEmailAddress]
GO

CREATE VIEW RegisteredUserMemberEmailAddress WITH SCHEMABINDING
AS
SELECT U.id, emailAddress FROM dbo.RegisteredUser AS U
INNER JOIN dbo.Member AS M ON M.id = U.id
GO

CREATE UNIQUE CLUSTERED INDEX UQ_RegisteredUser_Member_emailAddress
ON RegisteredUserMemberEmailAddress (emailAddress)
GO

-- Create an index for email address

CREATE INDEX IX_RegisteredUser_emailAddress
ON RegisteredUser (emailAddress)
GO

