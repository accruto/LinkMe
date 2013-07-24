if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeleteOldViewState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeleteOldViewState]
GO

CREATE PROCEDURE dbo.DeleteOldViewState
AS
BEGIN
	-- Delete viewstate more than a day old.

	DELETE dbo.linkme_viewstate
	WHERE UpdatedDate < DATEADD(Day, -1, GETDATE())
END
GO
