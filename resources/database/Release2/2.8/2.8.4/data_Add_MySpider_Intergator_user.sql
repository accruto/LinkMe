DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)

SET @intgeratorId = '7DAC081A-2D2D-4A8A-80CA-783E5188570C'
SET @intgeratorUserId = '7FA40734-E276-40D7-AAC4-4ACDA2F38C19'
SET @integratorUserName = 'MySpider_JobAdFeed'

INSERT INTO linkme_owner.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'MySpider')

--Add IT Wire user being able to only read job ads
--password: Ms7Ja*F               

INSERT INTO linkme_owner.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId,  @integratorUserName, 'JUwJhMKXhVmBRNGSGShWTA==', @intgeratorId, 4)

GO

