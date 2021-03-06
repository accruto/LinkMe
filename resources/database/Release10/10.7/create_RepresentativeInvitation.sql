IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RepresentativeInvitation]') AND type in (N'U'))
DROP TABLE [dbo].[RepresentativeInvitation]
GO

CREATE TABLE [dbo].[RepresentativeInvitation](
	[id] [uniqueidentifier] NOT NULL,
	[inviteeEmailAddress] [dbo].[EmailAddress] NULL,
	[inviterId] [uniqueidentifier] NOT NULL,
	[inviteeId] [uniqueidentifier] NULL,
CONSTRAINT [PK_RepresentativeInvitation] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)
)

GO

ALTER TABLE [dbo].[RepresentativeInvitation] WITH NOCHECK ADD CONSTRAINT [FK_RepresentativeInvitation_UserToUserRequest] FOREIGN KEY([id])
REFERENCES [dbo].[UserToUserRequest] ([id])
GO

ALTER TABLE [dbo].[RepresentativeInvitation] CHECK CONSTRAINT [FK_RepresentativeInvitation_UserToUserRequest]