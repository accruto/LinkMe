USE [Instrumentation]
GO
/****** Object:  StoredProcedure [dbo].[FiStoreDeleteOldMessages]    Script Date: 05/13/2012 00:44:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--===============================================================================
-- FiStoreDeleteOldMessages.sql
--===============================================================================


-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE [dbo].[FiStoreDeleteOldMessages]
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

