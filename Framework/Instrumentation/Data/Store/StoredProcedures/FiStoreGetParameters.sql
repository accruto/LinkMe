-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreGetParameters]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiStoreGetParameters]
GO


-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiStoreGetParameters
(
	@messageId INT
)
AS

SET NOCOUNT ON

SELECT
	p.name, p.format, t.fullName AS type, p.string, p.binary
FROM
	dbo.FiStoreParameter AS p
LEFT OUTER JOIN
	dbo.FiStoreType AS t ON p.typeId = t.id
WHERE
	messageId = @messageId
ORDER BY
	sequence

RETURN 0
GO
