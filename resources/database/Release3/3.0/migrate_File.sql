-- Split the File table into FileData and FileReference. The hash and sizeBytes should all be the same for
-- a given data ID, but the extension may be different (see defect 1748). Just pick any of them for the moment -
-- they have to be fixed later, because the database script has no way of knowing which is correct.

INSERT INTO dbo.FileData([id], context, extension, [hash], sizeBytes)
SELECT dataId, context, extension, [hash], sizeBytes
FROM linkme_owner.[File] AS f
INNER JOIN
(
	SELECT
	(
	        SELECT TOP 1 [id]
	        FROM linkme_owner.[File] AS f2
	        WHERE f2.dataId = f1.dataId
	) AS [id]
	FROM linkme_owner.[File] AS f1
	GROUP BY dataId, sizeBytes, [hash]
) AS lf
ON f.[id] = lf.[id]


INSERT INTO dbo.FileReference([id], createdTime, mimeType, [name], dataId)
SELECT [id], GETDATE(), mimeType, [name], dataId
FROM linkme_owner.[File]

GO
