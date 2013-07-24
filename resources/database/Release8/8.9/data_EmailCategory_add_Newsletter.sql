
DECLARE @id UNIQUEIDENTIFIER
DECLARE @name NVARCHAR(100)

-- Newsletter

SET @id = '{292A3436-A928-4b95-B6D3-0EA4113F215F}'
SET @name = 'Newsletter'

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

