IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsResumeEmpty]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[IsResumeEmpty]
GO

CREATE FUNCTION [dbo].[IsResumeEmpty](@id AS UNIQUEIDENTIFIER)
RETURNS INT
AS
BEGIN

	IF EXISTS
	(
		SELECT
			*
		FROM
			dbo.Resume
		WHERE
			id = @id
			AND
			(
				courses IS NOT NULL
				OR awards IS NOT NULL
				OR skills IS NOT NULL
				OR objective IS NOT NULL
				OR summary IS NOT NULL
				OR other IS NOT NULL
				OR citizenship IS NOT NULL
				OR affiliations IS NOT NULL
				OR professional IS NOT NULL
				OR interests IS NOT NULL
				OR referees IS NOT NULL
			)
	)
		RETURN 0

	-- Jobs

	IF EXISTS
	(
		SELECT
			*
		FROM
			dbo.ResumeJob
		WHERE
			resumeId = @id
	)
		RETURN 0

	-- Schools

	IF EXISTS
	(
		SELECT
			*
		FROM
			dbo.ResumeSchool
		WHERE
			resumeId = @id
	)
		RETURN 0
		
	RETURN 1
END
