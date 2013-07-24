IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalMember') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE VerticalMember
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Vertical') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Vertical
GO

CREATE TABLE Vertical ( 
	id UNIQUEIDENTIFIER NOT NULL,
	displayName NVARCHAR(100) NOT NULL,
	emailDomain NVARCHAR(100) NULL,
	url NVARCHAR(100) NOT NULL
)
GO

ALTER TABLE Vertical
	ADD CONSTRAINT UQ_Vertical_displayName UNIQUE (displayName)
GO

ALTER TABLE Vertical
	ADD CONSTRAINT UQ_Vertical_url UNIQUE (url)
GO

ALTER TABLE Vertical ADD CONSTRAINT PK_Vertical
	PRIMARY KEY CLUSTERED (id)
GO

