IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('EmailStats') AND NAME = 'verticalId')
BEGIN
	EXEC sp_rename 'EmailStats.verticalId', 'communityId', 'COLUMN'
END
