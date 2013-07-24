ALTER TABLE linkme_owner.state_synonym
	DROP CONSTRAINT PK_state_synonym
GO

If exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[state_synonym]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE linkme_owner.state_synonym
GO

CREATE TABLE linkme_owner.state_synonym ( 
	state  	varchar(20) NOT NULL,
	synonym	varchar(50) NOT NULL 
	)
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('ACT', 'ACT')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('ACT', 'AUSTRALIAN CAPITAL TERRITORY')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('NSW', 'NEW SOUTH WALES')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('NSW', 'NSW')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('NT', 'NORTHERN TERRITORY')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('NT', 'NT')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('QLD', 'QLD')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('QLD', 'QUEENSLAND')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('SA', 'SA')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('SA', 'SOUTH AUSTRALIA')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('TAS', 'TAS')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('TAS', 'TASMANIA')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('TAS', 'TASSIE')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('VIC', 'VIC')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('VIC', 'VICTORIA')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('WA', 'WA')
GO
INSERT INTO linkme_owner.state_synonym(state, synonym)
  VALUES('WA', 'WESTERN AUSTRALIA')
GO
ALTER TABLE linkme_owner.state_synonym
	ADD CONSTRAINT PK_state_synonym
	PRIMARY KEY (state, synonym)
GO
