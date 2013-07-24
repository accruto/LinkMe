EXEC sp_changeobjectowner 'linkme_owner.IntegratorUser', dbo
GO

ALTER TABLE [dbo].IntegratorUser
DROP CONSTRAINT UQ_IntegratorUser_username

ALTER TABLE dbo.IntegratorUser
ALTER COLUMN username LoginId NOT NULL

ALTER TABLE dbo.IntegratorUser
ADD CONSTRAINT UQ_IntegratorUser_username
UNIQUE (username)

ALTER TABLE dbo.IntegratorUser
ALTER COLUMN [password] PasswordHash NOT NULL

ALTER TABLE dbo.IntegratorUser
ALTER COLUMN [permissions] IntegratorPermissions NOT NULL

GO
