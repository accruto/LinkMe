
-- Make sure there are email settings for everyone.

INSERT
	EmailSettings (id, userId, sendPlainTextOnly)
SELECT
	NEWID(), id, 0
FROM
	RegisteredUser
WHERE
	id NOT IN (SELECT userId FROM EmailSettings)

-- Set the flag in EmailSettings

UPDATE
	EmailSettings
SET
	sendPlainTextOnly = 1
FROM
	EmailSettings AS ES
INNER JOIN
	RegisteredUser AS RU ON ES.userId = RU.id
WHERE
	RU.flags & 8 = 8

-- Clear the flag in RegisteredUser

UPDATE
	RegisteredUser
SET
	flags = flags & ~8
WHERE
	flags & 8 = 8


