-- Drop FKs

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductGrant_GrantedToUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductGrantOld]'))
ALTER TABLE [dbo].[ProductGrantOld] DROP CONSTRAINT [FK_ProductGrant_GrantedToUser]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employer_RegisteredUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employer]'))
ALTER TABLE [dbo].[Employer] DROP CONSTRAINT [FK_Employer_RegisteredUser]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Administrator_RegisteredUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[Administrator]'))
ALTER TABLE [dbo].[Administrator] DROP CONSTRAINT [FK_Administrator_RegisteredUser]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JoinReferral_RegisteredUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[JoinReferral]'))
ALTER TABLE [dbo].[JoinReferral] DROP CONSTRAINT [FK_JoinReferral_RegisteredUser]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CommunityAdministrator_RegisteredUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[CommunityAdministrator]'))
ALTER TABLE [dbo].[CommunityAdministrator] DROP CONSTRAINT [FK_CommunityAdministrator_RegisteredUser]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Member_RegisteredUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[Member]'))
ALTER TABLE [dbo].[Member] DROP CONSTRAINT [FK_Member_RegisteredUser]

-- Drop indexes

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RegisteredUser]') AND name = N'IX_RegisteredUser_createdTime')
DROP INDEX [IX_RegisteredUser_createdTime] ON [dbo].[RegisteredUser] WITH ( ONLINE = OFF )

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RegisteredUser]') AND name = N'PK_RegisteredUser')
ALTER TABLE [dbo].[RegisteredUser] DROP CONSTRAINT [PK_RegisteredUser]
GO

-- Create indexes

ALTER TABLE [dbo].[RegisteredUser]
ADD CONSTRAINT [PK_RegisteredUser] PRIMARY KEY NONCLUSTERED
(
	[id]
)
GO

CREATE CLUSTERED INDEX [IX_RegisteredUser_createdTime] ON [dbo].[RegisteredUser] 
(
	[createdTime]
)
GO

-- Create FKs

ALTER TABLE [dbo].[Employer]  WITH CHECK ADD  CONSTRAINT [FK_Employer_RegisteredUser] FOREIGN KEY([id])
REFERENCES [dbo].[RegisteredUser] ([id])

ALTER TABLE [dbo].[Administrator]  WITH CHECK ADD  CONSTRAINT [FK_Administrator_RegisteredUser] FOREIGN KEY([id])
REFERENCES [dbo].[RegisteredUser] ([id])

ALTER TABLE [dbo].[JoinReferral]  WITH CHECK ADD  CONSTRAINT [FK_JoinReferral_RegisteredUser] FOREIGN KEY([userId])
REFERENCES [dbo].[RegisteredUser] ([id])

ALTER TABLE [dbo].[CommunityAdministrator]  WITH CHECK ADD  CONSTRAINT [FK_CommunityAdministrator_RegisteredUser] FOREIGN KEY([id])
REFERENCES [dbo].[RegisteredUser] ([id])

ALTER TABLE [dbo].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Member_RegisteredUser] FOREIGN KEY([id])
REFERENCES [dbo].[RegisteredUser] ([id])
