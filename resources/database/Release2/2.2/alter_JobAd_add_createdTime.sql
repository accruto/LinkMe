ALTER TABLE linkme_owner.JobAd
ADD createdTime DATETIME
GO

-- Initialise the created from the expiry time. Should work since all job ads currently
-- have the same duration.
UPDATE linkme_owner.JobAd
SET createdTime = DATEADD(Day, -30, expiryTime)
GO

ALTER TABLE linkme_owner.JobAd
ALTER COLUMN createdTime DATETIME NOT NULL
GO
