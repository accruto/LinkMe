IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.PurchaseTransaction') AND type in (N'U'))
DROP TABLE dbo.PurchaseTransaction
GO

CREATE TABLE dbo.PurchaseTransaction
(
	orderId UNIQUEIDENTIFIER NOT NULL,
	transactionId NVARCHAR(64) NOT NULL,
	provider NVARCHAR(16) NOT NULL,
	requestMessage NTEXT,
	requestTime DATETIME,
	responseMessage NTEXT,
	responseTime DATETIME,
)

ALTER TABLE dbo.PurchaseTransaction
ADD CONSTRAINT PK_PurchaseTransaction PRIMARY KEY CLUSTERED
(
	orderId,
	transactionId
)
GO
