-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreGetMessageIdentifierRangeFilter]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiStoreGetMessageIdentifierRangeFilter]
GO


-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiStoreGetMessageIdentifierRangeFilter
(
	@fromTime BIGINT,
	@eventFilter VARCHAR(1024)
)
AS

SET NOCOUNT ON

SELECT
	m.id AS id, [time]
FROM
	dbo.FiStoreMessage AS m
WHERE
	[time] > @fromTime
	AND [event] LIKE @eventFilter
ORDER BY
	[time], sequence
GO
