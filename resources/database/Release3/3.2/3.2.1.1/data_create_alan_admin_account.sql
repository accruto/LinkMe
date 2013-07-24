-- Create Alan Cocks admin account
DECLARE @id UNIQUEIDENTIFIER
DECLARE @passwordHash VARCHAR(50)

SET @id = 'E5A0A295-83EB-4996-A097-4ACE5BDB095F'
SET @passwordHash = '4C+YKJIYzi7OVxqXoB3TfA=='

EXEC dbo.CreateAdminUser @id, 'Alan Admin', @passwordHash, 'Alan', 'Sparkes', 'acocks@linkme.com.au'