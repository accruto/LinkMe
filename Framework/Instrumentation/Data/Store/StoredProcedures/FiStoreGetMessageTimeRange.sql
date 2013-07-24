-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreGetMessageTimeRange]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiStoreGetMessageTimeRange]
GO


-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiStoreGetMessageTimeRange
(
	@fromTime BIGINT,
	@toTime BIGINT
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
ORDER BY
	[time], sequence
GO
