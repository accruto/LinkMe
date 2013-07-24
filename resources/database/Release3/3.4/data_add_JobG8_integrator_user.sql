DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = 'E61335AB-A34E-4aae-A8E0-9FD329EC9DCD'

SET @intgeratorUserId = '3BAE70FC-350B-4778-953A-05226170602B'

SET @integratorUserName = 'JobG8'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, @integratorUserName)

/*
password: 	aaaaaa                  C056Dl/oStNftflbnO6seQ==
			T6jkiR_e                YW6mPu9cSsAatjETtbXjMw==
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