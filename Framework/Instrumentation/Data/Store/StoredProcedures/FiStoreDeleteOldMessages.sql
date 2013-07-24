-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreDeleteOldMessages]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiStoreDeleteOldMessages]
GO


-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiStoreDeleteOldMessages
(
	@days AS INT
)
AS

SET NOCOUNT ON

-- Determine the date.

DECLARE @date AS DATETIME
SET @date = DATEADD(dd, 0, DATEDIFF(dd, 0, DATEADD(d, -1 * @days, GETDATE())))

-- Convert it to ticks.

DECLARE @ticks AS BIGINT
SET @ticks = dbo.DateTimeToTicks(@date)

DELETE
    dbo.FiStoreParameter
FROM
    dbo.FiStoreParameter AS P
INNER JOIN
	dbo.FiStoreMessage AS M ON M.id = P.messageId
WHERE
	M.time < @ticks

DELETE
	dbo.FiStoreMessage
WHERE
	time < @ticks

GO
