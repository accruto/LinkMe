ALTER TABLE dbo.Campaign
ADD communicationDefinitionId UNIQUEIDENTIFIER NULL
GO

ALTER TABLE dbo.Campaign
ADD CONSTRAINT FK_Campaign_CommunicationDefinition FOREIGN KEY (communicationDefinitionId)
REFERENCES dbo.CommunicationDefinition (id)
GO

