DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = '70F83B10-B74C-4ebe-9832-DDF0B600C7FD'

SET @intgeratorUserId = 'D08CB0F5-871A-456d-9CC7-29DA68F8EA8A'

SET @integratorUserName = 'DreamJob'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, @integratorUserName)

/*
password: 	aaaaaa                  C056Dl/oStNftflbnO6seQ==
			yW/SPWdN                OPdarVKpg4uLC9ICt+gF3w==
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