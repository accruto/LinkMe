IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateOfferingLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateOfferingLocation]
GO

CREATE PROCEDURE CreateOfferingLocation
(
	@id UNIQUEIDENTIFIER,
	@offeringId UNIQUEIDENTIFIER,
	@unstructuredLocation NVARCHAR(100)
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

INSERT
	dbo.OfferingCriteria (id, name, value)
VALUES
	(@id, 'Country', 1)

INSERT
	dbo.OfferingCriteria (id, name, value)
VALUES
	(@id, 'Location', @unstructuredLocation)

GO