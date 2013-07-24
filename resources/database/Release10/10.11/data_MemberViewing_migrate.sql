INSERT
	dbo.MemberViewing (id, time, employerId, memberId, jobAdId)
SELECT
	e.id AS id,
	e.time AS time,
	e.actorId AS employerId,
	r.resumeId AS memberId,
	j.jobAdId AS jobAdId
FROM
	dbo.UserEvent AS e
INNER JOIN
	dbo.UserEventActionedResume AS r on r.eventId = e.id
LEFT OUTER JOIN
	dbo.UserEventActionedJobAd AS j ON j.eventId = e.id
WHERE
	type = 3
