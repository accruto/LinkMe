if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetDistanceBetweenCoordinatesInKm]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[GetDistanceBetweenCoordinatesInKm]
GO

CREATE FUNCTION dbo.GetDistanceBetweenCoordinatesInKm(@latitude1 AS FLOAT, @longitude1 AS FLOAT, @latitude2 AS FLOAT, @longitude2 AS FLOAT)
RETURNS FLOAT
AS
BEGIN
	RETURN DEGREES(ACOS(SIN(RADIANS(@latitude1)) * SIN(RADIANS(@latitude2)) + COS(RADIANS(@latitude1)) * COS(RADIANS(@latitude2)) * COS(RADIANS(@longitude1 - @longitude2))) * 60 * 1.1515 * 1.609344)
END
GO
