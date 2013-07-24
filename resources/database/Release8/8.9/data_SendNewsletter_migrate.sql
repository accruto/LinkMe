
-- Make sure there are email settings for everyone.

INSERT
	EmailSettings (id, userId, sendPlainTextOnly)
SELECT
	NEWID(), id, 0
FROM
	RegisteredUser
WHERE
	id NOT IN (SELECT userId FROM EmailSettings)

DECLARE @categoryId UNIQUEIDENTIFIER

-- Reminder

SELECT
	@categoryId = id
FROM
	EmailCategory
WHERE
	name = 'Reminder'

INSERT
	EmailCategorySettings (id, categoryId, settingsId, suppress)
SELECT
	NEWID(), @categoryId, ES.id, 1
FROM
	EmailSettings AS ES
INNER JOIN
	RegisteredUser AS RU ON RU.id = ES.userId
WHERE
	RU.flags & 16 = 0

-- Newsletter

SELECT
	@categoryId = id
FROM
	EmailCategory
WHERE
	name = 'Newsletter'

INSERT
	EmailCategorySettings (id, categoryId, settingsId, suppress)
SELECT
	NEWID(), @categoryId, ES.id, 1
FROM
	EmailSettings AS ES
INNER JOIN
	RegisteredUser AS RU ON RU.id = ES.userId
WHERE
	RU.flags & 16 = 0

-- Clear the flag in RegisteredUser

UPDATE
	RegisteredUser
SET
	flags = flags & ~16
WHERE
	flags & 16 = 16


