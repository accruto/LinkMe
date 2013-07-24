if exists (select * from dbo.sysobjects where id = object_id(N'linkme_owner.employer_viewed_networkers') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table linkme_owner.employer_viewed_networkers
GO

CREATE TABLE linkme_owner.employer_viewed_networkers
(
	id VARCHAR(50) NOT NULL,
	employerId VARCHAR(50) NOT NULL,
	networkerId VARCHAR(50) NOT NULL
)
GO

ALTER TABLE linkme_owner.employer_viewed_networkers WITH NOCHECK
	ADD CONSTRAINT PK_employer_viewed_networkers PRIMARY KEY CLUSTERED
	(employerId, networkerId)
GO
