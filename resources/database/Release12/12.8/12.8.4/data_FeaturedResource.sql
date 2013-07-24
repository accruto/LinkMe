ALTER TABLE dbo.ResourceFeaturedArticle
ALTER COLUMN cssClass NVARCHAR(50) NULL
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ResourceFeaturedArticle_ResourceArticle]') AND parent_object_id = OBJECT_ID(N'[dbo].[ResourceFeaturedArticle]'))
ALTER TABLE [dbo].[ResourceFeaturedArticle] DROP CONSTRAINT [FK_ResourceFeaturedArticle_ResourceArticle]
GO

INSERT
	dbo.ResourceFeaturedArticle (id, resourceArticleId, cssClass, featuredType)
VALUES
	('{877E72B3-0B55-4222-B94E-19BC744D957F}', 'C531330B-F8ED-4998-BD1A-AF59BFFCD7E4', NULL, 1)
GO

INSERT
	dbo.ResourceFeaturedArticle (id, resourceArticleId, cssClass, featuredType)
VALUES
	('{5E4907EF-3AA7-4E8B-AFAA-04257E099235}', 'F9C3D03B-6C2D-401B-97E0-3B56CBCF1115', NULL, 1)
GO