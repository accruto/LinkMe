
-- Create settings for each user who has been sent a reminder.

INSERT
	EmailSettings (id, userId, sendPlainTextOnly)
SELECT
	NEWID(), id, 0
FROM
	Candidate
WHERE
	resumeReminderLastSentTime IS NOT NULL

-- Create the reminder settings for those users as well.

INSERT
	EmailDefinitionSettings (id, definitionid, settingsid, lastSentTime)
SELECT
	newid(),
	(SELECT id FROM EmailDefinition WHERE name = 'ResumeReminderEmail'),
	s.id,
	c.resumeReminderLastSentTime
FROM
	EmailSettings AS s
INNER JOIN
	Candidate AS c ON c.id = s.userId
WHERE
	c.resumeReminderLastSentTime IS NOT NULL

