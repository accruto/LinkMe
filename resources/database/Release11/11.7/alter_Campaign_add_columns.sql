ALTER TABLE dbo.Campaign
ADD communicationCategoryId UNIQUEIDENTIFIER NULL
GO

ALTER TABLE dbo.Campaign
ADD CONSTRAINT FK_Campaign_CommunicationCategory FOREIGN KEY (communicationCategoryId)
REFERENCES dbo.CommunicationCategory (id)
GO

UPDATE
	dbo.Campaign
SET
	communicationCategoryId = 'C353FE96-A654-42FF-81D8-2051AA146DFC'
GO

