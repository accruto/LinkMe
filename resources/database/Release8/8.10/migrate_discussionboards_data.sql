--1. Create discussion boards for exising groups
insert into [dbo].[discussionboard] (id, deleted) 
select id, 'false' from [group]

update [dbo].[group] set boardId = id

--2. Add board moderators (from group admins)
insert into [dbo].DiscussionBoardModerator (boardId, contributorId)
select groupid, contributorid from groupmembership
where flags & 1 = 1

--3. Disable NULLs for boardId when after all data has been migrated
ALTER TABLE [Group] ALTER COLUMN boardId uniqueidentifier NOT NULL