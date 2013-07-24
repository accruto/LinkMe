IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupEventAttendee') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupEventAttendee;

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupEventCoordinator') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupEventCoordinator;

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupEventInvitation') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupEventInvitation;

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupEvents') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupEvents;

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('GroupEvent') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE GroupEvent;

