DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)

SET @intgeratorId = '05285BBD-BEB1-4180-B95B-0A448E453790'
SET @intgeratorUserId = '97A32F8A-8F6E-4DFB-ABBD-E3BD645FD641'
SET @integratorUserName = 'ITWire_JobAdFeed'

INSERT INTO linkme_owner.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'ITWire')

--Add IT Wire user being able to only read job ads
--Password is 'Nt5#sLrW'                        
INSERT INTO linkme_owner.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId,  @integratorUserName, 'P9Iq+F2GFCguhQy89iFQyg==', @intgeratorId, 4)

GO

