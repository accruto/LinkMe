IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.ReportParameter') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.ReportParameter
GO

CREATE TABLE dbo.ReportParameter
( 
	id uniqueidentifier NOT NULL,
	[type] varchar(50) NOT NULL,
	[value] nvarchar(4000) NOT NULL,
	setId uniqueidentifier NOT NULL
)
GO

ALTER TABLE dbo.ReportParameter
ADD CONSTRAINT PK_ReportParameter 
PRIMARY KEY NONCLUSTERED (id)
GO

ALTER TABLE dbo.ReportParameter
ADD CONSTRAINT FK_ReportParameter_ReportParameterSet 
FOREIGN KEY (setId) REFERENCES ReportParameterSet (id)
GO
