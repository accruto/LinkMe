ALTER TABLE dbo.CommunicationCategory
ADD availableFrequencies NVARCHAR(100)
GO

UPDATE
	dbo.CommunicationCategory
SET
	defaultFrequency = 3,
	availableFrequencies = '3'
WHERE
	name = 'MemberUpdate'
GO

-- Update all settings to the new default.

UPDATE
	dbo.CommunicationCategorySettings
SET
	frequency = 3
WHERE
	frequency IN (0, 2, 3)
	AND categoryId = '6E92DE26-E536-459D-9641-EE4F34473DAD'

