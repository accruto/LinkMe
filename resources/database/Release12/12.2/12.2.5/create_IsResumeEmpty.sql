IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsResumeEmpty]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[IsResumeEmpty]
GO

CREATE FUNCTION [dbo].[IsResumeEmpty](@id AS UNIQUEIDENTIFIER)
RETURNS INT
WITH SCHEMABINDING
AS
BEGIN

	IF EXISTS
	(
		SELECT
			id
		FROM
			dbo.Resume
		WHERE
			id = @id
			AND
			(
				isEmpty = 1
				OR EXISTS (SELECT resumeId FROM dbo.ResumeJob WHERE resumeId = @id)
				OR EXISTS (SELECT resumeId FROM dbo.ResumeSchool WHERE resumeId = @id)
			)
	)
		RETURN 0

	RETURN 1
END
