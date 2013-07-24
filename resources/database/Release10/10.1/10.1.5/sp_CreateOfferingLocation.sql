IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateOfferingLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateOfferingLocation]
GO

CREATE PROCEDURE CreateOfferingLocation
(
	@offeringId UNIQUEIDENTIFIER,
	@locationReferenceId UNIQUEIDENTIFIER,
	@unstructuredLocation NVARCHAR(100),
	@namedLocationId INT
)
AS

IF NOT EXISTS (SELECT * FROM dbo.OfferingLocation WHERE offeringId = @offeringId AND locationReferenceId = @locationReferenceId)
BEGIN
	INSERT
		dbo.LocationReference (id, unstructuredLocation, namedLocationId, countrySubdivisionId, localityId)
	VALUES
		(@locationReferenceId, @unstructuredLocation, @namedLocationId, @namedLocationId, NULL)

	INSERT
		dbo.OfferingLocation (offeringId, locationReferenceId)
	VALUES
		(@offeringId, @locationReferenceId)
END

GO

