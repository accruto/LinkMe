DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = 'A0B73038-7AFF-41f7-B7FF-8EF3CA58E695'

SET @intgeratorUserId = '7DF828CD-B930-4400-BD6C-B1D2355EBD60'

SET @integratorUserName = 'FastTrack'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, @integratorUserName)

/*
password: 	password                X03MO1qnZdYdgyfeuILPmQ==
			T6jkiR_e                YW6mPu9cSsAatjETtbXjMw==
*/

SET @password = 'YW6mPu9cSsAatjETtbXjMw=='

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