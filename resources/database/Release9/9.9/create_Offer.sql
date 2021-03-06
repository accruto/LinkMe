IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Offer') AND type in (N'U'))
DROP TABLE dbo.Offer
GO

CREATE TABLE dbo.Offer
(
	id UNIQUEIDENTIFIER NOT NULL,
	requestId UNIQUEIDENTIFIER NOT NULL,
	offeringId UNIQUEIDENTIFIER NOT NULL,
	createdTime DATETIME NOT NULL,
)

ALTER TABLE dbo.Offer
ADD CONSTRAINT PK_Offer PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.Offer
ADD CONSTRAINT FK_Offer_OfferRequest FOREIGN KEY (requestId)
REFERENCES dbo.OfferRequest (id)

ALTER TABLE dbo.Offer
ADD CONSTRAINT FK_Offer_Offering FOREIGN KEY (offeringId)
REFERENCES dbo.Offering (id)


