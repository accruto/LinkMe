delete from dbo.CandidateListEntry
where jobApplicationId
in
(
	select app.[id]
	from dbo.JobApplication app
	left outer join dbo.JobAd ja
	on ja.[id] = app.jobAdId
	where ja.[id] IS NULL
)
GO

delete from dbo.JobApplication
where [id]
in
(
	select app.[id]
	from dbo.JobApplication app
	left outer join dbo.JobAd ja
	on ja.[id] = app.jobAdId
	where ja.[id] IS NULL
)
GO
