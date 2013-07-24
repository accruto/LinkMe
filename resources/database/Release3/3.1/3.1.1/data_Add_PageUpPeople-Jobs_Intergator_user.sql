DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)

SET @intgeratorId = '4B9E5370-CB12-4326-9E18-B959E16BC5E9'
SET @intgeratorUserId = '50C66741-504E-4b3e-AF93-0D10F71E52A0'
SET @integratorUserName = 'PageUpPeople'

INSERT INTO dbo.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'Page Up People')

--Add Personal Concept user being able to post job ads
--password: 	aaaaaa                  C056Dl/oStNftflbnO6seQ==
--		PzL+2G_2k               uxdOFbz0QvESZllCxsK8mQ==

INSERT INTO dbo.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId,  @integratorUserName, 'uxdOFbz0QvESZllCxsK8mQ==', @intgeratorId, 3)

GO