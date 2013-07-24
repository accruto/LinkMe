-- For countries other than Australia set the only subdivision name to be NULL (the "default" subdivision).
-- Set the ID to be the same as the country ID. This is just for simplicity of migration - it is NOT a
-- convention and the same will not hold true for newly added countries.

-- Add a NULL ("unknown") state even for Australia, because current data has many users
-- with an unknown or invalid state, but we still want to store the fact that they are in Australia.
-- Ideally we'd like to clean up this up later and remove this hack (the NULL Australian state).

INSERT INTO dbo.GeographicalArea([id], displayName)
SELECT [id], NULL
FROM dbo.Country

INSERT INTO dbo.CountrySubdivision([id], circleRadiusKm, circleCentreId, countryId)
SELECT [id], 0, NULL, [id]
FROM dbo.Country

GO
