DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)

SET @intgeratorId = '58117FFC-B1DE-4c4d-A452-744EB80FA5A5'
SET @intgeratorUserId = '91927D17-BF6E-4a11-8B35-0CDB1CB49A75'
SET @integratorUserName = 'RecruitLive'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'RecruitLive')

--Add Personal Concept user being able to post job ads
--password: 	aaaaaa                  C056Dl/oStNftflbnO6seQ==
--*c!sZ=TY]                       	/TF+nrEjnCn2wdNf/E7G9Q==

INSERT INTO dbo.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId,  @integratorUserName, 'C056Dl/oStNftflbnO6seQ==', @intgeratorId, 3)

GO