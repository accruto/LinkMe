DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)

SET @intgeratorId = '207E08E2-7F27-4f09-A2E2-431A49EE6D10'
SET @intgeratorUserId = '285DDA7F-37CB-4ffb-B790-87BD2CC49BE5'
SET @integratorUserName = 'PersonalConcept'

INSERT INTO linkme_owner.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'Personal Concept')

--Add Personal Concept user being able to post job ads
--password: Pc9JP#0

INSERT INTO linkme_owner.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId,  @integratorUserName, 'HR1Xp6WROzWnEID/C7/ceA==', @intgeratorId, 1)

GO

