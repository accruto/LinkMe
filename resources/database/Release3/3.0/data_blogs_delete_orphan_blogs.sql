-- Delete all existing blogs that are not owned by a Member. Because there is currently no RI in the Blog
-- tables we have 4 blogs belonging to Employers and 2 blogs belonging to non-existing users.

DELETE linkme_owner.tbl_Comment
FROM linkme_owner.tbl_Comment c
INNER JOIN linkme_owner.tbl_Post p
ON c.Parent = p.[id]
INNER JOIN linkme_owner.tbl_Blog b
ON p.Blog = b.[id]
WHERE NOT EXISTS ( SELECT * FROM linkme_owner.networker_profile WHERE [id] = b.UserId )

DELETE linkme_owner.tbl_Post
FROM linkme_owner.tbl_Post p
INNER JOIN linkme_owner.tbl_Blog b
ON p.Blog = b.[id]
WHERE NOT EXISTS ( SELECT * FROM linkme_owner.networker_profile WHERE [id] = b.UserId )

DELETE linkme_owner.tbl_Blog
FROM linkme_owner.tbl_Blog b
WHERE NOT EXISTS ( SELECT * FROM linkme_owner.networker_profile WHERE [id] = b.UserId )

GO
