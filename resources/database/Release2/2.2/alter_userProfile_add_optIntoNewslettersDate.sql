ALTER TABLE linkme_owner.user_profile
ADD optIntoNewslettersDate DATETIME
GO

-- Handle existing data - if they haven't opted in to newsletters default the date to SQL mindate,
-- otherwise set it to the join date.

declare @mindate as Datetime
Set @mindate=-53690
	
UPDATE linkme_owner.user_profile
SET optIntoNewslettersDate = @mindate
WHERE OptOutOfNewsletters = 1

UPDATE linkme_owner.user_profile
SET optIntoNewslettersDate = joinDate
WHERE OptOutOfNewsletters <> 1
	
GO


ALTER TABLE linkme_owner.user_profile
ALTER COLUMN optIntoNewslettersDate DATETIME NOT NULL
GO
