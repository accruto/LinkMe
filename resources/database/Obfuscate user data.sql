-- User email addresses

UPDATE dbo.RegisteredUser
SET emailAddress = REPLACE(emailAddress, '@', '+') + '@test.linkme.net.au'
WHERE emailAddress NOT LIKE '%@test.linkme.net.au'

UPDATE dbo.RegisteredUser
SET secondaryEmailAddress = REPLACE(secondaryEmailAddress, '@', '+') + '@test.linkme.net.au'
WHERE NOT secondaryEmailAddress IS NULL
AND secondaryEmailAddress NOT LIKE '%@test.linkme.net.au'

UPDATE dbo.RegisteredUser
SET loginId = REPLACE(loginId, '@', '+') + '@test.linkme.net.au'
WHERE loginId LIKE '%@%' AND loginId NOT LIKE '%@test.linkme.net.au'

-- Invitations, verifications, non-member settings

UPDATE dbo.NetworkInvitation
SET inviteeEmailAddress = REPLACE(inviteeEmailAddress, '@', '+') + '@test.linkme.net.au'
WHERE inviteeEmailAddress NOT LIKE '%@test.linkme.net.au'

UPDATE dbo.EmailVerification
SET emailAddress = REPLACE(emailAddress, '@', '+') + '@test.linkme.net.au'
WHERE emailAddress NOT LIKE '%@test.linkme.net.au'

UPDATE dbo.NonMemberSettings
SET emailAddress = REPLACE(emailAddress, '@', '+') + '@test.linkme.net.au'
WHERE emailAddress NOT LIKE '%@test.linkme.net.au'

UPDATE dbo.GroupJoinInvitation
SET inviteeEmail = REPLACE(inviteeEmail, '@', '+') + '@test.linkme.net.au'
WHERE inviteeEmail NOT LIKE '%@test.linkme.net.au'

UPDATE dbo.GroupEventInvitation
SET inviteeEmailAddress = REPLACE(inviteeEmailAddress, '@', '+') + '@test.linkme.net.au'
WHERE inviteeEmailAddress NOT LIKE '%@test.linkme.net.au'

-- Last names and passwords

DECLARE @testLastName VARCHAR(20)
DECLARE @testPassword VARCHAR(24)
SET @testLastName = 'Flinstone'
SET @testPassword = 'X03MO1qnZdYdgyfeuILPmQ==' -- 'password'

UPDATE dbo.RegisteredUser
SET lastName = @testLastName
WHERE lastName <> @testLastName AND firstName <> 'Linking' AND lastName <> 'Node'

UPDATE dbo.RegisteredUser
SET [passwordHash] = @testPassword
WHERE [passwordHash] <> @testPassword

-- Member phone numbers

DECLARE @testPhoneNumber VARCHAR(20)
SET @testPhoneNumber = '5555 5555'

UPDATE dbo.Member
SET primaryPhoneNumber = @testPhoneNumber
WHERE primaryPhoneNumber IS NOT NULL AND primaryPhoneNumber <> @testPhoneNumber

UPDATE dbo.Member
SET secondaryPhoneNumber = @testPhoneNumber
WHERE secondaryPhoneNumber IS NOT NULL AND secondaryPhoneNumber <> @testPhoneNumber

UPDATE dbo.Member
SET tertiaryPhoneNumber = @testPhoneNumber
WHERE tertiaryPhoneNumber IS NOT NULL AND tertiaryPhoneNumber <> @testPhoneNumber

-- Employer phone numbers

UPDATE dbo.Employer
SET contactPhoneNumber = @testPhoneNumber
WHERE contactPhoneNumber <> @testPhoneNumber

-- ContactDetails table - used for job ad contact details

UPDATE dbo.ContactDetails
SET phoneNumber = @testPhoneNumber
WHERE phoneNumber IS NOT NULL AND phoneNumber <> @testPhoneNumber

UPDATE dbo.ContactDetails
SET faxNumber = @testPhoneNumber
WHERE faxNumber IS NOT NULL AND faxNumber <> @testPhoneNumber

UPDATE dbo.ContactDetails
SET email = REPLACE(email, '@', '+') + '@test.linkme.net.au'
WHERE email NOT LIKE '%@test.linkme.net.au'

UPDATE dbo.ContactDetails
SET lastName = @testLastName
WHERE lastName <> @testLastName

-- Integrator passwords

UPDATE dbo.IntegratorUser
SET [password] = @testPassword
WHERE [password] <> @testPassword

-- Networker event deltas - we have to use the event type for these

UPDATE dbo.NetworkerEvent
SET [from] = @testPhoneNumber, [to] = @testPhoneNumber
WHERE type IN (13, 18, 19) AND ([from] <> @testPhoneNumber OR [to] <> @testPhoneNumber)

UPDATE dbo.NetworkerEvent
SET [from] = REPLACE([from], '@', '+') + '@test.linkme.net.au'
WHERE type = 17 AND [from] NOT LIKE '%@test.linkme.net.au'

UPDATE dbo.NetworkerEvent
SET [to] = REPLACE([to], '@', '+') + '@test.linkme.net.au'
WHERE type = 17 AND [to] NOT LIKE '%@test.linkme.net.au'

GO
