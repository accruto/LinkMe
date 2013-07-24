DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = '19FE3C4A-D2AB-46dc-90E2-396B53FFE8A1'
SET @intgeratorUserId = '5B698C8A-14E8-40fd-8B1D-57072EA92818'
SET @integratorUserName = 'JobSense'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, @integratorUserName)

/*
password: 	aaaaaa                  C056Dl/oStNftflbnO6seQ==
		J#40.pqS-!		ERfK8zpTR8cTFfcCU59KPQ==
*/

SET @password = 'ERfK8zpTR8cTFfcCU59KPQ=='

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