-- Convert all suburb names to pascal case.

UPDATE dbo.PostalSuburb
SET displayName = dbo.ToPascalCase(displayName)

-- Fix special cases: MC and DC

UPDATE dbo.PostalSuburb
SET displayName = REPLACE(displayName, ' Mc', ' MC')
WHERE displayName LIKE '% Mc'

UPDATE dbo.PostalSuburb
SET displayName = REPLACE(displayName, ' Dc', ' DC')
WHERE displayName LIKE '% Dc'

-- Fix special cases: McXXXX

UPDATE dbo.PostalSuburb
SET displayName = LEFT(displayName, CHARINDEX('Mc', displayName) + 1)
	+ UPPER(SUBSTRING(displayName, CHARINDEX('Mc', displayName) + 2, 1))
	+ RIGHT(displayName, DATALENGTH(displayName) / 2 - CHARINDEX('Mc', displayName) - 2)
WHERE displayName LIKE 'Mc[a-z]%' OR displayName LIKE '% Mc[a-z]%'

GO
