IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'endorsement'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	CREATE TABLE linkme_owner.endorsement (
		id VARCHAR(50) NOT NULL,
		endorserId VARCHAR(50) NULL,
		endorseeId VARCHAR(50) NULL,
		endorsementText TEXT NULL,
		relationshipType SMALLINT NULL,
		company VARCHAR(100) NULL,
		confirmed BIT NULL,
	
		CONSTRAINT pk_endorsement PRIMARY KEY (id)	
	)

	CREATE INDEX i_endorsement_endorserId ON linkme_owner.endorsement (endorserId)
	CREATE INDEX i_endorsement_endorseeId ON linkme_owner.endorsement (endorseeId)
END
GO	

IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'endorsement_relationship'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	CREATE TABLE linkme_owner.endorsement_relationship (
		id SMALLINT NOT NULL,
		selectionText VARCHAR(255) NULL,
		display_text VARCHAR(255) NULL,
		
		CONSTRAINT pk_endorsement_relationship PRIMARY KEY (id)	
	)

	insert into linkme_owner.endorsement_relationship VALUES (0, "You managed %%ENDORSEE_FIRSTNAME%% directly", "%%ENDORSER_FULLNAME%% managed %%ENDORSEE_FIRSTNAME%% directly at %%COMPANY%%")
	insert into linkme_owner.endorsement_relationship VALUES (1, "You were senior to %%ENDORSEE_FIRSTNAME%%, but did not manage directly", "%%ENDORSER_FULLNAME%% was senior to %%ENDORSEE_FIRSTNAME%%, but did not manage directly at %%COMPANY%%")
	insert into linkme_owner.endorsement_relationship VALUES (2, "You reported directly to %%ENDORSEE_FIRSTNAME%%", "%%ENDORSER_FULLNAME%% reported directly to %%ENDORSEE_FIRSTNAME%% at %%COMPANY%%")
	insert into linkme_owner.endorsement_relationship VALUES (3, "%%ENDORSEE_FIRSTNAME%% was senior to you, but you did not report directly", "%%ENDORSEE_FIRSTNAME%% was senior to %%ENDORSER_FULLNAME%%, but %%ENDORSER_FULLNAME%% did not report directly at %%COMPANY%%")
	insert into linkme_owner.endorsement_relationship VALUES (4, "You worked with %%ENDORSEE_FIRSTNAME%% in the same group", "%%ENDORSER_FULLNAME%% worked with %%ENDORSEE_FIRSTNAME%% in the same group at %%COMPANY%%")
	insert into linkme_owner.endorsement_relationship VALUES (5, "You worked with %%ENDORSEE_FIRSTNAME%% in different groups", "%%ENDORSER_FULLNAME%% worked with %%ENDORSEE_FIRSTNAME%% in different groups at %%COMPANY%%")
	insert into linkme_owner.endorsement_relationship VALUES (6, "You worked with %%ENDORSEE_FIRSTNAME%% but were at different companies", "%%ENDORSER_FULLNAME%% worked with %%ENDORSEE_FIRSTNAME%% but were at different companies")
	insert into linkme_owner.endorsement_relationship VALUES (7, "You were a client of %%ENDORSEE_FIRSTNAME%%'s", "%%ENDORSER_FULLNAME%% was a client of %%ENDORSEE_FIRSTNAME%%'s at %%COMPANY%%")
	insert into linkme_owner.endorsement_relationship VALUES (8, "%%ENDORSEE_FIRSTNAME%% was a client of yours", "%%ENDORSEE_FIRSTNAME%% at %%COMPANY%% was a client of %%ENDORSER_FULLNAME%%'s")
END
GO



