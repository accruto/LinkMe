DECLARE @communityId UNIQUEIDENTIFIER
SET @communityId = 'E9CE8788-C5CB-44FC-AC6D-0BAF164CE04A'

INSERT
	dbo.CommunityMember (id, primaryCommunityId)
SELECT
	u.id, @communityId
FROM
	dbo.RegisteredUser AS u
INNER JOIN
	dbo.ExternalUser AS e ON e.id = u.id
WHERE
	u.loginid is null
	AND u.id NOT IN (SELECT id FROM dbo.CommunityMember)
