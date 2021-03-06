IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.ProductReceipt') AND type in (N'U'))
DROP TABLE dbo.ProductReceipt
GO

CREATE TABLE dbo.ProductReceipt
(
	id UNIQUEIDENTIFIER NOT NULL,
	orderId UNIQUEIDENTIFIER NOT NULL,
	time DATETIME NOT NULL,
	externalTransactionId NVARCHAR(100) NOT NULL,
	externalTransactionTime DATETIME NOT NULL,
	type INT NOT NULL,
	creditCardPan NVARCHAR(32) NULL,
	creditCardType INT NULL
)

ALTER TABLE dbo.ProductReceipt
ADD CONSTRAINT PK_ProductReceipt PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.ProductReceipt
ADD CONSTRAINT FK_ProductReceipt_Order FOREIGN KEY (orderId)
REFERENCES dbo.ProductOrder (id)
GO
