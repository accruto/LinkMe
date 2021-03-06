INSERT
	dbo.ExternalApplication (id, createdTime, positionId, applicantId, isDeleted)
SELECT TOP 1000
	e.id, e.time, jobAdId, e.actorId, 0
FROM
	dbo.UserEventActionedJobAd AS j
INNER JOIN
	dbo.UserEvent AS e ON j.eventId = e.id
WHERE
	e.type = 27
	AND e.id NOT IN (SELECT id FROM dbo.ExternalApplication)

SELECT
	count(*)
FROM
	dbo.ExternalApplication

SELECT
	count(*)
FROM
	dbo.UserEventActionedJobAd AS j
INNER JOIN
	dbo.UserEvent AS e ON j.eventId = e.id
WHERE
	e.type = 27
	AND e.id NOT IN (SELECT id FROM dbo.ExternalApplication)
