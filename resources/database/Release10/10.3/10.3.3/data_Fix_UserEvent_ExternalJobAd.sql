UPDATE
	dbo.UserEvent
SET
	context = 3
WHERE
	(type = 27 OR type = 28) AND context <> 3
