DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER

-- The MyCareer integrator is not actually used to call any web services, so it
-- does not need any permissions or a password.

SET @intgeratorId = 'A52E664C-6282-40b2-A09B-6FBD03A2F5B0'
SET @intgeratorUserId = 'D50FBB3E-C8F6-4055-8681-D4B27C298173'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'MyCareer')

INSERT INTO dbo.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId,  'MyCareer-jobs', REPLICATE('*', 24), @intgeratorId, 0)

GO
