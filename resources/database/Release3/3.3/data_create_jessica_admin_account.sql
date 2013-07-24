-- Create Jessica Admin account
DECLARE @id UNIQUEIDENTIFIER
DECLARE @passwordHash VARCHAR(50)

SET @id = 'D5ABBC95-83EB-4996-A097-4ACE5BDB095F'
SET @passwordHash = '4C+YKJIYzi7OVxqXoB3TfA=='

EXEC dbo.CreateAdminUser @id, 'Jessica Admin', @passwordHash, 'Jessica', 'Graham', 'jgraham@linkme.com.au'