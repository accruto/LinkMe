INSERT
	NetworkLink
SELECT
	ru2.id AS user2, ru1.id as user1, nl1.addedTime
FROM
	RegisteredUser ru1
INNER JOIN
	NetworkLink nl1
ON
	ru1.id = nl1.fromNetworkerId
INNER JOIN
	RegisteredUser ru2
ON
	nl1.toNetworkerId = ru2.id
WHERE
	NOT EXISTS
	(
		SELECT
			*
		FROM
			NetworkLink nl2
		WHERE
			nl2.fromNetworkerId = ru2.id
			AND nl2.toNetworkerId = ru1.id
	)
ORDER BY user2

