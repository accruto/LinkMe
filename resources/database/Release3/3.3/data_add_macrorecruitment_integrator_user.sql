DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = 'E623F55A-BBAD-4676-B78D-DA017A563213'
SET @intgeratorUserId = '77ABB137-74DF-45f8-B7F7-40EC8617EFBF'
SET @integratorUserName = 'macrorecruitment'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, @integratorUserName)

/*
password: 	aaaaaa                  C056Dl/oStNftflbnO6seQ==
		M$lS=QoAz               MQ5pDKkkbY95ZpXFTUmlPA==
*/

SET @password = 'MQ5pDKkkbY95ZpXFTUmlPA=='

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