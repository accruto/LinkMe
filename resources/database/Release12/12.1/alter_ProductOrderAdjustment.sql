ALTER TABLE dbo.ProductOrderAdjustment
ADD percentageAmount DECIMAL(18,18) NULL
GO

ALTER TABLE dbo.ProductOrderAdjustment
ADD fixedAmount DECIMAL(18,2) NULL
GO

ALTER TABLE dbo.ProductOrderAdjustment
ADD referenceId UNIQUEIDENTIFIER NULL
GO

ALTER TABLE dbo.ProductOrderAdjustment
ADD code NVARCHAR(20) NULL
GO

UPDATE
	dbo.ProductOrderAdjustment
SET
	percentageAmount = CONVERT(DECIMAL(18,18), data1)
WHERE
	type = 'TaxAdjustment'

UPDATE
	dbo.ProductOrderAdjustment
SET
	percentageAmount = CONVERT(DECIMAL(18,18), data1)
WHERE
	type = 'BundleAdjustment'

UPDATE
	dbo.ProductOrderAdjustment
SET
	percentageAmount = CONVERT(DECIMAL(18,18), data1),
	code = 'Amex'
WHERE
	type = 'SurchargeAdjustment'
GO

ALTER TABLE dbo.ProductOrderAdjustment
DROP COLUMN data1
GO

ALTER TABLE dbo.ProductOrderAdjustment
DROP COLUMN data2
GO

ALTER TABLE dbo.ProductOrderAdjustment
DROP COLUMN data3
GO