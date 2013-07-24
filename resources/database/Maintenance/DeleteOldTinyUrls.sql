IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteOldTinyUrls]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteOldTinyUrls]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE [dbo].[DeleteOldTinyUrls]
(
	@startDaysAgo AS INT,
	@days AS INT
)
AS

SET NOCOUNT ON

-- Determine the date.

DECLARE @date AS DATETIME
SET @date = DATEADD(dd, 0, DATEDIFF(dd, 0, DATEADD(d, -1 * @startDaysAgo, GETDATE())))

DECLARE @createdTime DATETIME

-- There are 24 * 60 * 60 = 86400 1 second intervals in a day.

DECLARE @interationLength INT
SET @interationLength = 1

DECLARE @iterations INT
SET @iterations = @days * 86400

DECLARE @interval INT
SET @interval = 1

DECLARE @index INT
SET @index = 0

WHILE @index < @iterations
BEGIN
	SET @createdTime = DATEADD(s, @index * @interval * @interationLength, @date)

	DELETE
		dbo.TinyUrlMapping
	WHERE
		createdTime <= @createdTime

	SET @index = @index + 1
END

