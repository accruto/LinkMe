DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = 'a9643d83-bb40-4f8d-be84-ef3c80fccd21'

SET @intgeratorUserId = '563d3f35-c295-4dfb-bb6d-945ec15b0828'

SET @integratorUserName = 'Trovit'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, @integratorUserName)

/*
password: 	EHlxzow8                mQ66je/SCkdbYO59/hlxhA==
*/

SET @password = 'mQ66je/SCkdbYO59/hlxhA=='

/*
IntegratorPermissions: 
	None = 0,
        PostJobAds = 1,
        GetJobApplication = 2,
        GetJobAds = 4
*/

INSERT INTO dbo.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId,  @integratorUserName, @password, @intgeratorId, 4)

GO

