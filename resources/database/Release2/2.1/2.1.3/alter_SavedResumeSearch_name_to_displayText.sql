ALTER TABLE linkme_owner.SavedResumeSearch
DROP CONSTRAINT UQ_SavedResumeSearch_ownerId_name
GO

ALTER TABLE linkme_owner.SavedResumeSearch
ADD displayText NVARCHAR(2000)
GO

UPDATE linkme_owner.SavedResumeSearch
SET displayText = [name]
GO

ALTER TABLE linkme_owner.SavedResumeSearch
ALTER COLUMN displayText NVARCHAR(2000) NOT NULL
GO

ALTER TABLE linkme_owner.SavedResumeSearch
DROP COLUMN [name]
GO
