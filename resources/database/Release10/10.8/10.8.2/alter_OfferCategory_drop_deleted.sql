IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('dbo.OfferCategory') AND NAME = 'deleted')
BEGIN
	ALTER TABLE dbo.OfferCategory
	DROP COLUMN deleted
END
GO
