-- Create an industry 'None'
DECLARE @indId UNIQUEIDENTIFIER
SET @indId = 'D7B74C5D-E94C-4afd-A29B-EACAD62CBB7D'

-- Delete the Industry if it exists
DELETE FROM Industry
WHERE id = @indId

INSERT INTO Industry
SELECT @indId AS id, 
'Not specified' AS displayName, NULL AS keywordExpression,
'None' AS shortDisplayName, 'none' AS urlName