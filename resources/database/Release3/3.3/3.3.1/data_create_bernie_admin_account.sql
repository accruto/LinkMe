-- Create Jessica Admin account
DECLARE @id UNIQUEIDENTIFIER
DECLARE @passwordHash VARCHAR(50)

SET @id = 'A5A23C95-53EB-4996-A097-4ACE5BDB095F'
SET @passwordHash = '4C+YKJIYzi7OVxqXoB3TfA=='

EXEC dbo.CreateAdminUser @id, 'Bernie Admin', @passwordHash, 'Bernadette', 'Grinyer', 'bgrinyer@linkme.com.au'