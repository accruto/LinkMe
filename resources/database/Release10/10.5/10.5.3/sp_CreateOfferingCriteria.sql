IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateOfferingCriteria]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateOfferingCriteria]
GO

CREATE PROCEDURE CreateOfferingCriteria
(
	@id UNIQUEIDENTIFIER,
	@offeringId UNIQUEIDENTIFIER,
	@name1 NVARCHAR(50),
	@value1 SQL_VARIANT,
	@name2 NVARCHAR(50),
	@value2 SQL_VARIANT,
	@name3 NVARCHAR(50),
	@value3 SQL_VARIANT,
	@name4 NVARCHAR(50),
	@value4 SQL_VARIANT
)
AS

DELETE
	dbo.OfferingCriteria
WHERE
	id = @id

DELETE
	dbo.OfferingCriteriaSet
WHERE
	id = @id

INSERT
	dbo.OfferingCriteriaSet (id, offeringId, type)
VALUES
	(@id, @offeringId, 'AdvancedMemberSearchCriteria')

IF NOT @name1 IS NULL
	INSERT
		dbo.OfferingCriteria (id, name, value)
	VALUES
		(@id, @name1, @value1)

IF NOT @name2 IS NULL
	INSERT
		dbo.OfferingCriteria (id, name, value)
	VALUES
		(@id, @name2, @value2)

IF NOT @name3 IS NULL
	INSERT
		dbo.OfferingCriteria (id, name, value)
	VALUES
		(@id, @name3, @value3)

IF NOT @name4 IS NULL
	INSERT
		dbo.OfferingCriteria (id, name, value)
	VALUES
		(@id, @name4, @value4)

GO