IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('RelocationArea') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE RelocationArea
GO

