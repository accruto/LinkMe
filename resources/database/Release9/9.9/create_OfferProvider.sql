IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.OfferProvider') AND type in (N'U'))
DROP TABLE dbo.OfferProvider
GO

CREATE TABLE dbo.OfferProvider
(
	id UNIQUEIDENTIFIER NOT NULL,
	name NVARCHAR(255) NULL,
	enabled BIT NOT NULL,
)

ALTER TABLE dbo.OfferProvider
ADD CONSTRAINT PK_OfferProvider PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE UNIQUE INDEX IX_OfferProvider_name
ON dbo.OfferProvider (name)
