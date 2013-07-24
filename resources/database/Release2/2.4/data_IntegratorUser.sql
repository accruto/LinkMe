DECLARE @intgeratorId UNIQUEIDENTIFIER

SET @intgeratorId = NEWID()

INSERT INTO linkme_owner.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'turboRECRUIT')

INSERT INTO linkme_owner.IntegratorUser ([id], username, [password], integratorId)
VALUES (NEWID(), 'turborecruit-jobs', '54X4/43xFFVKwAou4rd8MQ==', @intgeratorId)

GO
