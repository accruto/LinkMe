if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Log]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Log]
GO

CREATE TABLE [dbo].[Log] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Date] [datetime] NOT NULL ,
	[Thread] [varchar] (32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Context] [varchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Level] [varchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Logger] [varchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Message] [varchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Exception] [varchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

