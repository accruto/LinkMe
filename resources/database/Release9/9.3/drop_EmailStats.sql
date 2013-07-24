IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.RegisterEmailLinkClick') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
DROP PROCEDURE dbo.RegisterEmailLinkClick
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.RegisterEmailSend') AND OBJECTPROPERTY(id, 'IsProcedure') = 1)
DROP PROCEDURE dbo.RegisterEmailSend
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.EmailLinkStats') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.EmailLinkStats
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.EmailSendStats') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.EmailSendStats
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.EmailStats') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.EmailStats
GO


