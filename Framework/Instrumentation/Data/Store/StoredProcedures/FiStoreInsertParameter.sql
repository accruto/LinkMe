-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreInsertParameter]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiStoreInsertParameter]
GO


-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiStoreInsertParameter
(
	@messageId INT,
	@sequence INT,
	@name VARCHAR(128),
	@format INT,
	@type VARCHAR(512),
	@string NTEXT,
	@binary IMAGE
)
AS

SET NOCOUNT ON

DECLARE @error INT
DECLARE @typeId INT

IF @type IS NULL
	SET @typeId = NULL
ELSE
BEGIN
	-- Find or create the type ID.

	SELECT
		@typeId = id
	FROM
		dbo.FiStoreType
	WHERE
		fullName = @type

	IF @typeId IS NULL
	BEGIN
		INSERT
			dbo.FiStoreType (fullName)
		VALUES
			(@type)

		SET @error = @@ERROR

		IF (@error = 0)
		BEGIN
			SET @typeId = @@IDENTITY
		END
		ELSE IF (@error = 2601)
		BEGIN
			-- Another connection inserted this row - select it again.

			SELECT
				@typeId = id
			FROM
				dbo.FiStoreType
			WHERE
				fullName = @type
		END
		ELSE
		BEGIN
			RAISERROR( 'Failed to get or create an entry for Type ''%s''.', 16, 1, @type)
			RETURN 1
		END
	END
END

-- Insert the parameter.

INSERT INTO dbo.FiStoreParameter (messageId, sequence, name, format, typeId, string, binary)
VALUES (@messageId, @sequence, @name, @format, @typeId, @string, @binary)

RETURN 0
GO
