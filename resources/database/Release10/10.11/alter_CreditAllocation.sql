ALTER TABLE dbo.CreditAllocation
ADD initialQuantity INT NULL
GO

ALTER TABLE dbo.CreditAllocation
ADD createdTime DATETIME NULL
GO

ALTER TABLE dbo.CreditAllocation
ADD deallocatedTime DATETIME NULL
GO

-- If there is an expiry date then assume the creation date is 1 year before that.

UPDATE
	dbo.CreditAllocation
SET
	createdTime = DATEADD(yy, -1, expiryDate)
WHERE
	expiryDate IS NOT NULL
GO

-- No expiry date, just use a year ago.

UPDATE
	dbo.CreditAllocation
SET
	createdTime = DATEADD(yy, -1, GETDATE())
WHERE
	expiryDate IS NULL
GO

-- Make the column not null

ALTER TABLE dbo.CreditAllocation
ALTER COLUMN createdTime DATETIME NOT NULL
GO

-- No order, so use the current amount.

UPDATE
	dbo.CreditAllocation
SET
	initialQuantity = a.quantity
FROM
	dbo.CreditAllocation AS a

-- Quantity allocated as part of an order, use the ProductCreditAdjustment

UPDATE
	dbo.CreditAllocation
SET
	initialQuantity =
	(
		SELECT
			MAX(c.quantity)
		FROM
			dbo.ProductOrder AS o
		INNER JOIN
			dbo.ProductOrderItem AS i ON i.orderId = o.id
		INNER JOIN
			dbo.Product AS p ON p.id = i.productId
		INNER JOIN
			dbo.ProductCreditAdjustment AS c ON c.productId = p.id AND c.creditId = a.creditId
		WHERE
			o.id = a.referenceId
	)
FROM
	dbo.CreditAllocation AS a
WHERE
	NOT a.quantity IS NULL
	AND NOT a.referenceId IS NULL

