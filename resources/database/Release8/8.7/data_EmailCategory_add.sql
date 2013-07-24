
DECLARE @id UNIQUEIDENTIFIER
DECLARE @name NVARCHAR(100)

-- Reminder

SET @id = '{9F57430E-64EC-4a81-9F5B-24518BB88789}'
SET @name = 'Reminder'

IF EXISTS (SELECT * FROM dbo.EmailCategory WHERE id = @id)
	UPDATE
		dbo.EmailCategory
	SET
		name = @name
	WHERE
		id = @id
ELSE
	INSERT
		dbo.EmailCategory (id, name)
	VALUES
		(@id, @name)

