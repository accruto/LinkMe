IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_n2Detail_n2DetailCollection]') AND parent_object_id = OBJECT_ID(N'[dbo].[n2Detail]'))
ALTER TABLE [dbo].[n2Detail] DROP CONSTRAINT [FK_n2Detail_n2DetailCollection]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_n2Detail_n2Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[n2Detail]'))
ALTER TABLE [dbo].[n2Detail] DROP CONSTRAINT [FK_n2Detail_n2Item]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[n2Detail]') AND type in (N'U'))
DROP TABLE [dbo].[n2Detail]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_n2DetailCollection_n2Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[n2DetailCollection]'))
ALTER TABLE [dbo].[n2DetailCollection] DROP CONSTRAINT [FK_n2DetailCollection_n2Item]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[n2DetailCollection]') AND type in (N'U'))
DROP TABLE [dbo].[n2DetailCollection]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_n2AllowedRole_n2Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[n2AllowedRole]'))
ALTER TABLE [dbo].[n2AllowedRole] DROP CONSTRAINT [FK_n2AllowedRole_n2Item]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[n2AllowedRole]') AND type in (N'U'))
DROP TABLE [dbo].[n2AllowedRole]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_n2Item_n2Item_Parent]') AND parent_object_id = OBJECT_ID(N'[dbo].[n2Item]'))
ALTER TABLE [dbo].[n2Item] DROP CONSTRAINT [FK_n2Item_n2Item_Parent]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_n2Item_n2Item_VersionOf]') AND parent_object_id = OBJECT_ID(N'[dbo].[n2Item]'))
ALTER TABLE [dbo].[n2Item] DROP CONSTRAINT [FK_n2Item_n2Item_VersionOf]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[n2Item]') AND type in (N'U'))
DROP TABLE [dbo].[n2Item]