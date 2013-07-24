DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = '28E58E01-B550-456d-94BD-DE454321C9F3'

SET @intgeratorUserId = '858E000F-033C-4950-AAF5-18980725C3AC'

SET @integratorUserName = 'johns'

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
VALUES (@intgeratorUserId,  @integratorUserName, @password, @intgeratorId, 6)

GO