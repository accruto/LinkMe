DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = '0E51AB27-8121-479e-982B-57B941912CE6'

SET @intgeratorUserId = '8DF592C1-CA7A-4b8f-AB02-BD588478A8BB'

SET @integratorUserName = 'AllJobs'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, @integratorUserName)

/*
password: 	aaaaaa                  C056Dl/oStNftflbnO6seQ==
			YO59/hlx                vx5+oJHOuj363fkW+/BWIw==
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