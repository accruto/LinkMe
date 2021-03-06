IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.OfferingCriteriaSet') AND type in (N'U'))
DROP TABLE dbo.OfferingCriteriaSet
GO

CREATE TABLE dbo.OfferingCriteriaSet
(
	id UNIQUEIDENTIFIER NOT NULL,
	offeringId UNIQUEIDENTIFIER NOT NULL,
	type NVARCHAR(50) NOT NULL,
)

ALTER TABLE dbo.OfferingCriteriaSet
ADD CONSTRAINT PK_OfferingCriteriaSet PRIMARY KEY NONCLUSTERED
(
	id
)
GO

ALTER TABLE dbo.OfferingCriteriaSet
ADD CONSTRAINT FK_OfferingCriteriaSet_Offering FOREIGN KEY (offeringId)
REFERENCES dbo.Offering (id)
GO

