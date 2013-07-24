-- Find all the users with duplicate login IDs. Leave the first one, but prefix the rest with
-- underscores to make them unique. The first one (the one left as is) should be the most recently
-- created active account.

DECLARE userCursor CURSOR FOR
SELECT up.id, userId
FROM linkme_owner.user_profile up
LEFT OUTER JOIN linkme_owner.employer_profile ep
ON up.id = ep.id
WHERE userId IN
(
 SELECT userId
 FROM linkme_owner.user_profile
 GROUP BY userId
 HAVING COUNT(*) > 1
)
ORDER BY userId, active DESC, joinDate DESC

OPEN userCursor

DECLARE @id VARCHAR(50)
DECLARE @userId VARCHAR(150)
DECLARE @lastUserId VARCHAR(150)
DECLARE @fixedUserId VARCHAR(150)

SET @lastUserId = NULL

FETCH NEXT FROM userCursor
INTO @id, @userId

WHILE @@FETCH_STATUS = 0
BEGIN
	IF (@userId = @lastUserId)
	BEGIN
		-- Duplicate - prefix it with an underscore.

		SET @fixedUserId = '_' + @fixedUserId

		UPDATE linkme_owner.user_profile
		SET userId = @fixedUserId
		WHERE [id] = @id
	END
	ELSE
	BEGIN
		SET @lastUserId = @userId
		SET @fixedUserId = @userId
	END

	-- Fetch the next row.

	FETCH NEXT FROM userCursor
	INTO @id, @userId
END

CLOSE userCursor
DEALLOCATE userCursor

GO
