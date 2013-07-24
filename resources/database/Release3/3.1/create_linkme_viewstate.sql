CREATE TABLE dbo.linkme_viewstate
(
	[key] UNIQUEIDENTIFIER NOT NULL,
	pageState TEXT NOT NULL,
	updatedDate DATETIME NOT NULL
)

ALTER TABLE dbo.linkme_viewstate
ADD CONSTRAINT PK_linkme_viewstate
PRIMARY KEY  NONCLUSTERED ([key])

GO
