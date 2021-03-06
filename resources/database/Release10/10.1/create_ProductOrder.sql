IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.ProductOrder') AND type in (N'U'))
DROP TABLE dbo.ProductOrder
GO

CREATE TABLE dbo.ProductOrder
(
	id UNIQUEIDENTIFIER NOT NULL,
	confirmationCode NVARCHAR(100) NOT NULL,
	ownerId UNIQUEIDENTIFIER NOT NULL,
	purchaserId UNIQUEIDENTIFIER NOT NULL,
	time DATETIME NOT NULL,
	priceExclTax DECIMAL NOT NULL,
	priceInclTax DECIMAL NOT NULL,
	currency INT NOT NULL
)

ALTER TABLE dbo.ProductOrder
ADD CONSTRAINT PK_ProductOrder PRIMARY KEY NONCLUSTERED
(
	id
)

