DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = 'a1ed0bb7-cff3-4424-af1f-ba8be4987189'

SET @intgeratorUserId = 'b07c560c-433b-4f77-8025-500a34f7bb27'

SET @integratorUserName = 'Jooble'

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