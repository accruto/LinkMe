-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreGetMessage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiStoreGetMessage]
GO


-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiStoreGetMessage
(
	@messageId INT
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
	m.id = @messageId

GO
