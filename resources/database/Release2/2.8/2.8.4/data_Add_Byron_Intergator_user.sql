DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)

SET @intgeratorId = 'A6998E83-6099-4F26-A4C4-700A0090BBBF'
SET @intgeratorUserId = 'E96F86B1-1425-45F4-B2A8-12CFE22AF362'
SET @integratorUserName = 'Byron_JobAdFeed'

INSERT INTO linkme_owner.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'Byron')

--Add IT Wire user being able to only read job ads
--password: BpJ#fA9                       

INSERT INTO linkme_owner.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId,  @integratorUserName, 'zw2355R7rbRy4SIxEH92wQ==', @intgeratorId, 4)

GO

