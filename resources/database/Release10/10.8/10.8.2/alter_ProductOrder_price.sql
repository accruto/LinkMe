ALTER TABLE dbo.ProductOrder
ALTER COLUMN priceExclTax DECIMAL(18,2) NOT NULL
GO

ALTER TABLE dbo.ProductOrder
ALTER COLUMN priceInclTax DECIMAL(18,2) NOT NULL
GO

ALTER TABLE dbo.ProductOrder
ALTER COLUMN priceInclSurcharge DECIMAL(18,2) NOT NULL
GO

