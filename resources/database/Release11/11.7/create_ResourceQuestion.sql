CREATE TABLE [dbo].[ResourceQuestion](
	[id] [uniqueidentifier] NOT NULL,
	[resourceCategoryId] [uniqueidentifier] NULL,
	[test] [text] NOT NULL,
	[createdTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ResourceQuestion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)

GO

ALTER TABLE [dbo].[ResourceQuestion]  WITH CHECK ADD  CONSTRAINT [FK_ResourceQuestion_ResourceCategory] FOREIGN KEY([resourceCategoryId])
REFERENCES [dbo].[ResourceCategory] ([id])
GO

ALTER TABLE [dbo].[ResourceQuestion] CHECK CONSTRAINT [FK_ResourceQuestion_ResourceCategory]