IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SearchableMember]'))
DROP VIEW [dbo].[SearchableMember]
GO

CREATE VIEW [dbo].[SearchableMember]
AS
SELECT
	DISTINCT m.id
FROM
    Member AS m WITH (NOLOCK)
INNER JOIN
    RegisteredUser AS u WITH (NOLOCK) ON u.id = m.id
INNER JOIN
    Candidate AS c WITH (NOLOCK) ON c.id = m.id
INNER JOIN
    CandidateResume AS cr WITH (NOLOCK) ON cr.candidateId = c.id
INNER JOIN
    Resume AS r WITH (NOLOCK) ON r.id = cr.resumeId
WHERE

-- Enabled

    (u.flags & 0x4) = 0

-- Resume visible

    AND (m.employerAccess & 0x1) = 1

-- Status is not 'Not looking'

    AND c.status != 1

-- Activated

    AND (u.flags & 0x20) = 0x20

-- Is contactable

    AND
    (
		(
			-- Email address is verified

            (u.emailAddress IS NOT NULL AND u.emailAddressVerified = 1)
            OR
            (u.secondaryEmailAddress IS NOT NULL AND u.secondaryEmailAddressVerified = 1)
		)
        OR
        (
			-- Has phone number

            m.primaryPhoneNumber IS NOT NULL
            OR m.secondaryPhoneNumber IS NOT NULL
            OR m.tertiaryPhoneNumber IS NOT NULL
		)
	)

-- Is resume empty

    AND
	(
		r.isEmpty = 0
		OR EXISTS (SELECT resumeId FROM dbo.ResumeJob WITH (NOLOCK) WHERE resumeId = r.id AND isEmpty = 0)
		OR EXISTS (SELECT resumeId FROM dbo.ResumeSchool WITH (NOLOCK) WHERE resumeId = r.id AND isEmpty = 0)
	)

GO
