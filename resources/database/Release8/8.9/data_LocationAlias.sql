
CREATE PROCEDURE CreateLocationAlias (@id INT, @countryId TINYINT, @alias NVARCHAR(100), @displayName NVARCHAR(100))
AS
BEGIN

IF NOT EXISTS (SELECT * FROM LocationAlias WHERE alias = @alias)
	INSERT LocationAlias (id, countryId, alias, displayName) VALUES (@id, @countryId, @alias, @displayName)
ELSE
	UPDATE LocationAlias SET displayName = @displayName WHERE countryId = @countryId AND alias = @alias

END
GO

EXEC CreateLocationAlias 1, 1, 'St', 'Saint'
EXEC CreateLocationAlias 2, 1, 'Pt', 'Port'
EXEC CreateLocationAlias 3, 1, 'Nth', 'North'
EXEC CreateLocationAlias 4, 1, 'N', 'North'
EXEC CreateLocationAlias 5, 1, 'Sth', 'South'
EXEC CreateLocationAlias 6, 1, 'S', 'South'
EXEC CreateLocationAlias 7, 1, 'E', 'East'
EXEC CreateLocationAlias 8, 1, 'W', 'West'

DROP PROCEDURE CreateLocationAlias
GO