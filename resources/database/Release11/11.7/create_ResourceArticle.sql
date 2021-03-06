CREATE TABLE [dbo].[ResourceArticle](
	[id] [uniqueidentifier] NOT NULL,
	[resourceSubcategoryId] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[text] [text] NOT NULL,
	[rating] [numeric](2, 1) NOT NULL CONSTRAINT [DF_ResourceArticle_rating]  DEFAULT ((0)),
	[createdTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ResourceArticle] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)

GO

ALTER TABLE [dbo].[ResourceArticle]  WITH CHECK ADD  CONSTRAINT [FK_ResourceArticle_ResourceSubcategory] FOREIGN KEY([resourceSubcategoryId])
REFERENCES [dbo].[ResourceSubcategory] ([id])
GO

ALTER TABLE [dbo].[ResourceArticle] CHECK CONSTRAINT [FK_ResourceArticle_ResourceSubcategory]