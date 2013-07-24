DELETE
	dbo.ProductOrderAdjustment

-- Single tax adjustment

INSERT
	dbo.ProductOrderAdjustment (id, orderId, rank, initialPrice, adjustedPrice, type, data1, data2)
SELECT
	NEWID(), id, 0, priceExclTax, priceInclTax, 'TaxAdjustment', 0.1, NULL
FROM
	dbo.ProductOrder
WHERE
	priceInclSurcharge = priceInclTax

-- Tax adjustment and surcharge

INSERT
	dbo.ProductOrderAdjustment (id, orderId, rank, initialPrice, adjustedPrice, type, data1, data2)
SELECT
	NEWID(), id, 0, priceExclTax, priceInclTax, 'TaxAdjustment', 0.1, NULL
FROM
	dbo.ProductOrder
WHERE
	priceInclSurcharge <> priceInclTax

INSERT
	dbo.ProductOrderAdjustment (id, orderId, rank, initialPrice, adjustedPrice, type, data1, data2)
SELECT
	NEWID(), id, 1, priceInclTax, priceInclSurcharge, 'SurchargeAdjustment', 0.025, 2
FROM
	dbo.ProductOrder
WHERE
	priceInclSurcharge <> priceInclTax

UPDATE
	dbo.ProductOrder
SET
	priceInclTax = priceInclSurcharge

