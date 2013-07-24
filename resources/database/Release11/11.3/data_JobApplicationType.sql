BEGIN TRANSACTION
GO
UPDATE jobApplication
SET applicationType = 1	--managed
WHERE status = 5 or status = 4 --managed externally or externally pending
GO
COMMIT
