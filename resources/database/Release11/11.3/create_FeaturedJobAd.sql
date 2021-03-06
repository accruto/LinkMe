IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.FeaturedJobAd') AND type in (N'U'))
DROP TABLE dbo.FeaturedJobAd
GO

CREATE TABLE dbo.FeaturedJobAd
(
	id UNIQUEIDENTIFIER NOT NULL,
	url NVARCHAR(1024) NOT NULL,
	title NVARCHAR(1024) NOT NULL
)

ALTER TABLE dbo.FeaturedJobAd
ADD CONSTRAINT PK_FeaturedJobAd PRIMARY KEY NONCLUSTERED
(
	id
)
GO

