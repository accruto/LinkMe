DECLARE @include_system_tables BIT
SET @include_system_tables = 0

SELECT [Table Name], (SELECT rows FROM sysindexes s WHERE s.indid < 2 AND s.id = OBJECT_ID(a.[Table Name])) AS [Row count], [Total space used (MB)] FROM  
(
SELECT	QUOTENAME(USER_NAME(o.uid)) + '.' + QUOTENAME(OBJECT_NAME(i.id)) AS [Table Name],
	CONVERT(numeric(15,2),(((CONVERT(numeric(15,2),SUM(i.reserved)) * (SELECT low FROM master.dbo.spt_values (NOLOCK) WHERE number = 1 AND type = 'E')) / 1024.)/1024.)) AS [Total space used (MB)]
FROM	sysindexes i (NOLOCK)
		INNER JOIN 
	sysobjects o (NOLOCK) 
		ON 
	i.id = o.id AND 
	((@include_system_tables = 1 AND o.type IN ('U', 'S')) OR o.type = 'U') AND 
	((@include_system_tables = 1)OR (OBJECTPROPERTY(i.id, 'IsMSShipped') = 0))
WHERE	indid IN (0, 1, 255)
GROUP BY	QUOTENAME(USER_NAME(o.uid)) + '.' + QUOTENAME(OBJECT_NAME(i.id))

) as a
ORDER BY [Total space used (MB)] DESC
