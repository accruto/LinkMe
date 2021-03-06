CREATE TABLE [dbo].[ResourceArticleUserRating](
	[id] [uniqueidentifier] NOT NULL,
	[resourceArticleId] [uniqueidentifier] NOT NULL,
	[userId] [uniqueidentifier] NOT NULL,
	[rating] [tinyint] NOT NULL,
	[lastUpdatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ResourceArticleUserRating] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)

GO
ALTER TABLE [dbo].[ResourceArticleUserRating]  WITH CHECK ADD  CONSTRAINT [FK_ResourceArticleUserRating_ResourceArticle] FOREIGN KEY([resourceArticleId])
REFERENCES [dbo].[ResourceArticle] ([id])
GO
ALTER TABLE [dbo].[ResourceArticleUserRating] CHECK CONSTRAINT [FK_ResourceArticleUserRating_ResourceArticle]