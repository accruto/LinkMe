DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER

-- The CareerOne integrator is not actually used to call any web services, so it
-- does not need any permissions or a password.

SET @intgeratorId = 'b13fe5d6-c425-4120-a25e-7f5dac643f6b'
SET @intgeratorUserId = '9b732acd-17ea-4500-8011-59952466e15d'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'CareerOne')

INSERT INTO dbo.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId,  'CareerOne-jobs', REPLICATE('*', 24), @intgeratorId, 0)

GO
