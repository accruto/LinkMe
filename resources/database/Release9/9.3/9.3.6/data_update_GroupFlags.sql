UPDATE
	[group]
SET
	flags = flags | 2
WHERE
	whoCanJoin = 2 OR whoCanJoin = 3

