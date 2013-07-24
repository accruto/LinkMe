
CREATE PROCEDURE CreateRelativeLocation (@id INT, @countryId TINYINT, @displayName NVARCHAR(100), @prefix BIT, @suffix BIT)
AS
BEGIN

IF NOT EXISTS (SELECT * FROM RelativeLocation WHERE displayName = @displayName)
	INSERT RelativeLocation (id, countryId, displayName, prefix, suffix) VALUES (@id, @countryId, @displayName, @prefix, @suffix)
ELSE
	UPDATE RelativeLocation SET prefix = @prefix, suffix = @suffix WHERE countryId = @countryId AND displayName = @displayName

END
GO

EXEC CreateRelativeLocation 1, 1, 'North', 1, 1
EXEC CreateRelativeLocation 2, 1, 'South', 1, 1
EXEC CreateRelativeLocation 3, 1, 'East', 1, 1
EXEC CreateRelativeLocation 4, 1, 'West', 1, 1

DROP PROCEDURE CreateRelativeLocation
GO