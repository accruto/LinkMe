if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetJobAdFirstOpenedTime]') and xtype in (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[GetJobAdFirstOpenedTime]
GO

CREATE FUNCTION dbo.GetJobAdFirstOpenedTime (@jobAdId UNIQUEIDENTIFIER)
RETURNS DATETIME
AS
BEGIN

	DECLARE @time DATETIME

	SELECT
		@time = MIN(s.time)
	FROM
		dbo.JobAdStatus AS s
	WHERE
		s.jobAdId = @jobAdId
		AND s.previousStatus = 1
		AND s.newStatus = 2

	RETURN @time
END
GO

