CREATE TABLE [dbo].[ResourceCategory](
	[id] [uniqueidentifier] NOT NULL,
	[displayname] [nvarchar](50) NOT NULL,
	[displayorder] [tinyint] NOT NULL,
 CONSTRAINT [PK_ResourceCategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)
