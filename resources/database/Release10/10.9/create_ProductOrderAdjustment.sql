IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.ProductOrderAdjustment') AND type in (N'U'))
DROP TABLE dbo.ProductOrderAdjustment
GO

CREATE TABLE dbo.ProductOrderAdjustment
(
	id UNIQUEIDENTIFIER NOT NULL,
	orderId UNIQUEIDENTIFIER NOT NULL,
	rank INT NOT NULL,
	initialPrice DECIMAL(18,2) NOT NULL,
	adjustedPrice DECIMAL(18,2) NOT NULL,
	type NVARCHAR(100) NOT NULL,
	data1 SQL_VARIANT,
	data2 SQL_VARIANT
)

ALTER TABLE dbo.ProductOrderAdjustment
ADD CONSTRAINT PK_ProductOrderAdjustment PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.ProductOrderAdjustment
ADD CONSTRAINT FK_ProductOrderAdjustment_Order FOREIGN KEY (orderId)
REFERENCES dbo.ProductOrder (id)
GO
