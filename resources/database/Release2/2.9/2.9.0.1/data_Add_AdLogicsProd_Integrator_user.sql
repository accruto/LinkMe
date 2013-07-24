DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER

SET @intgeratorId = '7701C4A4-E96B-4226-9392-6872E600A815'
SET @intgeratorUserId = '69CB6BEE-2939-4296-B7D6-65146E2F81C1'

INSERT INTO linkme_owner.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'adlogicprod')

-- password is F-m$aS5L                        
INSERT INTO linkme_owner.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId, 'adlogicprod', 'l7XF67NbxthSuBhTvK40Zg==', @intgeratorId, 3)

GO

