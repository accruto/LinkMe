IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.OfferCategory') AND type in (N'U'))
DROP TABLE dbo.OfferCategory
GO

CREATE TABLE dbo.OfferCategory
(
	id UNIQUEIDENTIFIER NOT NULL,
	parentId UNIQUEIDENTIFIER NULL,
	name NVARCHAR(255) NULL,
	enabled BIT NOT NULL,
	deleted BIT NOT NULL
)

ALTER TABLE dbo.OfferCategory
ADD CONSTRAINT PK_OfferCategory PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.OfferCategory
ADD CONSTRAINT FK_OfferCategory_OfferCategory FOREIGN KEY (parentId)
REFERENCES dbo.OfferCategory (id)

