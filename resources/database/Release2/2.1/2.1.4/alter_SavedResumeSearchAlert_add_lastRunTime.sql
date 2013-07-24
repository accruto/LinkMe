ALTER TABLE linkme_owner.SavedResumeSearchAlert
ADD lastRunTime datetime 
GO

UPDATE linkme_owner.SavedResumeSearchAlert
SET lastRunTime = '09/29/2006'
GO

ALTER TABLE linkme_owner.SavedResumeSearchAlert
ALTER COLUMN lastRunTime datetime NOT NULL
GO

