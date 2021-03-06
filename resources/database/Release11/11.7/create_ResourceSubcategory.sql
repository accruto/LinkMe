CREATE TABLE [dbo].[ResourceSubcategory](
	[id] [uniqueidentifier] NOT NULL,
	[resourceCategoryId] [uniqueidentifier] NOT NULL,
	[displayname] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ResourceSubcategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)

GO

ALTER TABLE [dbo].[ResourceSubcategory]  WITH CHECK ADD  CONSTRAINT [FK_ResourceSubcategory_ResourceCategory] FOREIGN KEY([resourceCategoryId])
REFERENCES [dbo].[ResourceCategory] ([id])
GO

ALTER TABLE [dbo].[ResourceSubcategory] CHECK CONSTRAINT [FK_ResourceSubcategory_ResourceCategory]