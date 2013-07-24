
DECLARE @id UNIQUEIDENTIFIER
DECLARE @name NVARCHAR(100)
DECLARE @categoryId UNIQUEIDENTIFIER

-- ResumeReminderEmail

SET @id = '{5782AF4A-583C-4945-B957-9C3313902A8A}'
SET @name = 'ResumeReminderEmail'

SELECT @categoryId = id FROM EmailCategory WHERE name = 'Reminder'

IF EXISTS (SELECT * FROM dbo.EmailDefinition WHERE id = @id)
	UPDATE
		dbo.EmailDefinition
	SET
		name = @name,
		categoryId = @categoryId
	WHERE
		id = @id
ELSE
	INSERT
		dbo.EmailDefinition (id, name, categoryId)
	VALUES
		(@id, @name, @categoryId)

-- EmployerExpiryEmail

SET @id = '{8352CE8A-64F3-4716-BC9A-B2DFB1D30950}'
SET @name = 'EmployerExpiryEmail'

SELECT @categoryId = id FROM EmailCategory WHERE name = 'Reminder'

IF EXISTS (SELECT * FROM dbo.EmailDefinition WHERE id = @id)
	UPDATE
		dbo.EmailDefinition
	SET
		name = @name,
		categoryId = @categoryId
	WHERE
		id = @id
ELSE
	INSERT
		dbo.EmailDefinition (id, name, categoryId)
	VALUES
		(@id, @name, @categoryId)

-- EmployerExpiryEmail

SET @id = '{89328497-0779-4a12-919E-105F6706422B}'
SET @name = 'EmployerUsageEmail'

SELECT @categoryId = id FROM EmailCategory WHERE name = 'Reminder'

IF EXISTS (SELECT * FROM dbo.EmailDefinition WHERE id = @id)
	UPDATE
		dbo.EmailDefinition
	SET
		name = @name,
		categoryId = @categoryId
	WHERE
		id = @id
ELSE
	INSERT
		dbo.EmailDefinition (id, name, categoryId)
	VALUES
		(@id, @name, @categoryId)



