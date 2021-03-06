IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Campaign') AND type in (N'U'))
DROP TABLE dbo.Campaign
GO

CREATE TABLE dbo.Campaign
(
	id UNIQUEIDENTIFIER NOT NULL,
	name NVARCHAR(100) NOT NULL,
	createdTime DATETIME NOT NULL,
	createdBy UNIQUEIDENTIFIER NOT NULL,
	status INT NOT NULL,
	category INT NOT NULL
)

ALTER TABLE dbo.Campaign
ADD CONSTRAINT PK_Campaign PRIMARY KEY NONCLUSTERED
(
	id
)

ALTER TABLE dbo.Campaign
ADD CONSTRAINT UQ_Campaign_name UNIQUE NONCLUSTERED 
(
	name 
)

CREATE CLUSTERED INDEX IX_Campaign_createdTime ON dbo.Campaign
(
	createdTime
)
GO

