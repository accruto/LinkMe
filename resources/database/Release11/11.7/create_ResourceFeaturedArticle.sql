CREATE TABLE [dbo].[ResourceFeaturedArticle](
	[id] [uniqueidentifier] NOT NULL,
	[resourceArticleId] [uniqueidentifier] NOT NULL,
	[cssClass] [nvarchar](50) NOT NULL,
	[featuredType] [tinyint] NOT NULL,
 CONSTRAINT [PK_ResourceFeaturedArticle] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)

GO
ALTER TABLE [dbo].[ResourceFeaturedArticle]  WITH CHECK ADD  CONSTRAINT [FK_ResourceFeaturedArticle_ResourceArticle] FOREIGN KEY([resourceArticleId])
REFERENCES [dbo].[ResourceArticle] ([id])
GO
ALTER TABLE [dbo].[ResourceFeaturedArticle] CHECK CONSTRAINT [FK_ResourceFeaturedArticle_ResourceArticle]