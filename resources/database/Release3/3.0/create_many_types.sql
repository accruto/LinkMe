if exists (select * from dbo.systypes where name = N'AddressLine')
exec sp_droptype N'AddressLine'
GO

if exists (select * from dbo.systypes where name = N'CandidateStatus')
exec sp_droptype N'CandidateStatus'
GO

if exists (select * from dbo.systypes where name = N'CompanyName')
exec sp_droptype N'CompanyName'
GO

if exists (select * from dbo.systypes where name = N'EmailAddress')
exec sp_droptype N'EmailAddress'
GO

if exists (select * from dbo.systypes where name = N'EmployerAccess')
exec sp_droptype N'EmployerAccess'
GO

if exists (select * from dbo.systypes where name = N'EmployerSubRole')
exec sp_droptype N'EmployerSubRole'
GO

if exists (select * from dbo.systypes where name = N'FileContext')
exec sp_droptype N'FileContext'
GO

if exists (select * from dbo.systypes where name = N'Filename')
exec sp_droptype N'Filename'
GO

if exists (select * from dbo.systypes where name = N'Gender')
exec sp_droptype N'Gender'
GO

if exists (select * from dbo.systypes where name = N'GeographicCoordinate')
exec sp_droptype N'GeographicCoordinate'
GO

if exists (select * from dbo.systypes where name = N'IntegratorPermissions')
exec sp_droptype N'IntegratorPermissions'
GO

if exists (select * from dbo.systypes where name = N'JobAdStatus')
exec sp_droptype N'JobAdStatus'
GO

if exists (select * from dbo.systypes where name = N'JobApplicationStatus')
exec sp_droptype N'JobApplicationStatus'
GO

if exists (select * from dbo.systypes where name = N'JobPosterFlags')
exec sp_droptype N'JobPosterFlags'
GO

if exists (select * from dbo.systypes where name = N'JobTitle')
exec sp_droptype N'JobTitle'
GO

if exists (select * from dbo.systypes where name = N'JobTypes')
exec sp_droptype N'JobTypes'
GO

if exists (select * from dbo.systypes where name = N'LocationDisplayName')
exec sp_droptype N'LocationDisplayName'
GO

if exists (select * from dbo.systypes where name = N'LoginId')
exec sp_droptype N'LoginId'
GO

if exists (select * from dbo.systypes where name = N'MemberAccess')
exec sp_droptype N'MemberAccess'
GO

if exists (select * from dbo.systypes where name = N'MessageSubject')
exec sp_droptype N'MessageSubject'
GO

if exists (select * from dbo.systypes where name = N'MessageText')
exec sp_droptype N'MessageText'
GO

if exists (select * from dbo.systypes where name = N'NetworkerEventType')
exec sp_droptype N'NetworkerEventType'
GO

if exists (select * from dbo.systypes where name = N'NonMemberFlags')
exec sp_droptype N'NonMemberFlags'
GO

if exists (select * from dbo.systypes where name = N'PasswordHash')
exec sp_droptype N'PasswordHash'
GO

if exists (select * from dbo.systypes where name = N'PersonName')
exec sp_droptype N'PersonName'
GO

if exists (select * from dbo.systypes where name = N'PhoneNumber')
exec sp_droptype N'PhoneNumber'
GO

if exists (select * from dbo.systypes where name = N'Postcode')
exec sp_droptype N'Postcode'
GO

if exists (select * from dbo.systypes where name = N'RefereeRelationship')
exec sp_droptype N'RefereeRelationship'
GO

if exists (select * from dbo.systypes where name = N'RequestFlags')
exec sp_droptype N'RequestFlags'
GO

if exists (select * from dbo.systypes where name = N'RequestStatus')
exec sp_droptype N'RequestStatus'
GO

if exists (select * from dbo.systypes where name = N'Salary')
exec sp_droptype N'Salary'
GO

if exists (select * from dbo.systypes where name = N'SalaryRateType')
exec sp_droptype N'SalaryRateType'
GO

if exists (select * from dbo.systypes where name = N'ThreadParticipantFlags')
exec sp_droptype N'ThreadParticipantFlags'
GO

if exists (select * from dbo.systypes where name = N'Url')
exec sp_droptype N'Url'
GO

if exists (select * from dbo.systypes where name = N'UserEventContext')
exec sp_droptype N'UserEventContext'
GO

if exists (select * from dbo.systypes where name = N'UserEventType')
exec sp_droptype N'UserEventType'
GO

if exists (select * from dbo.systypes where name = N'UserFlags')
exec sp_droptype N'UserFlags'
GO

EXEC sp_addtype N'AddressLine', N'nvarchar (100)', N'not null'
GO

EXEC sp_addtype N'CandidateStatus', N'tinyint', N'not null'
GO

EXEC sp_addtype N'CompanyName', N'nvarchar (100)', N'not null'
GO

EXEC sp_addtype N'EmailAddress', N'nvarchar (320)', N'not null'
GO

EXEC sp_addtype N'EmployerAccess', N'tinyint', N'not null'
GO

EXEC sp_addtype N'EmployerSubRole', N'tinyint', N'not null'
GO

EXEC sp_addtype N'FileContext', N'tinyint', N'not null'
GO

EXEC sp_addtype N'Filename', N'nvarchar (260)', N'not null'
GO

EXEC sp_addtype N'Gender', N'tinyint', N'not null'
GO

EXEC sp_addtype N'GeographicCoordinate', N'real', N'not null'
GO

EXEC sp_addtype N'IntegratorPermissions', N'smallint', N'not null'
GO

EXEC sp_addtype N'JobAdStatus', N'tinyint', N'not null'
GO

EXEC sp_addtype N'JobApplicationStatus', N'tinyint', N'not null'
GO

EXEC sp_addtype N'JobPosterFlags', N'tinyint', N'not null'
GO

EXEC sp_addtype N'JobTitle', N'nvarchar (100)', N'not null'
GO

EXEC sp_addtype N'JobTypes', N'tinyint', N'not null'
GO

EXEC sp_addtype N'LocationDisplayName', N'nvarchar (100)', N'not null'
GO

EXEC sp_addtype N'LoginId', N'varchar (30)', N'not null'
GO

EXEC sp_addtype N'MemberAccess', N'int', N'not null'
GO

EXEC sp_addtype N'MessageSubject', N'nvarchar (100)', N'not null'
GO

EXEC sp_addtype N'MessageText', N'nvarchar (2000)', N'not null'
GO

EXEC sp_addtype N'NetworkerEventType', N'tinyint', N'not null'
GO

EXEC sp_addtype N'NonMemberFlags', N'tinyint', N'not null'
GO

EXEC sp_addtype N'PasswordHash', N'char (24)', N'not null'
GO

EXEC sp_addtype N'PersonName', N'nvarchar (30)', N'not null'
GO

EXEC sp_addtype N'PhoneNumber', N'varchar (20)', N'not null'
GO

EXEC sp_addtype N'Postcode', N'varchar (8)', N'not null'
GO

EXEC sp_addtype N'RefereeRelationship', N'tinyint', N'not null'
GO

EXEC sp_addtype N'RequestFlags', N'tinyint', N'not null'
GO

EXEC sp_addtype N'RequestStatus', N'tinyint', N'not null'
GO

EXEC sp_addtype N'Salary', N'decimal(18,8)', N'not null'
GO

EXEC sp_addtype N'SalaryRateType', N'tinyint', N'not null'
GO

EXEC sp_addtype N'ThreadParticipantFlags', N'tinyint', N'not null'
GO

EXEC sp_addtype N'Url', N'nvarchar (1000)', N'not null'
GO

EXEC sp_addtype N'UserEventContext', N'tinyint', N'not null'
GO

EXEC sp_addtype N'UserEventType', N'tinyint', N'not null'
GO

EXEC sp_addtype N'UserFlags', N'smallint', N'not null'
GO

