sp_dropserver 'DEV00', droplogins
GO

sp_addserver 'DEVXX', local -- Replace DEVXX with your correct machine name
GO

EXEC sp_helpserver
GO
