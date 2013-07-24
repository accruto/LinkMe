if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetJobAdLastOpenedTime]') and xtype in (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[GetJobAdLastOpenedTime]
GO

CREATE FUNCTION dbo.GetJobAdLastOpenedTime (@jobAdId UNIQUEIDENTIFIER)
RETURNS DATETIME
AS
BEGIN

	DECLARE @time DATETIME

	SELECT
		@time = MAX(s.time)
	FROM
		dbo.JobAdStatus AS s
	WHERE
		s.jobAdId = @jobAdId
		AND s.previousStatus = 1
		AND s.newStatus = 2

	RETURN @time
END
GO

