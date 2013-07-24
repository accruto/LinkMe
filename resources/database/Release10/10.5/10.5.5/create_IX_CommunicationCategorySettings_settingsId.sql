IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CommunicationCategorySettings]') AND name = N'IX_CommunicationCategorySettings_settingsId')
DROP INDEX [IX_CommunicationCategorySettings_settingsId] ON [dbo].[CommunicationCategorySettings] WITH ( ONLINE = OFF )


CREATE NONCLUSTERED INDEX IX_CommunicationCategorySettings_settingsId ON dbo.CommunicationCategorySettings 
(
	settingsId ASC
)