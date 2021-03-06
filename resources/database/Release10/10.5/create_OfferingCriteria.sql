IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.OfferingCriteria') AND type in (N'U'))
DROP TABLE dbo.OfferingCriteria
GO

CREATE TABLE dbo.OfferingCriteria
(
	id UNIQUEIDENTIFIER NOT NULL,
	name NVARCHAR(50) NOT NULL,
	value SQL_VARIANT NULL
)

ALTER TABLE dbo.OfferingCriteria
ADD CONSTRAINT PK_OfferingCriteria PRIMARY KEY NONCLUSTERED
(
	id,
	name
)
GO

ALTER TABLE dbo.OfferingCriteria
ADD CONSTRAINT FK_OfferingCriteria_OfferingCriteriaSet FOREIGN KEY (id)
REFERENCES dbo.OfferingCriteriaSet (id)
GO

