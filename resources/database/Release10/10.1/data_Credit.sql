-- ApplicantCredit

DECLARE @id UNIQUEIDENTIFIER
SET @id = '{E63229B6-1F14-4f3a-9707-B09B375DA3A5}'

IF EXISTS (SELECT * FROM dbo.Credit WHERE id = @id)
	UPDATE
		dbo.Credit
	SET
		name = 'ApplicantCredit',
		displayName = 'Job applicants',
		description = 'A credit that allows a candidate to apply for one of your jobs'
	WHERE
		id = @id
ELSE
	INSERT
		dbo.Credit (id, name, displayName, description)
	VALUES
		(@id, 'ApplicantCredit', 'Job applicants', 'A credit that allows a candidate to apply for one of your jobs')

-- JobAdCredit

UPDATE
	dbo.Credit
SET
	name = 'JobAdCredit'
WHERE
	name = 'JobAd'