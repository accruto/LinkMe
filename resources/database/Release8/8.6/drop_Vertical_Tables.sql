IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalContent') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE VerticalContent
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalNav') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE VerticalNav
GO

