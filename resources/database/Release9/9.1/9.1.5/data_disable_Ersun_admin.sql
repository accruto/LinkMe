UPDATE dbo.RegisteredUser
SET flags = flags | 4
WHERE loginId = 'Ersun Admin' AND flags & 4 <> 4
GO
