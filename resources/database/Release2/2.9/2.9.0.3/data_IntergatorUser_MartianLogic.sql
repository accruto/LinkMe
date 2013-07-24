DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)

SET @intgeratorId = 'ce10d4b0-dcde-4ecc-b857-995e07813b12'
SET @intgeratorUserId = '95909d42-f5af-4126-a51f-d6a665b5a7a2'

INSERT INTO linkme_owner.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'Martian Logic')

-- Password: b2cb3f65
INSERT INTO linkme_owner.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId,  'adlogicjobs', 'OLvq62iPzJAUj4scm89Qwg==', @intgeratorId, 3)

GO
