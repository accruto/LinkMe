If exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[locality]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE linkme_owner.locality
GO

CREATE TABLE linkme_owner.locality
( 
	locality	varchar(50) NOT NULL,
	postCode	varchar(20) NOT NULL,
	state   	varchar(10) NOT NULL,
	lat		float,
	lon		float 
)
GO

-- The primary key needs to include the state, because there is one locality (URIARRA, 2611) that is in
-- two states (ACT and NSW)!

ALTER TABLE linkme_owner.locality WITH NOCHECK
	ADD CONSTRAINT PK_locality PRIMARY KEY CLUSTERED (locality, postCode, state)
GO
