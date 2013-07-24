IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('EmailStats') AND NAME = 'sends')
BEGIN
	ALTER TABLE EmailStats
		DROP COLUMN sends
END
GO
