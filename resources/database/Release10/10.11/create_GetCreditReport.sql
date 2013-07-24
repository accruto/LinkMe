if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetCreditReport]') and xtype in (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[GetCreditReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetCredits]') and xtype in (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[GetCredits]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetUsedCredits]') and xtype in (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[GetUsedCredits]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetAllocationUsedCredits]') and xtype in (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[GetAllocationUsedCredits]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetAddedCredits]') and xtype in (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[GetAddedCredits]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetExpiredCredits]') and xtype in (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[GetExpiredCredits]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetDeallocatedCredits]') and xtype in (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[GetDeallocatedCredits]
GO

CREATE FUNCTION dbo.GetCredits (@ownerId UNIQUEIDENTIFIER, @creditId UNIQUEIDENTIFIER, @date DATETIME)
RETURNS INT
AS
BEGIN

	DECLARE @credits INT

	-- Look for unlimited credits allocated before the start date but
	-- had not already expired or been deallocated.

	IF EXISTS
	(
		SELECT
			*
		FROM
			dbo.CreditAllocation
		WHERE
			ownerId = @ownerId AND creditId = @creditId
			AND createdTime < @date
			AND (expiryDate IS NULL OR expiryDate >= @date)
			AND (deallocatedTime IS NULL OR deallocatedTime >= @date)
			AND initialQuantity IS NULL
	)
		SET @credits = NULL
	ELSE
		SET @credits =
		(
			SELECT
				COALESCE(SUM(initialQuantity - dbo.GetAllocationUsedCredits(a.id, @date)), 0)
			FROM
				dbo.CreditAllocation AS a
			WHERE
				a.ownerId = @ownerId AND a.creditId = @creditId
				AND createdTime < @date
				AND (expiryDate IS NULL OR expiryDate >= @date)
				AND (deallocatedTime IS NULL OR deallocatedTime >= @date)
				AND NOT initialQuantity IS NULL
		)

	RETURN @credits
END
GO

CREATE FUNCTION dbo.GetAddedCredits (@ownerId UNIQUEIDENTIFIER, @creditId UNIQUEIDENTIFIER, @startDate DATETIME, @endDate DATETIME)
RETURNS INT
AS
BEGIN

	DECLARE @credits INT

	-- Credits with created time between the dates

	IF EXISTS
	(
		SELECT
			*
		FROM
			dbo.CreditAllocation
		WHERE
			ownerId = @ownerId AND creditId = @creditId
			AND createdTime >= @startDate AND createdTime < @endDate
			AND initialQuantity IS NULL
	)
		SET @credits = NULL
	ELSE
		SET @credits =
		(
			SELECT
				COALESCE(SUM(initialQuantity), 0)
			FROM
				dbo.CreditAllocation
			WHERE
				ownerId = @ownerId AND creditId = @creditId
				AND createdTime >= @startDate AND createdTime < @endDate
		)

	RETURN @credits
END
GO

CREATE FUNCTION dbo.GetUsedCredits (@ownerId UNIQUEIDENTIFIER, @creditId UNIQUEIDENTIFIER, @startDate DATETIME, @endDate DATETIME)
RETURNS INT
AS
BEGIN

	DECLARE @credits INT

	SET @credits =
	(
		SELECT
			COUNT(*)
		FROM
			dbo.CandidateAccessPurchase AS p
		INNER JOIN
			dbo.CreditAllocation AS a ON a.id = p.allocationId
		WHERE
			a.ownerId = @ownerId AND a.creditId = @creditId
			AND p.purchaseTime >= @startDate AND p.purchaseTime < @endDate
			AND p.adjustedAllocation = 1
	)

	RETURN @credits
END
GO

CREATE FUNCTION dbo.GetAllocationUsedCredits (@allocationId UNIQUEIDENTIFIER, @date DATETIME)
RETURNS INT
AS
BEGIN

	DECLARE @credits INT

	SET @credits =
	(
		SELECT
			COUNT(*)
		FROM
			dbo.CandidateAccessPurchase
		WHERE
			allocationId = @allocationId
			AND purchaseTime < @date
			AND adjustedAllocation = 1
	)

	RETURN @credits
END
GO

CREATE FUNCTION dbo.GetExpiredCredits (@ownerId UNIQUEIDENTIFIER, @creditId UNIQUEIDENTIFIER, @startDate DATETIME, @endDate DATETIME)
RETURNS INT
AS
BEGIN

	DECLARE @credits INT

	-- Credits with expiry date between the dates.

	IF EXISTS
	(
		SELECT
			*
		FROM
			dbo.CreditAllocation
		WHERE
			ownerId = @ownerId AND creditId = @creditId
			AND expiryDate IS NOT NULL
			AND expiryDate >= @startDate AND expiryDate < @endDate
			AND quantity IS NULL
	)
		SET @credits = NULL
	ELSE
		SET
			@credits = 
			(
				SELECT
					COALESCE(SUM(quantity), 0)
				FROM
					dbo.CreditAllocation
				WHERE
					ownerId = @ownerId AND creditId = @creditId
					AND expiryDate IS NOT NULL
					AND expiryDate >= @startDate AND expiryDate < @endDate
			)

	RETURN @credits
END
GO

CREATE FUNCTION dbo.GetDeallocatedCredits (@ownerId UNIQUEIDENTIFIER, @creditId UNIQUEIDENTIFIER, @startDate DATETIME, @endDate DATETIME)
RETURNS INT
AS
BEGIN

	DECLARE @credits INT

	-- Credits with deallocation date between the dates.

	IF EXISTS
	(
		SELECT
			*
		FROM
			dbo.CreditAllocation
		WHERE
			ownerId = @ownerId AND creditId = @creditId
			AND deallocatedTime IS NOT NULL
			AND deallocatedTime >= @startDate AND deallocatedTime < @endDate
			AND quantity IS NULL
	)
		SET @credits = NULL
	ELSE
		SET
			@credits = 
			(
				SELECT
					COALESCE(SUM(quantity), 0)
				FROM
					dbo.CreditAllocation
				WHERE
					ownerId = @ownerId AND creditId = @creditId
					AND deallocatedTime IS NOT NULL
					AND deallocatedTime >= @startDate AND deallocatedTime < @endDate
			)

	RETURN @credits
END
GO

CREATE FUNCTION dbo.GetCreditReport (@ownerId UNIQUEIDENTIFIER, @creditId UNIQUEIDENTIFIER, @startDate DATETIME, @endDate DATETIME)
RETURNS @report TABLE
(
	opening INT,
	added INT,
	used INT,
	expired INT,
	deallocated INT,
	closing INT
)
AS
BEGIN

	-- Create the default row to start with.

	DECLARE @opening INT
	DECLARE @added INT
	DECLARE @used INT
	DECLARE @expired INT
	DECLARE @deallocated INT
	DECLARE @closing INT

	-- Opening.

	SET @opening = dbo.GetCredits (@ownerId, @creditId, @startDate)

	-- Added.
	
	SET @added = dbo.GetAddedCredits (@ownerId, @creditId, @startDate, @endDate)

	-- Used.
	
	SET @used = dbo.GetUsedCredits (@ownerId, @creditId, @startDate, @endDate)

	-- Expired.

	SET @expired = dbo.GetExpiredCredits (@ownerId, @creditId, @startDate, @endDate)

	-- Deallocated.

	SET @deallocated = dbo.GetDeallocatedCredits (@ownerId, @creditId, @startDate, @endDate)

	-- Closing.

	SET @closing = dbo.GetCredits (@ownerId, @creditId, @endDate)

	INSERT
		@report (opening, added, used, expired, deallocated, closing)
	VALUES
		(@opening, @added, @used, @expired, @deallocated, @closing)

	RETURN
END
GO