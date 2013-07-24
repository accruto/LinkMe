ALTER TABLE linkme_owner.IntegratorUser
ADD [permissions] INT NULL
GO

-- Grant PostJobAds and GetJobApplication to existing users
UPDATE linkme_owner.IntegratorUser
SET [permissions] = 3
GO

ALTER TABLE linkme_owner.IntegratorUser
ALTER COLUMN [permissions] INT NOT NULL
GO
