IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('[GroupIndustry]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [GroupIndustry]
GO

CREATE TABLE [GroupIndustry] ( 
	[groupId] uniqueidentifier NOT NULL,
	[industryId] uniqueidentifier NOT NULL
)
GO

ALTER TABLE [GroupIndustry] ADD CONSTRAINT [PK_GroupIndustry] 
	PRIMARY KEY CLUSTERED ([groupId], [industryId])
GO

ALTER TABLE [GroupIndustry] ADD CONSTRAINT [FK_GroupIndustry_Group] 
	FOREIGN KEY ([groupId]) REFERENCES [Group] ([id])
GO

ALTER TABLE [GroupIndustry] ADD CONSTRAINT [FK_GroupIndustry_Industry] 
	FOREIGN KEY ([industryId]) REFERENCES [Industry] ([id])
GO



