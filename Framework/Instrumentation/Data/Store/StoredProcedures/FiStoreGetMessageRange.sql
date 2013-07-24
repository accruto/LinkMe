-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreGetMessageRange]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiStoreGetMessageRange]
GO


-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiStoreGetMessageRange
(
	@fromTime BIGINT,
	@afterSequence INT
)
AS

SET NOCOUNT ON

SELECT
	TOP 1000 m.id AS id, [time], sequence, s.fullName AS source, [event], [type], method, [message], exception
FROM
	dbo.FiStoreMessage AS m
INNER JOIN
	dbo.FiStoreSource AS s ON m.sourceId = s.id
WHERE
	[time] > @fromTime
	OR ([time] = @fromTime AND sequence > @afterSequence)
ORDER BY
	[time], sequence
GO
