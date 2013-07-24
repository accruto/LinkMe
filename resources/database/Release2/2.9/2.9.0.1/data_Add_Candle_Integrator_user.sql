DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER

SET @intgeratorId = '20E67BA1-2341-4ea8-8915-0A6E23ED7652'
SET @intgeratorUserId = 'E7C174FC-C9A4-4e13-A7B7-24CB8F166D60'

INSERT INTO linkme_owner.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'Candle_intergator')

-- password is %Go19Z4@                        
INSERT INTO linkme_owner.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId, 'Candle_intergator', 'gFASx9ZcVpPiqAoNX2NTow==', @intgeratorId, 3)

GO

