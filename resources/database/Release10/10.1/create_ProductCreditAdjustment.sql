IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.ProductCreditAdjustment') AND type in (N'U'))
DROP TABLE dbo.ProductCreditAdjustment
GO

CREATE TABLE dbo.ProductCreditAdjustment
(
	productId UNIQUEIDENTIFIER NOT NULL,
	creditId UNIQUEIDENTIFIER NOT NULL,
	quantity INT NULL,
	duration BIGINT NULL
)

ALTER TABLE dbo.ProductCreditAdjustment
ADD CONSTRAINT PK_ProductCreditAdjustment PRIMARY KEY CLUSTERED
(
	productId,
	creditId
)

ALTER TABLE dbo.ProductCreditAdjustment
ADD CONSTRAINT FK_ProductCreditAdjustment_Product FOREIGN KEY (productId)
REFERENCES dbo.Product (id)
GO
