IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.ReportParameterSet') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.ReportParameterSet
GO

CREATE TABLE dbo.ReportParameterSet
( 
	id uniqueidentifier NOT NULL
)
GO

ALTER TABLE dbo.ReportParameterSet
ADD CONSTRAINT PK_ReportParameterSet 
PRIMARY KEY NONCLUSTERED (id)
GO
