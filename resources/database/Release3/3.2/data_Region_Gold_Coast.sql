DECLARE @regionId INT
SET @regionId = 2816

INSERT INTO GeographicalArea([id], displayName)
VALUES (@regionId, 'Gold Coast')

INSERT INTO Region([id], urlName, isMajorCity)
VALUES (@regionId, 'gold-coast', 0)

INSERT INTO dbo.LocalityRegion(regionId, localityId)
SELECT DISTINCT @regionId, localityId
FROM PostalCode
WHERE postcode IN ('4210', '4211', '4212', '4213', '4214', '4215', '4216', '4217', '4218', '4219', '4220', '4221', '4223', '4224', '4225', '4226', '4227', '4228', '4229', '4230', '4270', '4271', '4272', '4275', '4285', '4287', '9726')

GO
