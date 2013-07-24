EXEC sp_changeobjectowner 'linkme_owner.NonMemberSettings', dbo
GO

ALTER TABLE dbo.NonMemberSettings
ALTER COLUMN emailAddress EmailAddress NOT NULL

ALTER TABLE dbo.NonMemberSettings
ALTER COLUMN flags NonMemberFlags NOT NULL

GO
