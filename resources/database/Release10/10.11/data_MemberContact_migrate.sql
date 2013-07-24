INSERT
	dbo.MemberContact (id, time, reason, employerId, memberId, exercisedCreditId)
SELECT
	e.id AS id,
	e.time AS time,
	CASE e.type WHEN 4 THEN 0 ELSE 1 END AS reason,
	e.actorId AS employerId,
	u.userId AS memberId,
	NULL
FROM
	dbo.UserEvent AS e
INNER JOIN
	dbo.UserEventActionedUser AS u on u.eventId = e.id
WHERE
	type IN (4, 5)
