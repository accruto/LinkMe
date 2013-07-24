
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('JobAdArea') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE JobAdArea
GO

