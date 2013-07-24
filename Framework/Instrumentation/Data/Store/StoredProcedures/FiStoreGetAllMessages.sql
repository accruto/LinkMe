-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreGetAllMessages]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiStoreGetAllMessages]
GO


-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiStoreGetAllMessages
AS

SET NOCOUNT ON

SELECT
	m.id AS id, [time], sequence, s.fullName AS source, [event], [type], method, [message], exception
FROM
	dbo.FiStoreMessage AS m
INNER JOIN
	dbo.FiStoreSource AS s ON m.sourceId = s.id
ORDER BY
	[time], sequence
GO
