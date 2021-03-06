IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.CreditAdjustment') AND type in (N'U'))
DROP TABLE dbo.CreditAdjustment
GO

CREATE TABLE dbo.CreditAdjustment
(
	id UNIQUEIDENTIFIER NOT NULL,
	ownerId UNIQUEIDENTIFIER NOT NULL,
	type INT NOT NULL,
	time DATETIME NOT NULL,
	adjustedByUserId UNIQUEIDENTIFIER NOT NULL,
	reasonId UNIQUEIDENTIFIER NOT NULL,
	notes NVARCHAR(3000) NULL,
	allocationId UNIQUEIDENTIFIER NULL,
	creditId UNIQUEIDENTIFIER NULL,
	expiryDate DATETIME NULL,
	quantity INT NULL,
)

ALTER TABLE dbo.CreditAdjustment
ADD CONSTRAINT PK_CreditAdjustment PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.CreditAdjustment
ADD CONSTRAINT FK_CreditAdjustment_Credit FOREIGN KEY (creditId)
REFERENCES dbo.Credit (id)
GO

ALTER TABLE dbo.CreditAdjustment
ADD CONSTRAINT FK_CreditAdjustment_CreditAdjustmentReason FOREIGN KEY (reasonId)
REFERENCES dbo.CreditAdjustmentReason (id)
GO

DECLARE @neverExpiryDate DATETIME
SET @neverExpiryDate = '9999-12-31 00:00:00.000'

DECLARE @unlimitedQuantity INT
SET @unlimitedQuantity = 2000000000

INSERT
	CreditAdjustment (id, ownerId, type, time, adjustedByUserId, reasonId, notes, creditId, expiryDate, quantity)
SELECT
	id,
	grantedToId,
	CASE WHEN quantity >= 0 THEN 0 ELSE 1 END,
	dateTime,
	grantedById,
	reasonId,
	notes,
	productDefinitionId,
	NULLIF(expiryDate, @neverExpiryDate),
	NULLIF(ABS(quantity), @unlimitedQuantity)
FROM
	ProductGrant
WHERE
	productDefinitionId IN (SELECT id FROM Credit)

GO

