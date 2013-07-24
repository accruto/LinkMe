-- Insert a CachedMemberData row with the contact count for members that have a network.

INSERT INTO dbo.CachedMemberData(memberId, contactCount, currentJobs, currentJobTitles, interests)
SELECT un.[id], COUNT(*), NULL, NULL, NULL
FROM dbo.Networker un
JOIN dbo.NetworkLink nl
ON un.[id] = nl.fromNetworkerId
INNER JOIN dbo.RegisteredUser ru
ON nl.toNetworkerId = ru.[id]
WHERE (ru.flags & 4) = 0
GROUP BY un.[id]

GO

-- Insert a CachedMemberData row for every other member.

INSERT INTO dbo.CachedMemberData(memberId, contactCount, currentJobs, currentJobTitles, interests)
SELECT m.[id], 0, NULL, NULL, NULL
FROM dbo.Member m
WHERE NOT EXISTS (SELECT * FROM dbo.CachedMemberData cmd WHERE cmd.[memberId] = m.[id])

GO
