IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MemberIndexing_Member]') AND parent_object_id = OBJECT_ID(N'[dbo].[MemberIndexing]'))
ALTER TABLE [dbo].[MemberIndexing] DROP CONSTRAINT [FK_MemberIndexing_Member]
GO

