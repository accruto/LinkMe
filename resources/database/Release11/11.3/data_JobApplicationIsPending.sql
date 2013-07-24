BEGIN TRANSACTION
GO
UPDATE jobApplication
SET isPending = 1	--pending
WHERE status = 0 or status = 4 --not submitted or externally pending
GO
COMMIT
