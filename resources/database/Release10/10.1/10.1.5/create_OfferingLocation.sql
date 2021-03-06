IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.OfferingLocation') AND type in (N'U'))
DROP TABLE dbo.OfferingLocation
GO

CREATE TABLE dbo.OfferingLocation
(
	offeringId UNIQUEIDENTIFIER NOT NULL,
	locationReferenceId UNIQUEIDENTIFIER NOT NULL
)

ALTER TABLE dbo.OfferingLocation
ADD CONSTRAINT PK_OfferingLocation PRIMARY KEY CLUSTERED
(
	offeringId,
	locationReferenceId
)

ALTER TABLE dbo.OfferingLocation
ADD CONSTRAINT FK_OfferingLocation_Offering FOREIGN KEY (offeringId)
REFERENCES dbo.Offering (id)

ALTER TABLE dbo.OfferingLocation
ADD CONSTRAINT FK_OfferingLocation_LocationReference FOREIGN KEY (locationReferenceId)
REFERENCES dbo.LocationReference (id)

