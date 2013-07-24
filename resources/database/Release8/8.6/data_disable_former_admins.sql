UPDATE RegisteredUser
SET flags = flags | 4
WHERE loginId IN ('Andrew Admin', 'Anna Admin', 'dot admin', 'Lee Admin', 'Lucy Admin', 'Michelle Admin')
GO
