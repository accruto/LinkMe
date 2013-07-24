-- There are some duplicate rows in the existing data. Just take one of the codes (any)
-- for each user ID. It takes a monster of a query to achieve this seemingly simple goal!

INSERT INTO dbo.JoinReferral(userId, promotionCode, refererUrl, referralCode)
SELECT DISTINCT dbo.GuidFromString(upp.userProfileId), LTRIM(RTRIM(upp.code)), NULL, NULL
FROM linkme_owner.user_profile_promotions upp
WHERE NOT EXISTS
(
	SELECT *
	FROM linkme_owner.user_profile_promotions upp2
	WHERE upp2.userProfileId = upp.userProfileId AND upp2.code < upp.code
)

GO

-- Move the referral code (identified by "ref=" prefix) into their proper column.

UPDATE dbo.JoinReferral
SET referralCode = RIGHT(promotionCode, LEN(promotionCode) - 4), promotionCode = NULL
WHERE promotionCode LIKE 'ref=%'

GO
