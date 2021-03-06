IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ResourceArticleUserRating_ResourceArticle]') AND parent_object_id = OBJECT_ID(N'[dbo].[ResourceArticleUserRating]'))
ALTER TABLE [dbo].[ResourceArticleUserRating] DROP CONSTRAINT [FK_ResourceArticleUserRating_ResourceArticle]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ResourceArticleUserRating]') AND name = N'PK_ResourceArticleUserRating')
ALTER TABLE [dbo].[ResourceArticleUserRating] DROP CONSTRAINT [PK_ResourceArticleUserRating]
GO

ALTER TABLE dbo.ResourceArticleUserRating
ADD CONSTRAINT PK_ResourceArticleUserRating PRIMARY KEY NONCLUSTERED
(
	id
)
GO

CREATE CLUSTERED INDEX [IX_ResourceArticleUserRating] ON [dbo].[ResourceArticleUserRating]
(
	resourceArticleId ASC
)
GO

ALTER TABLE dbo.ResourceArticleUserRating
ADD resourceType TINYINT NULL
GO

UPDATE
	dbo.ResourceArticleUserRating
SET
	resourceType = 2
GO

ALTER TABLE dbo.ResourceArticleUserRating
ALTER COLUMN resourceType TINYINT NOT NULL
GO
