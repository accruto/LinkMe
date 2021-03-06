IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProfessionalVisibilityDisplayText]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetProfessionalVisibilityDisplayText]
GO

CREATE FUNCTION [dbo].[GetProfessionalVisibilityDisplayText] (@visibility TINYINT)
RETURNS NVARCHAR(1024)
AS
BEGIN

	DECLARE @text NVARCHAR(1024)
	SET @text = ''

	IF ((@visibility & 1) = 1)
		SET @text = 'Resume'

	IF ((@visibility & 2) = 2)
	BEGIN
		IF (LEN(@text) = 0)
			SET @text = 'Name'
		ELSE
			SET @text = @text + ' | ' + 'Name'
	END

	IF ((@visibility & 4) = 4)
	BEGIN
		IF (LEN(@text) = 0)
			SET @text = 'PhoneNumbers'
		ELSE
			SET @text = @text + ' | ' + 'PhoneNumbers'
	END

	IF ((@visibility & 8) = 8)
	BEGIN
		IF (LEN(@text) = 0)
			SET @text = 'ProfilePhoto'
		ELSE
			SET @text = @text + ' | ' + 'ProfilePhoto'
	END

	IF ((@visibility & 16) = 16)
	BEGIN
		IF (LEN(@text) = 0)
			SET @text = 'RecentEmployers'
		ELSE
			SET @text = @text + ' | ' + 'RecentEmployers'
	END

	IF ((@visibility & 32) = 32)
	BEGIN
		IF (LEN(@text) = 0)
			SET @text = 'Communities'
		ELSE
			SET @text = @text + ' | ' + 'Communities'
	END

	IF ((@visibility & 64) = 64)
	BEGIN
		IF (LEN(@text) = 0)
			SET @text = 'Referees'
		ELSE
			SET @text = @text + ' | ' + 'Referees'
	END

	IF ((@visibility & 128) = 128)
	BEGIN
		IF (LEN(@text) = 0)
			SET @text = 'Salary'
		ELSE
			SET @text = @text + ' | ' + 'Salary'
	END

	RETURN @text
END