-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiMessageHandlersGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiMessageHandlersGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiMessageHandlersGet
(
	@parentFullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

IF @parentFullName IS NULL OR @parentFullName = ''
BEGIN
	SELECT fullName, messageHandlerType, configurationData
	FROM dbo.FiMessageHandler
	WHERE parentId IS NULL
END
ELSE
BEGIN
	SELECT Child.fullName, Child.messageHandlerType, Child.configurationData
	FROM dbo.FiMessageHandler AS Child
	INNER JOIN dbo.FiMessageHandler AS Parent
	ON Child.parentId = Parent.id
	WHERE Parent.fullName = @parentFullName
END

RETURN 0

END
GO
