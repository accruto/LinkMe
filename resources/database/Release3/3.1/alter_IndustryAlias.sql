-- Rename the normalisedName column to displayName

EXEC sp_rename 'dbo.IndustryAlias.normalisedName', 'displayName', 'COLUMN'
GO