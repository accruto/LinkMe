ALTER TABLE Candidate
ADD relocationReason NVARCHAR(200) NULL
GO

ALTER TABLE Candidate
ADD relocationPreference RelocationPreference NULL
GO

UPDATE Candidate
SET relocationPreference = 0

ALTER TABLE Candidate
ALTER COLUMN relocationPreference RelocationPreference NOT NULL
GO
