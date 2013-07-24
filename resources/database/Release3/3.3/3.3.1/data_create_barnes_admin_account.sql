-- Create Jessica Admin account
DECLARE @id UNIQUEIDENTIFIER
DECLARE @passwordHash VARCHAR(50)

SET @id = '13A23C95-52EB-4996-A097-4ABE5BDB095F'
SET @passwordHash = '4C+YKJIYzi7OVxqXoB3TfA=='

EXEC dbo.CreateAdminUser @id, 'Barnes Admin', @passwordHash, 'Sarah', 'Barnes', 'sbarnes@linkme.com.au'