IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Offering') AND type in (N'U'))
DROP TABLE dbo.Offering
GO

CREATE TABLE dbo.Offering
(
	id UNIQUEIDENTIFIER NOT NULL,
	providerId UNIQUEIDENTIFIER NOT NULL,
	name NVARCHAR(255) NULL,
	enabled BIT NOT NULL,
)

ALTER TABLE dbo.Offering
ADD CONSTRAINT PK_Offering PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.Offering
ADD CONSTRAINT FK_Offering_OfferProvider FOREIGN KEY (providerId)
REFERENCES dbo.OfferProvider (id)

CREATE UNIQUE CLUSTERED INDEX IX_Offering_providerId_name
ON dbo.Offering (providerId, name)


