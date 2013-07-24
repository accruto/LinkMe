ALTER TABLE linkme_owner.[File]
ALTER COLUMN sizeBytes int NOT NULL

ALTER TABLE linkme_owner.[File]
ALTER COLUMN [hash] binary(16) NOT NULL
GO

CREATE INDEX IX_File_sizeBytes_hash
ON linkme_owner.[File] (sizeBytes, [hash])
GO
