-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreGetDetails]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiStoreGetDetails]
GO


-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiStoreGetDetails
(
	@messageId INT
)
AS

SET NOCOUNT ON

SELECT
	details
FROM
	dbo.FiStoreMessage
WHERE
	id = @messageId

GO
