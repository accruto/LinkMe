EXEC sp_changeobjectowner 'linkme_owner.equivalent_terms', dbo
GO

EXEC sp_rename 'dbo.equivalent_terms', 'EquivalentTerms', 'OBJECT'
GO
