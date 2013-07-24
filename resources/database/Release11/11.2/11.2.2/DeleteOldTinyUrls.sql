-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeleteOldTinyUrls]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeleteOldTinyUrls]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.DeleteOldTinyUrls
(
	@days AS INT,
	@batch AS INT
)
AS

SET NOCOUNT ON

DECLARE @count INT

-- Determine the date.

DECLARE @date AS DATETIME
SET @date = DATEADD(dd, 0, DATEDIFF(dd, 0, DATEADD(d, -1 * @days, GETDATE())))

-- Place in temporary table.

SELECT
	TOP (@batch) tinyId
INTO
	#TinyUrlMappingBatch
FROM
	dbo.TinyUrlMapping
WHERE
	createdTime < @date

-- Delete them now.

DELETE
	dbo.TinyUrlMapping
FROM
	dbo.TinyUrlMapping AS T
INNER JOIN
	#TinyUrlMappingBatch AS B ON B.tinyId = T.tinyId

SET @count = @@ROWCOUNT

DROP TABLE #TinyUrlMappingBatch

RETURN @count

GO
