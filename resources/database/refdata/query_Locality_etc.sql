SELECT *
FROM dbo.Country
ORDER BY [id]
GO

SELECT *
FROM dbo.GeographicalArea
ORDER BY [id]
GO

SELECT *
FROM dbo.Locality
ORDER BY [id]
GO

SELECT *
FROM dbo.CountrySubdivision
ORDER BY [id]
GO

SELECT *
FROM dbo.LocalityCountrySubdivision
ORDER BY localityId, countrySubdivisionId
GO

SELECT *
FROM dbo.Region
ORDER BY [id]
GO

SELECT *
FROM dbo.LocalityRegion
ORDER BY localityId, regionId
GO

SELECT *
FROM dbo.PostalCode
ORDER BY [id]
GO

SELECT *
FROM dbo.PostalSuburb
ORDER BY [id]
GO

SELECT *
FROM dbo.CountrySubdivisionAlias
ORDER BY [id]
GO
