if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[networker_promotion]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE linkme_owner.networker_promotion
GO


CREATE TABLE linkme_owner.networker_promotion
(
	id		varchar(50)	NOT NULL,
	promoCode	varchar(50)	NULL,
	networkerId	varchar(50)	NULL,
	networkerMsg	varchar(2000)	NULL,
	dateApplied	datetime	NULL,
	appliedVia	varchar(50)	NULL	
)
GO


ALTER TABLE linkme_owner.networker_promotion WITH NOCHECK 
	 ADD CONSTRAINT PK_networker_promotion PRIMARY KEY CLUSTERED (id) 
GO
