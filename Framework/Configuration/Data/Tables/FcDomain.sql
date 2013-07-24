-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcDomain]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FcDomain]
GO

-------------------------------------------------------------------------------
-- Create

CREATE TABLE dbo.FcDomain
(
	name VARCHAR(128) NOT NULL,
	description NVARCHAR(512) NOT NULL,
	isDefault BIT NOT NULL
)
GO

ALTER TABLE dbo.FcDomain ADD
CONSTRAINT PK_FcDomain PRIMARY KEY CLUSTERED (name)
GO

