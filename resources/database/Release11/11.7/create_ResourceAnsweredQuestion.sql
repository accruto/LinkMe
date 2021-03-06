CREATE TABLE [dbo].[ResourceAnsweredQuestion](
	[id] [uniqueidentifier] NOT NULL,
	[resourceSubcategoryId] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[text] [text] NOT NULL,
	[createdTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ResourceAnsweredQuestion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)

GO

ALTER TABLE [dbo].[ResourceAnsweredQuestion]  WITH CHECK ADD  CONSTRAINT [FK_ResourceAnsweredQuestion_ResourceSubcategory] FOREIGN KEY([resourceSubcategoryId])
REFERENCES [dbo].[ResourceSubcategory] ([id])
GO

ALTER TABLE [dbo].[ResourceAnsweredQuestion] CHECK CONSTRAINT [FK_ResourceAnsweredQuestion_ResourceSubcategory]