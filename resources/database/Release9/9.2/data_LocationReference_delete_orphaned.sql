-- Disable triggers and drop foreign keys

ALTER TABLE dbo.LocationReference
DISABLE TRIGGER ALL

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_LocationReference]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_LocationReference]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Group_LocationReference]') AND parent_object_id = OBJECT_ID(N'[dbo].[Group]'))
ALTER TABLE [dbo].[Group] DROP CONSTRAINT [FK_Group_LocationReference]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAdLocation_LocationReference]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAdLocation]'))
ALTER TABLE [dbo].[JobAdLocation] DROP CONSTRAINT [FK_JobAdLocation_LocationReference]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RelocationLocation_GeographicalArea]') AND parent_object_id = OBJECT_ID(N'[dbo].[RelocationLocation]'))
ALTER TABLE [dbo].[RelocationLocation] DROP CONSTRAINT [FK_RelocationLocation_GeographicalArea]

GO

-- Copy the rows to keep into a temp table, truncate, and copy back

IF object_id('tempdb..#NewLocationReference') IS NOT NULL
DROP TABLE #NewLocationReference

SELECT *
INTO #NewLocationReference
FROM dbo.LocationReference lr
WHERE EXISTS
(
	SELECT id FROM dbo.Address a
	WHERE lr.id = a.locationReferenceId
	UNION
	SELECT id FROM dbo.[Group] g
	WHERE lr.id = g.locationReferenceId
	UNION
	SELECT jobAdId FROM  dbo.JobAdLocation jal
	WHERE lr.id = jal.locationReferenceId
	UNION
	SELECT id FROM  dbo.RelocationLocation rl
	WHERE lr.id = rl.locationReferenceId
)

TRUNCATE TABLE dbo.LocationReference

INSERT INTO dbo.LocationReference
SELECT *
FROM #NewLocationReference

DROP TABLE #NewLocationReference

GO

-- Re-enable triggers and re-create foreign keys

ALTER TABLE dbo.LocationReference
ENABLE TRIGGER ALL

ALTER TABLE dbo.Address
ADD CONSTRAINT FK_Address_LocationReference
FOREIGN KEY (locationReferenceId) REFERENCES dbo.LocationReference (id)

ALTER TABLE dbo.[Group]
ADD CONSTRAINT FK_Group_LocationReference
FOREIGN KEY (locationReferenceId) REFERENCES dbo.LocationReference (id)

ALTER TABLE dbo.JobAdLocation
ADD CONSTRAINT FK_JobAdLocation_LocationReference
FOREIGN KEY (locationReferenceId) REFERENCES dbo.LocationReference (id)

ALTER TABLE dbo.RelocationLocation
ADD CONSTRAINT FK_RelocationLocation_GeographicalArea
FOREIGN KEY (locationReferenceId) REFERENCES dbo.LocationReference (id)

GO

ALTER TABLE dbo.Address
CHECK CONSTRAINT FK_Address_LocationReference

ALTER TABLE dbo.[Group]
CHECK CONSTRAINT FK_Group_LocationReference

ALTER TABLE dbo.JobAdLocation
CHECK CONSTRAINT FK_JobAdLocation_LocationReference

ALTER TABLE dbo.RelocationLocation
CHECK CONSTRAINT FK_RelocationLocation_GeographicalArea

GO
