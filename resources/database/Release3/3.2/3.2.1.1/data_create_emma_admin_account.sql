-- Create Alison Holmes admin account
DECLARE @id UNIQUEIDENTIFIER
DECLARE @passwordHash VARCHAR(50)

SET @id = '95A0A2A5-8843-4996-A097-4AC05BCB095F'
SET @passwordHash = '4C+YKJIYzi7OVxqXoB3TfA=='

EXEC dbo.CreateAdminUser @id, 'Emma Admin', @passwordHash, 'Emma', 'Ridgewell', 'eridgewell@linkme.com.au'