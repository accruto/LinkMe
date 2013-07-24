UPDATE RegisteredUser
SET flags = flags | 4
WHERE loginId = 'Art Admin' AND flags & 4 <> 4
GO
