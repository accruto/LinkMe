SELECT r.[id]
FROM dbo.Resume r
INNER JOIN dbo.RegisteredUser ru
ON r.[id] = ru.[id]
WHERE r.lastEditedTime > '2008-01-05' OR ru.createdTime > '2008-01-05'
