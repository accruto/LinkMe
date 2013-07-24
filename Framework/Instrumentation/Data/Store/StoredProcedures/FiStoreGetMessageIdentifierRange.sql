-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreGetMessageIdentifierRange]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiStoreGetMessageIdentifierRange]
GO


-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiStoreGetMessageIdentifierRange
(
	@fromTime BIGINT
)
AS

SET NOCOUNT ON

SELECT
	m.id AS id, [time]
FROM
	dbo.FiStoreMessage AS m
WHERE
	[time] > @fromTime
ORDER BY
	[time], sequence
GO
