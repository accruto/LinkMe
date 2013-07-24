DECLARE csrCompaniesToFix CURSOR FOR
SELECT c.*
FROM dbo.Company c
JOIN
(
	SELECT [name]
	FROM dbo.Company
	WHERE verifiedById IS NOT NULL
	GROUP BY [name]
	HAVING COUNT(*) > 1
) dup
ON c.[name] = dup.[name]
WHERE c.verifiedById IS NOT NULL
ORDER BY c.[name], c.[id]

DECLARE @id UNIQUEIDENTIFIER
DECLARE @name CompanyName
DECLARE @verifiedById UNIQUEIDENTIFIER

DECLARE @setToId UNIQUEIDENTIFIER
DECLARE @setToName CompanyName

OPEN csrCompaniesToFix

FETCH NEXT FROM csrCompaniesToFix
INTO @id, @name, @verifiedById

WHILE (@@FETCH_STATUS = 0)
BEGIN
	IF (@name = @setToName)
	BEGIN
		PRINT 'Replacing company ID ' + CAST(@id AS VARCHAR(40)) + ' with ' + CAST(@setToId AS VARCHAR(40))

		UPDATE dbo.Employer
		SET companyId = @setToId
		WHERE companyId = @id

		DELETE dbo.Company
		WHERE [id] = @id
	END
	ELSE
	BEGIN
		-- Found the next company - set subsequent companies with the same name to this

		PRINT 'Processing company ' + @name + ' (ID: ' + CAST(@id AS VARCHAR(40)) + ')'

		SET @setToName = @name
		SET @setToId = @id
	END

	FETCH NEXT FROM csrCompaniesToFix
	INTO @id, @name, @verifiedById
END

CLOSE csrCompaniesToFix
DEALLOCATE csrCompaniesToFix
