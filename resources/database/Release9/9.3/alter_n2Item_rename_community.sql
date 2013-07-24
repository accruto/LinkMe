IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('dbo.n2Item') AND NAME = 'Vertical')
	EXEC sp_rename 'dbo.n2Item.Community', 'Vertical'
GO

