IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalContent') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE VerticalContent
GO

CREATE TABLE VerticalContent ( 
	verticalId UNIQUEIDENTIFIER NOT NULL,
	brandControl NVARCHAR(100) NULL,
	brandFooterControl NVARCHAR(100) NULL,
	candidateImageUrl NVARCHAR(100) NULL
)
GO

ALTER TABLE VerticalContent ADD CONSTRAINT PK_VerticalContent
	PRIMARY KEY CLUSTERED (verticalId)
GO

