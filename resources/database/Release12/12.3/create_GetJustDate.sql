IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetJustDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetJustDate]
GO

CREATE FUNCTION [dbo].[GetJustDate] (@dt DATETIME)
RETURNS DATETIME
AS
BEGIN
	RETURN CONVERT(DATETIME, FLOOR(CONVERT(FLOAT, @dt)))
END
