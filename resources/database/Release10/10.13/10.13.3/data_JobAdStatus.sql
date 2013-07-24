-- For those jobads that do not have a 'open' change create it.
-- Assume the job was opened the same time it was created.

INSERT
	dbo.JobAdStatus (id, jobAdId, time, previousStatus, newStatus)
SELECT
	NEWID(), j.id, j.createdTime, 1, 2
FROM
	dbo.JobAd AS j
WHERE
	j.id NOT IN
	(
		SELECT DISTINCT
			jobAdId
		FROM
			dbo.JobAdStatus
		WHERE
			previousStatus = 1 and newStatus = 2
	)
	AND (j.status = 2 OR j.status = 3)

-- For those job ads that do not have a 'close' change but are closed create it.

-- Use user event.

INSERT
	dbo.JobAdStatus (id, jobAdId, time, previousStatus, newStatus)
SELECT
	NEWID(), j.id, u.time, 2, 3
FROM
	dbo.JobAd AS j
INNER JOIN
	dbo.UserEventActionedJobAd AS uj ON uj.jobAdId = j.id
INNER JOIN
	dbo.UserEvent AS u ON u.id = uj.eventId
WHERE
	j.id NOT IN
	(
		SELECT DISTINCT
			jobAdId
		FROM
			dbo.JobAdStatus
		WHERE
			previousStatus = 2 and newStatus = 3
	)
	AND j.status = 3
	AND u.type = 20

-- If no user event use expiry date.

INSERT
	dbo.JobAdStatus (id, jobAdId, time, previousStatus, newStatus)
SELECT
	NEWID(), j.id, j.expiryTime, 2, 3
FROM
	dbo.JobAd AS j
WHERE
	j.id NOT IN
	(
		SELECT DISTINCT
			jobAdId
		FROM
			dbo.JobAdStatus
		WHERE
			previousStatus = 2 and newStatus = 3
	)
	AND j.status = 3
