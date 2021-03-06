-- Drop referencing foreign keys

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserEventActionedJobAd_UserEvent]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserEventActionedJobAd]'))
ALTER TABLE [dbo].[UserEventActionedJobAd] DROP CONSTRAINT [FK_UserEventActionedJobAd_UserEvent]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserEventActionedResume_UserEvent]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserEventActionedResume]'))
ALTER TABLE [dbo].[UserEventActionedResume] DROP CONSTRAINT [FK_UserEventActionedResume_UserEvent]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserEventActionedUser_UserEvent]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserEventActionedUser]'))
ALTER TABLE [dbo].[UserEventActionedUser] DROP CONSTRAINT [FK_UserEventActionedUser_UserEvent]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserEventExtraData_UserEvent]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserEventExtraData]'))
ALTER TABLE [dbo].[UserEventExtraData] DROP CONSTRAINT [FK_UserEventExtraData_UserEvent]

GO

-- Re-create the PK as non-clustered

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserEvent]') AND name = N'PK_UserEvent')
ALTER TABLE [dbo].[UserEvent] DROP CONSTRAINT [PK_UserEvent]
GO

ALTER TABLE dbo.UserEvent
ADD CONSTRAINT PK_UserEvent
PRIMARY KEY NONCLUSTERED ([id])
GO

-- Re-create the foreign keys

ALTER TABLE [dbo].[UserEventActionedJobAd]  WITH CHECK ADD  CONSTRAINT [FK_UserEventActionedJobAd_UserEvent] FOREIGN KEY([eventId])
REFERENCES [dbo].[UserEvent] ([id])

ALTER TABLE [dbo].[UserEventActionedResume]  WITH CHECK ADD  CONSTRAINT [FK_UserEventActionedResume_UserEvent] FOREIGN KEY([eventId])
REFERENCES [dbo].[UserEvent] ([id])

ALTER TABLE [dbo].[UserEventActionedUser]  WITH CHECK ADD  CONSTRAINT [FK_UserEventActionedUser_UserEvent] FOREIGN KEY([eventId])
REFERENCES [dbo].[UserEvent] ([id])

ALTER TABLE [dbo].[UserEventExtraData]  WITH CHECK ADD  CONSTRAINT [FK_UserEventExtraData_UserEvent] FOREIGN KEY([eventId])
REFERENCES [dbo].[UserEvent] ([id])

GO
