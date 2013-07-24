DECLARE @intgeratorId UNIQUEIDENTIFIER
DECLARE @intgeratorUserId UNIQUEIDENTIFIER
DECLARE @integratorUserName NVARCHAR(255)

SET @intgeratorId = '35E7A6F8-0A5E-4250-9581-A7240EA03E48'
SET @intgeratorUserId = 'B0341465-929A-485D-B60D-2EBABDCA90CF'
SET @integratorUserName = 'RecruitDotNet_JobAdFeed'

INSERT INTO linkme_owner.AtsIntegrator ([id], [name])
VALUES (@intgeratorId, 'RecruitDotNet')

--Add IT Wire user being able to only read job ads
--password: Rd3Jp$m                 

INSERT INTO linkme_owner.IntegratorUser ([id], username, [password], integratorId, [permissions])
VALUES (@intgeratorUserId,  @integratorUserName, 'ek2ws3I62KYeNdWLb2pzrQ==', @intgeratorId, 4)

GO

