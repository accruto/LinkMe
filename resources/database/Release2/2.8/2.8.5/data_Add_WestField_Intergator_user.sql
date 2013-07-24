DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)
DECLARE @password NVARCHAR(255)

SET @intgeratorId = 'F5156A7C-647F-4bfb-8D26-4E356C3CB2AA'
SET @intgeratorUserId = '9CD58F62-0274-4c6c-8D53-97F20E319B03'
SET @integratorUserName = 'Westfield_IU'

INSERT INTO linkme_owner.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'Westfield')

--Add IT Wire user being able to only read job ads
--password: wF@45X0a                        
SET @password = 'PXydtoQX9zUME6+2QqjoPw=='

INSERT INTO linkme_owner.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId,  @integratorUserName, @password, @intgeratorId, 5)

GO

