DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = '9dfa1f46-09e0-43a6-915f-a8fac00fb6b4'

SET @intgeratorUserId = '97e36ec9-4e41-46ab-bf44-a508ddb07b98'

SET @integratorUserName = 'Odusee'

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
