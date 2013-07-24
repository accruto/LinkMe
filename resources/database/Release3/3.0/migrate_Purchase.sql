EXEC sp_changeobjectowner 'linkme_owner.Purchase', dbo
GO

-- Change grantedById and grantedToId to UNIQUEIDENTIFIER, updating constraints.

ALTER TABLE [dbo].[Purchase] DROP CONSTRAINT [FK_Purchase_administrator_profile]

ALTER TABLE [dbo].[Purchase] DROP CONSTRAINT [FK_Purchase_user_profile]

GO

EXEC sp_rename 'dbo.Purchase.purchaserId', '_purchaserId', 'COLUMN'

EXEC sp_rename 'dbo.Purchase.processedByAdministratorId', '_processedByAdministratorId', 'COLUMN'

ALTER TABLE dbo.Purchase
ADD purchaserId UNIQUEIDENTIFIER NULL

ALTER TABLE dbo.Purchase
ADD processedByAdministratorId UNIQUEIDENTIFIER NULL

GO

UPDATE dbo.Purchase
SET purchaserId = dbo.GuidFromString(_purchaserId),
	processedByAdministratorId = dbo.GuidFromString(_processedByAdministratorId)

GO

ALTER TABLE dbo.Purchase
ALTER COLUMN purchaserId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.Purchase
DROP COLUMN _purchaserId

ALTER TABLE dbo.Purchase
DROP COLUMN _processedByAdministratorId

GO

ALTER TABLE dbo.Purchase
ADD CONSTRAINT FK_Purchase_PurchaserUser
FOREIGN KEY (purchaserId) REFERENCES dbo.RegisteredUser ([id])

ALTER TABLE dbo.Purchase
ADD CONSTRAINT FK_Purchase_ProcessedByAdministrator
FOREIGN KEY (processedByAdministratorId) REFERENCES dbo.Administrator ([id])

GO
