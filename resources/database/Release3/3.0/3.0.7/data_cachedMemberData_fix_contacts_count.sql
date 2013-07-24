-- Update the cached data to include the actual number of network links in a networkers account

UPDATE
	CachedMemberData
SET
	contactCount = sums.total
FROM
	(
	SELECT
		un.id, COUNT(*) AS total
	FROM
		Networker AS un
	INNER JOIN
		NetworkLink AS nl
	ON	nl.fromNetworkerId = un.id
	INNER JOIN
		RegisteredUser AS ru
	ON	ru.id = nl.toNetworkerId 
	AND	ru.flags & 0x04 != 0x04
	GROUP BY
		un.id
	) AS sums
WHERE 
	memberId = sums.id

GO