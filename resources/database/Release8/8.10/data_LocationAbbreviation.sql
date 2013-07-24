
CREATE PROCEDURE CreateLocationAbbreviation (@id INT, @countryId TINYINT, @abbreviation NVARCHAR(100), @displayName NVARCHAR(100), @prefix BIT, @suffix BIT)
AS
BEGIN

IF NOT EXISTS (SELECT * FROM LocationAbbreviation WHERE abbreviation = @abbreviation)
	INSERT LocationAbbreviation (id, countryId, abbreviation, displayName, prefix, suffix) VALUES (@id, @countryId, @abbreviation, @displayName, @prefix, @suffix)
ELSE
	UPDATE LocationAbbreviation SET displayName = @displayName, prefix = @prefix, suffix = @suffix WHERE countryId = @countryId AND abbreviation = @abbreviation

END
GO

EXEC CreateLocationAbbreviation 1, 1, 'St', 'Saint', 1, 0
EXEC CreateLocationAbbreviation 2, 1, 'Pt', 'Port', 1, 0
EXEC CreateLocationAbbreviation 3, 1, 'Nth', 'North', 1, 1
EXEC CreateLocationAbbreviation 4, 1, 'N', 'North', 1, 1
EXEC CreateLocationAbbreviation 5, 1, 'Sth', 'South', 1, 1
EXEC CreateLocationAbbreviation 6, 1, 'S', 'South', 1, 1
EXEC CreateLocationAbbreviation 7, 1, 'E', 'East', 1, 1
EXEC CreateLocationAbbreviation 8, 1, 'W', 'West', 1, 1

DROP PROCEDURE CreateLocationAbbreviation
GO

