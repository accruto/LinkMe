IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.OfferCategoryOffering') AND type in (N'U'))
DROP TABLE dbo.OfferCategoryOffering
GO

CREATE TABLE dbo.OfferCategoryOffering
(
	categoryId UNIQUEIDENTIFIER NOT NULL,
	offeringId UNIQUEIDENTIFIER NOT NULL
)

ALTER TABLE dbo.OfferCategoryOffering
ADD CONSTRAINT PK_OfferCategoryOffering PRIMARY KEY CLUSTERED
(
	categoryId,
	offeringId
)

ALTER TABLE dbo.OfferCategoryOffering
ADD CONSTRAINT FK_OfferCategoryOffering_OfferCategory FOREIGN KEY (categoryId)
REFERENCES dbo.OfferCategory (id)

ALTER TABLE dbo.OfferCategoryOffering
ADD CONSTRAINT FK_OfferCategoryOffering_Offering FOREIGN KEY (offeringId)
REFERENCES dbo.Offering (id)
