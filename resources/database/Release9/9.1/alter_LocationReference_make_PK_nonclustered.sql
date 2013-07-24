-- Drop all referencing FKs

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_LocationReference]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_LocationReference]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Group_LocationReference]') AND parent_object_id = OBJECT_ID(N'[dbo].[Group]'))
ALTER TABLE [dbo].[Group] DROP CONSTRAINT [FK_Group_LocationReference]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAdLocation_LocationReference]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAdLocation]'))
ALTER TABLE [dbo].[JobAdLocation] DROP CONSTRAINT [FK_JobAdLocation_LocationReference]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RelocationLocation_GeographicalArea]') AND parent_object_id = OBJECT_ID(N'[dbo].[RelocationLocation]'))
ALTER TABLE [dbo].[RelocationLocation] DROP CONSTRAINT [FK_RelocationLocation_GeographicalArea]

GO

-- Re-create PK_LocationReference as non-clustered

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[LocationReference]') AND name = N'PK_LocationReference')
ALTER TABLE [dbo].[LocationReference] DROP CONSTRAINT [PK_LocationReference]
GO

ALTER TABLE dbo.LocationReference
ADD CONSTRAINT PK_LocationReference PRIMARY KEY NONCLUSTERED ([id])
GO

-- Re-create all referencing FKs

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_LocationReference] FOREIGN KEY([locationReferenceId])
REFERENCES [dbo].[LocationReference] ([id])
GO

ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_LocationReference] FOREIGN KEY([locationReferenceId])
REFERENCES [dbo].[LocationReference] ([id])
GO

ALTER TABLE [dbo].[JobAdLocation]  WITH CHECK ADD  CONSTRAINT [FK_JobAdLocation_LocationReference] FOREIGN KEY([locationReferenceId])
REFERENCES [dbo].[LocationReference] ([id])
GO

ALTER TABLE [dbo].[RelocationLocation]  WITH CHECK ADD  CONSTRAINT [FK_RelocationLocation_GeographicalArea] FOREIGN KEY([locationReferenceId])
REFERENCES [dbo].[LocationReference] ([id])
GO
