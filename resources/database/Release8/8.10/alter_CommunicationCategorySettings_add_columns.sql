
IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('CommunicationCategorySettings') AND NAME = 'frequency')
BEGIN
	ALTER TABLE dbo.CommunicationCategorySettings
	ADD frequency CommunicationFrequency NULL
END
GO

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('CommunicationCategorySettings') AND NAME = 'suppress')
BEGIN

	-- If suppress has been set then indicate a frequency of 'Never'

	UPDATE
		CommunicationCategorySettings
	SET
		frequency = 4
	WHERE
		suppress = 1

	ALTER TABLE dbo.CommunicationcategorySettings
	DROP COLUMN suppress

END
	
