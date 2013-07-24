DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER

SET @intgeratorId = '8DC78A45-4F24-41DB-96DB-F7220B65B41F'
SET @intgeratorUserId = 'A7429B7D-DC34-46A0-A6C7-A5E14A22845F'

INSERT INTO linkme_owner.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'dmi_intergator')

-- password is Zx0kWm9aL
INSERT INTO linkme_owner.IntegratorUser ([id], username, [password], integratorId)
VALUES (@intgeratorUserId, 'dmi_intergator', 'V0g1r2YnTn6ziAoQSTpakQ==', @intgeratorId)

GO

