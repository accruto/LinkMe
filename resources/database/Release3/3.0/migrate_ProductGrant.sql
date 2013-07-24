EXEC sp_changeobjectowner 'linkme_owner.ProductGrant', dbo
GO

-- Change grantedById and grantedToId to UNIQUEIDENTIFIER, updating constraints.

ALTER TABLE [dbo].[ProductGrant] DROP CONSTRAINT [FK_ProductGrant_administrator_profile]

ALTER TABLE [dbo].[ProductGrant] DROP CONSTRAINT [FK_ProductGrant_user_profile]

GO

EXEC sp_rename 'dbo.ProductGrant.grantedById', '_grantedById', 'COLUMN'

EXEC sp_rename 'dbo.ProductGrant.grantedToId', '_grantedToId', 'COLUMN'

ALTER TABLE dbo.ProductGrant
ADD grantedById UNIQUEIDENTIFIER NULL

ALTER TABLE dbo.ProductGrant
ADD grantedToId UNIQUEIDENTIFIER NULL

GO

UPDATE dbo.ProductGrant
SET grantedById = dbo.GuidFromString(_grantedById), grantedToId = dbo.GuidFromString(_grantedToId)

GO

ALTER TABLE dbo.ProductGrant
ALTER COLUMN grantedById UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.ProductGrant
ALTER COLUMN grantedToId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.ProductGrant
DROP COLUMN _grantedById

ALTER TABLE dbo.ProductGrant
DROP COLUMN _grantedToId

GO

ALTER TABLE dbo.ProductGrant
ADD CONSTRAINT FK_ProductGrant_GrantedByAdministrator
FOREIGN KEY (grantedById) REFERENCES dbo.Administrator ([id])

ALTER TABLE dbo.ProductGrant
ADD CONSTRAINT FK_ProductGrant_GrantedToUser
FOREIGN KEY (grantedToId) REFERENCES dbo.RegisteredUser ([id])

GO
