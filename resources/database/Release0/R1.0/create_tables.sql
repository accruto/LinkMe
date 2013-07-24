CREATE TABLE linkme_owner.user_profile (
	id VARCHAR(50),
	userId VARCHAR(150) NULL,
	password VARCHAR(50) NULL,
	firstName VARCHAR(150) NULL,
	lastName VARCHAR(150) NULL,
	active BIT NOT NULL,

	CONSTRAINT pk_user_profile PRIMARY KEY (id)
)

CREATE INDEX i_user_profile_userId ON linkme_owner.user_profile (userId)

CREATE TABLE linkme_owner.networker_profile (
	id VARCHAR(50),
	resumeId VARCHAR(150) NULL,
	resumeUpdatedRemindedDate DATETIME NULL,
	contactsCount INTEGER NULL,
	networkerMatches INTEGER NULL,
	employerMatches INTEGER NULL,
	employerMisses INTEGER NULL,
	matchEmployerSearches BIT NOT NULL,
	matchNetworkerSearches BIT NOT NULL,
	postcode VARCHAR(10) NULL,
	matchOtherStatesSearches BIT NULL,

	CONSTRAINT pk_networker_profile PRIMARY KEY (id)
)

CREATE TABLE linkme_owner.employer_profile (
	id VARCHAR(50),
	organisationName VARCHAR(255) NULL,
	contactPhoneNumber VARCHAR(150) NULL,
	credits INTEGER NULL,
	emailAddress VARCHAR(255) NULL,
	subRole VARCHAR(255) NULL,

	CONSTRAINT pk_employer_profile PRIMARY KEY (id)
)

CREATE TABLE linkme_owner.user_profile_contact (
	id VARCHAR(50) NOT NULL,
	contactId VARCHAR(50) NOT NULL,

	CONSTRAINT pk_user_profile_contact PRIMARY KEY (id, contactId)
)

CREATE TABLE linkme_owner.event (
	id VARCHAR(50) NOT NULL,
	timestamp DATETIME NOT NULL,
	type TINYINT NOT NULL,
	ownerId VARCHAR(50) NULL,
	data VARCHAR(255) NULL,

	CONSTRAINT pk_event PRIMARY KEY (id)
)

CREATE INDEX i_event_ownerId ON linkme_owner.event (ownerId)

CREATE TABLE linkme_owner.user_data (
	id VARCHAR(50) NOT NULL,
	value VARCHAR(7000) NULL,
	lastModifiedDate DATETIME NULL,

	CONSTRAINT pk_user_data PRIMARY KEY (id)
)
