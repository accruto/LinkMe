-- Only migrate pending email verification codes - there is no need to store used (activated) ones.
-- Assume all existing verifications are for the user's primary email address. This is not
-- foolproof (see defect 1516), but it will have to do.

INSERT INTO dbo.EmailVerification([id], createdTime, emailAddress, verificationCode, userId)
SELECT NEWID(), ev.accountCreatedDate, ru.emailAddress, ev.activationCode, ru.[id]
FROM linkme_owner.email_verification ev
INNER JOIN dbo.RegisteredUser ru
ON dbo.GuidFromString(ev.userProfileId) = ru.[id]
WHERE activated = 0
GO
