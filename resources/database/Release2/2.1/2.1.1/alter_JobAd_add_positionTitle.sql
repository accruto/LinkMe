/*
ALTER TABLE linkme_owner.JobAd
DROP COLUMN positionTitle
*/

ALTER TABLE linkme_owner.JobAd
ADD positionTitle NVARCHAR(200)
GO

UPDATE linkme_owner.JobAd
SET positionTitle = title
GO

ALTER TABLE linkme_owner.JobAd
ALTER COLUMN positionTitle NVARCHAR(200) NOT NULL
GO
