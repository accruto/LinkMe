IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ResumesViewedBySearcher]'))
DROP VIEW [dbo].[ResumesViewedBySearcher]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.UserEventActionedGroup') AND type in (N'U'))
DROP TABLE dbo.UserEventActionedGroup
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.UserEventActionedJobAd') AND type in (N'U'))
DROP TABLE dbo.UserEventActionedJobAd
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.UserEventActionedResume') AND type in (N'U'))
DROP TABLE dbo.UserEventActionedResume
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.UserEventActionedUser') AND type in (N'U'))
DROP TABLE dbo.UserEventActionedUser
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.UserEventExtraData') AND type in (N'U'))
DROP TABLE dbo.UserEventExtraData
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.UserEvent') AND type in (N'U'))
DROP TABLE dbo.UserEvent
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.InstanceState') AND type in (N'U'))
DROP TABLE dbo.InstanceState
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.UserMessageAttachment') AND type in (N'U'))
DROP TABLE dbo.UserMessageAttachment
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.UserMessage') AND type in (N'U'))
DROP TABLE dbo.UserMessage
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.NetworkerEventDelta') AND type in (N'U'))
DROP TABLE dbo.NetworkerEventDelta
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.NetworkerEvent') AND type in (N'U'))
DROP TABLE dbo.NetworkerEvent
