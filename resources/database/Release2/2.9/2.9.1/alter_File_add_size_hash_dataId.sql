ALTER TABLE linkme_owner.[File]
ADD sizeBytes int, [hash] binary(16), dataId UNIQUEIDENTIFIER
GO

-- Default "dataId" to "id", since that's the way "id" has been used until now.
UPDATE linkme_owner.[File]
SET dataId = [id]
GO

ALTER TABLE linkme_owner.[File]
ALTER COLUMN dataId UNIQUEIDENTIFIER NOT NULL
GO
