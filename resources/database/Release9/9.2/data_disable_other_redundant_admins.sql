UPDATE dbo.RegisteredUser
SET flags = flags | 4
WHERE loginId IN ('Penny Admin', 'Stuart Admin', 'Jamie Admin', 'av admin', 'stephanie admin',
	'jimena admin', 'andrea admin', 'Marcelo Admin', 'PT Admin') AND flags & 4 <> 4
GO
