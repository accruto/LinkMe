UPDATE RegisteredUser
SET flags = flags | 4
WHERE loginId IN ('Alan Admin', 'DH Admin', 'Khan Admin', 'Lucas Admin', 'MS Admin',
	'Ray Admin', 'Barnes Admin')
	AND flags & 4 <> 4
GO
