IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('[Group]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Group];

CREATE TABLE [Group] ( 
	id uniqueidentifier NOT NULL,
	name nvarchar(100) NOT NULL,
	category GroupCategory NOT NULL,
	description nvarchar(1000) NOT NULL,
	accreditedStatus RequestStatus NOT NULL,
	websiteUrl Url NULL,
	visibility GroupAccess NOT NULL,
	activitiesVisibility GroupAccess NOT NULL,
	flags GroupFlags NOT NULL,
	whoCanJoin GroupJoinRestriction NOT NULL,
	parentGroupId uniqueidentifier NULL,
	imageFileReferenceId uniqueidentifier NULL,
	smallImageFileReferenceId uniqueidentifier NULL,
	rootGroupId uniqueidentifier NULL,
	locationReferenceId uniqueidentifier NULL
);

ALTER TABLE [Group] ADD CONSTRAINT PK_Group 
	PRIMARY KEY CLUSTERED (id);

ALTER TABLE [Group] ADD CONSTRAINT FK_Group_LocationReference 
	FOREIGN KEY (locationReferenceId) REFERENCES LocationReference (id);

ALTER TABLE [Group] ADD CONSTRAINT FK_Group_ImageFile 
	FOREIGN KEY (imageFileReferenceId) REFERENCES FileReference (id);

ALTER TABLE [Group] ADD CONSTRAINT FK_Group_SmallImageFile 
	FOREIGN KEY (smallImageFileReferenceId) REFERENCES FileReference (id);
















