DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = 'AD3207AE-2E5B-4d1c-BB9C-232AA8B3FA8C'

SET @intgeratorUserId = '31473A33-AE2E-4a0a-9BFE-9A14EB4260E7'

SET @integratorUserName = 'SimplyHiring'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, @integratorUserName)

/*
password: 	aaaaaa                  C056Dl/oStNftflbnO6seQ==
			kW+/BWIw                dYfGixMcf8opYv2BhFGSwg==
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
VALUES (@intgeratorUserId,  @integratorUserName, @password, @intgeratorId, 4)

GO