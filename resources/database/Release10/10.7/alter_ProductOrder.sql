ALTER TABLE dbo.[ProductOrder]
ADD priceInclSurcharge DECIMAL NULL
GO

UPDATE
	[dbo].[ProductOrder]
SET
	priceInclSurcharge = priceInclTax
GO

-- Make the column not null

ALTER TABLE dbo.[ProductOrder]
ALTER COLUMN priceInclSurcharge DECIMAL NOT NULL
GO

