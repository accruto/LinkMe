DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = '31ee6a2b-f335-4a58-bfab-6839655eaf0d'

SET @intgeratorUserId = 'de070ae5-a1db-4352-9fc1-604f7dc58c1a'

SET @integratorUserName = 'SimplyHired'

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