-- Insert an empty Resume for RI purposes if no row exists for a viewed resume, but there's a Candidate
-- with that ID. In the future Resume rows should not be deleted, but instead the text should be set to NULL.

INSERT INTO dbo.Resume([id], version, candidateId)
SELECT DISTINCT dbo.GuidFromString(elp.profileId), 1, dbo.GuidFromString(elp.profileId)
FROM linkme_owner.event_log el
INNER JOIN linkme_owner.event_log_profile elp
ON elp.eventLogId = el.[id]
INNER JOIN dbo.Candidate c
ON c.[id] = dbo.GuidFromString(elp.profileId)
WHERE el.event = 'ViewResume' AND LEN(elp.profileId) = 32 AND NOT EXISTS
(
	SELECT *
	FROM dbo.Resume
	WHERE [id] = dbo.GuidFromString(elp.profileId)
)

GO

-- Hack the old event_log table to add the new ID (GUID) to it. This is required, because there are
-- multiple inserts to do for some rows (actioned user/actioned resume). Normally this could be handled
-- using cursors instead of a schema hack, but cursors are just too slow for this amount of data.

ALTER TABLE linkme_owner.event_log
ADD [newId] UNIQUEIDENTIFIER NULL

GO

UPDATE linkme_owner.event_log
SET [newId] = NEWID()

GO

-- Migrate all events, except for SortOrderToggle.
-- Note that there are some rows with invalid GUIDs in event_log_profile - ignore them.

INSERT INTO dbo.UserEvent([id], [time], type, context, actorId)
SELECT [newId], [date],
	CASE event
		WHEN 'Login' THEN 1
		WHEN 'LoginPrompt' THEN 2
		WHEN 'ViewResume' THEN 3
		WHEN 'SendEmail' THEN 4
		WHEN 'ViewPhoneNumber' THEN 5
		WHEN 'DeactivateUser' THEN 6
		ELSE NULL -- This will fail if there's an unrecognised event type
	END,
	CASE event
		WHEN 'Login' THEN
			CASE context
				WHEN 'SuggestedCandidatesEmail' THEN 2
				ELSE 1
			END
		WHEN 'LoginPrompt' THEN
			CASE context
				WHEN 'SuggestedCandidatesEmail' THEN 2
				ELSE 1
			END
		ELSE 0
	END,
	dbo.GuidFromString(el.profileId)
FROM linkme_owner.event_log el
LEFT OUTER JOIN linkme_owner.event_log_profile elp
ON elp.eventLogId = el.[id]
WHERE el.event <> 'SortOrderToggle' AND (elp.profileId IS NULL OR elp.profileId = '' OR LEN(elp.profileId) = 32)

GO

-- Insert actioned user for SendEmail and ViewPhoneNumber events.

INSERT INTO dbo.UserEventActionedUser(eventId, userId)
SELECT el.[newId], dbo.GuidFromString(elp.profileId)
FROM linkme_owner.event_log el
INNER JOIN linkme_owner.event_log_profile elp
ON elp.eventLogId = el.[id]
WHERE (event = 'SendEmail' OR event = 'ViewPhoneNumber') AND LEN(elp.profileId) = 32

GO

-- In dev, there are some rows where no Resume or Candidate with the ID exists - just delete them.

DELETE dbo.UserEvent
FROM dbo.UserEvent ue
INNER JOIN linkme_owner.event_log el
ON ue.[id] = el.[newId]
INNER JOIN linkme_owner.event_log_profile elp
ON elp.eventLogId = el.[id]
WHERE el.event = 'ViewResume' AND LEN(elp.profileId) = 32 AND NOT EXISTS
(
	SELECT *
	FROM dbo.Candidate
	WHERE [id] = dbo.GuidFromString(elp.profileId)
)

GO

-- Insert actioned resume for ViewResume.

INSERT INTO dbo.UserEventActionedResume(eventId, resumeId)
SELECT el.[newId], dbo.GuidFromString(elp.profileId)
FROM linkme_owner.event_log el
INNER JOIN linkme_owner.event_log_profile elp
ON elp.eventLogId = el.[id]
INNER JOIN dbo.UserEvent ue
ON ue.[id] = el.[newId]
WHERE event = 'ViewResume' AND LEN(elp.profileId) = 32

GO

-- Insert extra data, other than "reason". Truncate the value to 1000 characters.

INSERT INTO dbo.UserEventExtraData(eventId, [key], value)
SELECT el.[newId], ed.dataKey, CAST(ed.dataValue AS NVARCHAR(1000))
FROM linkme_owner.event_log el
INNER JOIN linkme_owner.EventLogExtraData ed
ON el.[id] = ed.eventLogId
WHERE dataKey <> 'reason' AND dataValue IS NOT NULL AND dataValue NOT LIKE ''

-- Insert extra data for "reason", where the values need to be migrated from full text to enum names

INSERT INTO dbo.UserEventExtraData(eventId, [key], value)
SELECT el.[newId], ed.dataKey, CASE CAST(ed.dataValue AS NVARCHAR(1000))
	WHEN 'I don''t want my current employer to know I''m looking for work' THEN 'HideFromEmployer'
	WHEN 'I''m not looking for work anymore' THEN 'NoLongerLooking'
	WHEN 'I receive too many newsletter emails' THEN 'NewsletterSpam'
	WHEN 'I receive too many job alert emails' THEN 'JobAlertSpam'
	WHEN 'I don''t find the website useful' THEN 'NotUseful'
	WHEN 'I couldn''t find the job I was looking for' THEN 'NotFoundJob'
	WHEN 'I couldn''t contact people to build my network' THEN 'NotBuiltNetwork'
	WHEN 'I am going to sign up using different login details' THEN 'Rejoining'
	WHEN 'Other' THEN 'Other'
	ELSE NULL -- Invalid - this will fail due to not-NULL constraint.
	END
FROM linkme_owner.event_log el
INNER JOIN linkme_owner.EventLogExtraData ed
ON el.[id] = ed.eventLogId
WHERE dataKey = 'reason' AND dataValue IS NOT NULL AND dataValue NOT LIKE ''

GO

-- Return event_log to its previous state.

ALTER TABLE linkme_owner.event_log
DROP COLUMN [newId]

GO

-- Migrate disabled user profiles to DeactivateUser events.

INSERT INTO dbo.UserEvent([id], [time], type, context, actorId)
SELECT NEWID(), DisabledDate, 6, 0, dbo.GuidFromString([id])
FROM linkme_owner.disabled_user_profile_info

GO

-- Set the "actioned user" for disabled user profiles to the actor. From 3.0 it's only
-- possible for an admin to "disable" the user whereas the user can "deactivate" themselves.
-- Before 3.0, the concept of self deactivation was like the current "disable" function, so
-- it seems more accurate to migrate those event as "disable" events, but with the user as
-- the actor.

INSERT INTO dbo.UserEventActionedUser (eventId, userId)
SELECT [id], actorId
FROM dbo.UserEvent
WHERE type = 6

GO

-- Insert UploadResume events from networker_profile.resumeFirstUploadedDate.

INSERT INTO dbo.UserEvent([id], [time], type, context, actorId)
SELECT NEWID(), resumeFirstUploadedDate, 8, 0, dbo.GuidFromString([id])
FROM linkme_owner.networker_profile
WHERE resumeFirstUploadedDate IS NOT NULL AND resumeFirstUploadedDate <> '1753-01-01'

-- Insert EditResume events from networker_profile.resumeLastUpdatedDate.
-- Note that if it's the same as the join date that means the resume was never edited.

INSERT INTO dbo.UserEvent([id], [time], type, context, actorId)
SELECT NEWID(), resumeLastUpdatedDate, 9, 0, dbo.GuidFromString(np.[id])
FROM linkme_owner.networker_profile np
INNER JOIN linkme_owner.user_profile up
ON np.[id] = up.[id]
WHERE resumeLastUpdatedDate IS NOT NULL AND resumeLastUpdatedDate <> joinDate

-- Set the "actioned resume" for uploads and edits to be the one and only resume for the user, if any.
-- The fact that resume IDs were the same as member IDs helps here.

INSERT INTO dbo.UserEventActionedResume(eventId, resumeId)
SELECT e.[id], r.[id]
FROM dbo.UserEvent e
INNER JOIN dbo.Resume r
ON e.actorId = r.[id]
WHERE e.type IN (8, 9)

GO
