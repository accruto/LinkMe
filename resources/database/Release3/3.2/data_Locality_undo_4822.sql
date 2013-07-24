-- Case 5029 - undo the change for 4822 to (try to) remove postcode 6992.

INSERT INTO dbo.PostalCode ([id], postcode, localityId)
VALUES (2743, '6991', 2684)
INSERT INTO dbo.PostalCode ([id], postcode, localityId)
VALUES (2745, '6997', 2684)

GO

INSERT INTO dbo.PostalSuburb([id], displayName, postcodeId, countrySubdivisionId)
VALUES (9519, 'Kelmscott', 2743, 19)
INSERT INTO dbo.PostalSuburb([id], displayName, postcodeId, countrySubdivisionId)
VALUES (9521, 'Kelmscott DC', 2745, 19)

GO
