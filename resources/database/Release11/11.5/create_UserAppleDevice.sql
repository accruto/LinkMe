IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.UserAppleDevice') AND type in (N'U'))
	DROP TABLE [dbo].[UserAppleDevice]
GO

CREATE TABLE [dbo].[UserAppleDevice](
	[id] [uniqueidentifier] NOT NULL,
	[ownerId] [uniqueidentifier] NOT NULL,
	[deviceToken] [nvarchar](100) NOT NULL,
	[badgeCount] [smallint] NULL,
	[active] [bit] NOT NULL CONSTRAINT [DF_AppleUserData_active]  DEFAULT ((1)),
 CONSTRAINT [PK_AppleUserData] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
