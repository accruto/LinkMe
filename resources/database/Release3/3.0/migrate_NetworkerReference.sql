-- Relationship mappings:
-- 0 (managed) -> 2 (Boss)
-- 1 (senior) -> 3 (Colleague)
-- 4, 5 (worked with) -> 3 (Colleague)
-- 2, 3, 6, 7, 8 (reported to, worked at different companies) -> 0 (Other)

INSERT INTO dbo.NetworkerReference([id], createdTime, relationship, status, [text], subjectId, refereeId)
SELECT dbo.GuidFromString([id]), endorsementDate,
	CASE WHEN relationshipType = 0 THEN 2 WHEN relationshipType IN (1, 4, 5) THEN 3 ELSE 0 END AS relationship,
	CASE confirmed WHEN 1 THEN 2 ELSE 1 END AS status, CAST(ISNULL(endorsementText, '') AS NVARCHAR(2000)),
	dbo.GuidFromString(endorseeId), dbo.GuidFromString(endorserId)
FROM linkme_owner.endorsement
WHERE endorserId IS NOT NULL AND endorseeId IS NOT NULL

GO
