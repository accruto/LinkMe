if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetOfferRequestCategories]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[GetOfferRequestCategories]
GO

CREATE FUNCTION [dbo].[GetOfferRequestCategories] (@userId UNIQUEIDENTIFIER, @providerId UNIQUEIDENTIFIER, @startDate DATETIME, @endDate DATETIME)
RETURNS NVARCHAR(4000)

AS
BEGIN

DECLARE @categories NVARCHAR(4000)
SET @categories = ''
DECLARE @part NVARCHAR(4000)

DECLARE @categoryName NVARCHAR(255)
DECLARE @parentCategoryName NVARCHAR(255)
DECLARE @grandParentCategoryName NVARCHAR(255)

DECLARE @csrCategories CURSOR
SET @csrCategories = CURSOR FOR
SELECT
	DISTINCT
	C.name AS Category,
	PC.name AS ParentCategory,
	GC.name AS GrandParentCategory
FROM
	Offer AS O
INNER JOIN
	OfferRequest AS R ON R.id = O.requestId
INNER JOIN
	OfferCategory AS C ON C.id = R.categoryId
INNER JOIN
	Offering AS F ON F.id = O.offeringId
INNER JOIN
	OfferProvider AS P ON P.id = F.providerId
INNER JOIN
	Member AS M ON M.id = R.userId
INNER JOIN
	RegisteredUser AS U ON U.id = M.id
LEFT OUTER JOIN
	OfferCategory AS PC ON PC.id = C.parentId
LEFT OUTER JOIN
	OfferCategory AS GC ON GC.id = PC.parentId
WHERE
	R.userId = @userId
	AND P.id = @providerId
	AND (O.createdTime >= @startDate AND O.createdTime < DATEADD(d, 1, @endDate))
	AND P.enabled = 1
	AND F.enabled = 1
	AND C.enabled = 1
	AND C.deleted = 0

OPEN @csrCategories
FETCH NEXT FROM @csrCategories INTO @categoryName, @parentCategoryName, @grandParentCategoryName

WHILE @@FETCH_STATUS = 0
BEGIN
	IF NOT @grandParentCategoryName IS NULL
		SET @part = @grandParentCategoryName + CHAR(13) + CHAR(10) + '  ' + @parentCategoryName + CHAR(13) + CHAR(10) + '  ' + @categoryName
	ELSE IF NOT @parentCategoryName IS NULL
		SET @part = @parentCategoryName + CHAR(13) + CHAR(10) + '  ' + @categoryName
	ELSE
		SET @part = @categoryName

	IF LEN(@categories) = 0
		SET @categories = @part
	ELSE
		SET @categories = @categories + CHAR(13) + CHAR(10) + @part

	FETCH NEXT FROM @csrCategories INTO @categoryName, @parentCategoryName, @grandParentCategoryName
END
CLOSE @csrCategories
DEALLOCATE @csrCategories

RETURN @categories

END

GO