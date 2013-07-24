DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = '8AB9F0E9-35E3-48d4-AB04-1170E09F5FFE'

SET @intgeratorUserId = '4D86BFED-0FCD-4c6d-8A9A-A1352BB602A7'

SET @integratorUserName = 'JobServe'

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
VALUES (@intgeratorUserId,  @integratorUserName, @password, @intgeratorId, 7)

GO