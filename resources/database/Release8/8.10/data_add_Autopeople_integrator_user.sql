DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = '9B459DEF-46AA-40c4-93AE-A682D502ABB2'

SET @intgeratorUserId = 'E149BB3A-DB70-4a22-9082-1F8FFBFC2D6A'

SET @integratorUserName = 'autopeople-jobs'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, @integratorUserName)

/*
password: 	aaaaaa                  C056Dl/oStNftflbnO6seQ==
			P9Iq+F2G                MUgwUdMMx6//yW/SPWdN3w==
*/

SET @password = 'C056Dl/oStNftflbnO6seQ=='

/*
IntegratorPermissions: 
	None = 0,
        PostJobAds = 1,
        GetJobApplication = 2,
        GetJobAds = 4
*/

INSERT INTO dbo.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId,  @integratorUserName, @password, @intgeratorId, 3)

GO