EXEC sp_changeobjectowner 'linkme_owner.Product', dbo
GO

-- Change memberId (VARCHAR) to userId (UNIQUEIDENTIFIER), updating constraints.

ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_user_profile]

GO

ALTER TABLE dbo.Product
ADD userId UNIQUEIDENTIFIER NULL

GO

UPDATE dbo.Product
SET userId = dbo.GuidFromString(memberId)

GO

ALTER TABLE dbo.Product
ALTER COLUMN userId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.Product
DROP COLUMN memberId

GO

ALTER TABLE dbo.Product
ADD CONSTRAINT FK_Product_RegisteredUser
FOREIGN KEY (userId) REFERENCES dbo.RegisteredUser ([id])

GO
