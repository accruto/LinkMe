ALTER TABLE dbo.Product
ALTER COLUMN price DECIMAL(18, 2) NOT NULL
GO

ALTER TABLE dbo.ProductOrder
ALTER COLUMN priceExclTax DECIMAL(18, 2) NOT NULL
GO

ALTER TABLE dbo.ProductOrder
ALTER COLUMN priceInclTax DECIMAL(18, 2) NOT NULL
GO

ALTER TABLE dbo.ProductOrderAdjustment
ALTER COLUMN initialPrice DECIMAL(18, 2) NOT NULL
GO

ALTER TABLE dbo.ProductOrderAdjustment
ALTER COLUMN adjustedPrice DECIMAL(18, 2) NOT NULL
GO

ALTER TABLE dbo.ProductOrderItem
ALTER COLUMN price DECIMAL(18, 2) NOT NULL
GO

