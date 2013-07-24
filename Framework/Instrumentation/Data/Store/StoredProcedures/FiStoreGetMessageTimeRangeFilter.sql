-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreGetMessageTimeRangeFilter]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiStoreGetMessageTimeRangeFilter]
GO


-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiStoreGetMessageTimeRangeFilter
(
	@fromTime BIGINT,
	@toTime BIGINT,
	@eventFilter VARCHAR(1024)
)
AS

SET NOCOUNT ON

SELECT
	m.id AS id, [time], sequence, s.fullName AS source, [event], [type], method, [message], exception
FROM
	dbo.FiStoreMessage AS m
INNER JOIN
	dbo.FiStoreSource AS s ON m.sourceId = s.id
WHERE
	[time] >= @fromTime AND [time] <= @toTime
	AND [event] LIKE @eventFilter
ORDER BY
	[time], sequence
GO
