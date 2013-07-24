DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER

SET @intgeratorId = 'f79eb506-f46d-4c9d-b9bf-1fbd4944bd2d'
SET @intgeratorUserId = '486d78b2-cf2f-4156-a9ec-4bf14b1b4c29'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'Broadbean')

INSERT INTO dbo.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId,  'Broadbean-jobs', 'dAfur542SUUtmtmukFLMfA==', @intgeratorId, 3)

GO
