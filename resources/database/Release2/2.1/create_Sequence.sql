if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sequence]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Sequence]
GO

CREATE TABLE dbo.[Sequence]
(
	[Name] VARCHAR(20) NOT NULL,
	NextNumber INT NOT NULL
)
GO

ALTER TABLE dbo.[Sequence]
ADD CONSTRAINT PK_Sequence PRIMARY KEY CLUSTERED ([Name])
GO
