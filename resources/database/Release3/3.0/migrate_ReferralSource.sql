EXEC sp_changeobjectowner 'linkme_owner.ReferralSource', dbo
GO

-- Migrate GUID id to an incremental tinyint id.

EXEC sp_rename 'dbo.ReferralSource.id', '_id', 'COLUMN'
GO

ALTER TABLE dbo.ReferralSource
ADD [id] TINYINT NULL
GO

DECLARE @count TINYINT
SET @count = 0

UPDATE dbo.ReferralSource
SET @count = @count + 1, [id] = @count
FROM dbo.ReferralSource

UPDATE dbo.ReferralSource
SET [id] = 0
WHERE displayName = 'Other'

ALTER TABLE dbo.ReferralSource
ALTER COLUMN [id] TINYINT NOT NULL
GO

-- Change the primary key to be the new (tinyint) id

ALTER TABLE linkme_owner.networker_profile
DROP CONSTRAINT FK_referralSourceId
GO

ALTER TABLE dbo.ReferralSource
DROP CONSTRAINT PK_ReferralSource
GO

ALTER TABLE dbo.ReferralSource
ADD CONSTRAINT PK_ReferralSource
PRIMARY KEY CLUSTERED ([id])
GO
