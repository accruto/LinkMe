UPDATE dbo.JoinReferral
SET referralCode = SUBSTRING(referralCode, 5, LEN(referralCode) - 4)
WHERE referralCode LIKE 'ref=%'
GO
