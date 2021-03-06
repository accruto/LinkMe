CREATE TABLE [dbo].[ResourceVideo](
	[id] [uniqueidentifier] NOT NULL,
	[resourceSubcategoryId] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[transcript] [text] NOT NULL,
	[externalVideoId] [nvarchar](20) NOT NULL,
	[createdTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ResourceVideo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)

GO

ALTER TABLE [dbo].[ResourceVideo]  WITH CHECK ADD  CONSTRAINT [FK_ResourceVideo_ResourceSubcategory] FOREIGN KEY([resourceSubcategoryId])
REFERENCES [dbo].[ResourceSubcategory] ([id])
GO

ALTER TABLE [dbo].[ResourceVideo] CHECK CONSTRAINT [FK_ResourceVideo_ResourceSubcategory]