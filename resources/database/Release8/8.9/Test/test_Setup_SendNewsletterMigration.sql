CREATE PROCEDURE CreateTestUser(@id UNIQUEIDENTIFIER, @emailAddress NVARCHAR(320), @flags SMALLINT, @hasEmailSettings BIT)
AS
BEGIN

	IF EXISTS (SELECT * FROM RegisteredUser WHERE id = @id)
		UPDATE
			RegisteredUser
		SET
			loginId = @emailAddress,
			emailAddress = @emailAddress,
			flags = @flags
		WHERE
			id = @id
	ELSE
		INSERT
			RegisteredUser (id, loginId, createdTime, emailAddress, firstName, lastName, flags, passwordHash)
		VALUES
			(@id, @emailAddress, GETDATE(), @emailAddress, 'Homer', 'Simpson', @flags, 'AAA')

	DELETE
		EmailSettings
	WHERE
		userId = @id
		
	IF (@hasEmailSettings = 1)
		INSERT
			EmailSettings (id, userId, sendPlainTextOnly)
		VALUES
			(NEWID(), @id, 0)

END

GO

DECLARE @id UNIQUEIDENTIFIER
DECLARE @emailAddress NVARCHAR(320)
DECLARE @flags SMALLINT

-- Nothing set.

SET @id = '{55815EDB-9FC3-4fd3-9B43-4B2D37820D61}'
SET @emailAddress = 'test1@test.linkme.net.au'
SET @flags = 0
EXEC CreateTestUser @id, @emailAddress, @flags, 0

-- Activated, Off, No EmailSettings

SET @id = '{F7266874-8699-4f42-99D3-2916710E5190}'
SET @emailAddress = 'test2@test.linkme.net.au'
SET @flags = 32 + 1 -- Activated / EmailVerified
EXEC CreateTestUser @id, @emailAddress, @flags, 0

-- Activated, On, No EmailSettings

SET @id = '{217301AC-8443-4087-948E-0925D7908F04}'
SET @emailAddress = 'test3@test.linkme.net.au'
SET @flags = 32 + 16 + 1 -- Activated / SendNewsletters / EmailVerified
EXEC CreateTestUser @id, @emailAddress, @flags, 0

-- Disabled, Off, No EmailSettings

SET @id = '{B162FF33-3AF2-4afe-8D2E-4C3C80C00D9A}'
SET @emailAddress = 'test4@test.linkme.net.au'
SET @flags = 32 + 4 + 1 -- Activated / Disabled / EmailVerified
EXEC CreateTestUser @id, @emailAddress, @flags, 0

-- Disabled, On, No EmailSettings

SET @id = '{0526A101-1537-449b-B10D-68DFD038B056}'
SET @emailAddress = 'test5@test.linkme.net.au'
SET @flags = 32 + 16 + 4 + 1 -- Activated / SendNewsletters / Disabled / EmailVerified
EXEC CreateTestUser @id, @emailAddress, @flags, 0

-- Activated, Off, EmailSettings

SET @id = '{053F5A30-45AB-4436-AA29-59EE06D26B77}'
SET @emailAddress = 'test6@test.linkme.net.au'
SET @flags = 32 + 1 -- Activated / EmailVerified
EXEC CreateTestUser @id, @emailAddress, @flags, 1

-- Activated, On, EmailSettings

SET @id = '{E578637A-1E5F-41f8-91FB-466E92296BBC}'
SET @emailAddress = 'test7@test.linkme.net.au'
SET @flags = 32 + 16 + 1 -- Activated / SendNewsletters / EmailVerified
EXEC CreateTestUser @id, @emailAddress, @flags, 1

-- Disabled, Off, EmailSettings

SET @id = '{3AE6E084-24C5-48ff-B4F7-00C925D6926E}'
SET @emailAddress = 'test8@test.linkme.net.au'
SET @flags = 32 + 4 + 1 -- Activated / Disabled / EmailVerified
EXEC CreateTestUser @id, @emailAddress, @flags, 1

-- Disabled, On, EmailSettings

SET @id = '{B2D6BB6C-966B-4a5d-A5F2-1BF71DD21A3E}'
SET @emailAddress = 'test9@test.linkme.net.au'
SET @flags = 32 + 16 + 4 + 1 -- Activated / SendNewsletters / Disabled / EmailVerified
EXEC CreateTestUser @id, @emailAddress, @flags, 1

GO

DROP PROCEDURE CreateTestUser
GO