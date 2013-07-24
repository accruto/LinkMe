UPDATE RegisteredUser
SET flags = flags | 4
WHERE loginId IN ('linkmeadmin', 'Corinne Admin')

GO
