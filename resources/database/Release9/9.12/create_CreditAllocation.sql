IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.CreditAllocation') AND type in (N'U'))
DROP TABLE dbo.CreditAllocation
GO

CREATE TABLE dbo.CreditAllocation
(
	id UNIQUEIDENTIFIER NOT NULL,
	ownerId UNIQUEIDENTIFIER NOT NULL,
	creditId UNIQUEIDENTIFIER NOT NULL,
	expiryDate DATETIME NULL,
	quantity INT NULL
)

ALTER TABLE dbo.CreditAllocation
ADD CONSTRAINT PK_CreditAllocation PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.CreditAllocation
ADD CONSTRAINT FK_CreditAllocation_Credit FOREIGN KEY (creditId)
REFERENCES dbo.Credit (id)
GO

DECLARE @neverExpiryDate DATETIME
SET @neverExpiryDate = '9999-12-31 00:00:00.000'

DECLARE @unlimitedQuantity INT
SET @unlimitedQuantity = 2000000000

INSERT
	CreditAllocation (id, ownerId, creditId, expiryDate, quantity)
SELECT
	id, userId, productDefinitionId, NULLIF(expiryDate, @neverExpiryDate), NULLIF(quantity, @unlimitedQuantity)
FROM
	Product
WHERE
	productDefinitionId IN (SELECT id FROM Credit)

GO

