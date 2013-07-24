ALTER TABLE linkme_owner.SavedJobSearch
ADD displayText NVARCHAR(2000)
GO

UPDATE linkme_owner.SavedJobSearch
SET displayText = ''
WHERE displayText IS NULL

ALTER TABLE linkme_owner.SavedJobSearch
ALTER COLUMN displayText NVARCHAR(2000) NOT NULL
GO
