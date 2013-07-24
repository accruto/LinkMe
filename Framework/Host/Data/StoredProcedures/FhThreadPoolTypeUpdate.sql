-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhThreadPoolTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhThreadPoolTypeUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhThreadPoolTypeUpdate
(
	@name AS VARCHAR(128),
	@class VARCHAR(512),
	@configurationHandlerClass VARCHAR(512),
	@configurationPropertyPageClass VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the thread pool type.

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FhThreadPoolType
WHERE name = @name

IF @id IS NULL
BEGIN
	-- ThreadPoolType doesn't exist - insert it.

	INSERT INTO dbo.FhThreadPoolType (id, name, class, configurationHandlerClass, configurationPropertyPageClass)
	VALUES (NEWID(), @name, @class, @configurationHandlerClass, @configurationPropertyPageClass)
END
ELSE
BEGIN
	-- ThreadPoolType exists - update it.

	UPDATE dbo.FhThreadPoolType
	SET
		class = @class,
		configurationHandlerClass = @configurationHandlerClass,
		configurationPropertyPageClass = @configurationPropertyPageClass
	WHERE id = @id
END

RETURN 0

END
GO