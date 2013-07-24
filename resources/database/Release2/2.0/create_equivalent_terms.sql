if exists (select * from dbo.sysobjects where id = object_id(N'linkme_owner.equivalent_terms') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table linkme_owner.equivalent_terms
GO

CREATE TABLE linkme_owner.equivalent_terms
(
	searchTerm VARCHAR(200) NOT NULL,
	equivalentGroupId UNIQUEIDENTIFIER NOT NULL
)
GO

ALTER TABLE linkme_owner.equivalent_terms WITH NOCHECK
	ADD CONSTRAINT PK_equivalent_terms PRIMARY KEY CLUSTERED (searchTerm)
GO
