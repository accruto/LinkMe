INSERT INTO dbo.UserEventActionedGroup (eventId, groupId)
SELECT eventId, dbo.GuidFromString([value])
FROM dbo.UserEventExtraData
WHERE [key] = 'groupId'
GO

DELETE dbo.UserEventExtraData
WHERE [key] = 'groupId'
GO
