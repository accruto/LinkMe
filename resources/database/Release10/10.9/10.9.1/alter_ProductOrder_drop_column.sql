IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('dbo.ProductOrder') AND NAME = 'priceInclSurcharge')
BEGIN
	ALTER TABLE dbo.ProductOrder
	DROP COLUMN priceInclSurcharge
END
GO
