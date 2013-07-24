DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = '8054F9AB-771A-4bd7-8C68-176F03718B30'

SET @intgeratorUserId = '1A7E6650-C617-4c57-830F-56D48E9AF506'

SET @integratorUserName = 'JXT'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, @integratorUserName)

/*
password: 	password                X03MO1qnZdYdgyfeuILPmQ==
			yW/SPWdN                OPdarVKpg4uLC9ICt+gF3w==
*/

SET @password = 'OPdarVKpg4uLC9ICt+gF3w=='

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