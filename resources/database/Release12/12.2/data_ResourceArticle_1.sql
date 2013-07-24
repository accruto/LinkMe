insert into resourcearticle
values ('E89F2F11-13E3-451E-BD5B-A59230F36FC3',
'22374AB3-7AEB-4C4B-95EE-EC89C3DC9944',
'Why are Australian workers doing more hours?',
'In a recent survey of almost 1000 IT workers, Balance Recruitment has found more than 50% of employees are working significant levels of overtime with no additional remuneration. Some employees indicated they were working as much as 40 additional hours per month over and above their standard working hours. Based on a Australian IT workforce of approximately 500000, and an average salary of $85000, it translates to businesses getting around $A2.6 billion of free work on an annual basis.

Greg Pankhurst, co-owner of Balance Recruitment, cited economic issues as being a key driver for the increased pressures on IT teams. “IT is now a global business, and with the Australian dollar so high, outsourcing to China, the Philippines, the Sub-Continent or even Europe has never been so attractive financially. And while there are certainly challenges associated with offshoring your technical teams, the model is far more mature than it was a few years ago. If a company makes the decision to bear the additional costs and keep their IT  based in Australia, their expectations around levels of service and delivery are going to be very high.”

“The rise of mobile devices has also been a factor. People are expecting to be available and on-line 24x7, and that in turn puts pressure on the IT team to be available 24x7”.

That increased workload is coming at a cost though. Balance also asked those same people what their biggest workplace stress was, and the standout was unrealistic workloads. 

Pankhurst went on to say it’s important employers recognise and acknowledge the efforts of their employees.  “As much as companies are under pressure, It’s important to monitor the hours people are pulling and make sure that they are being recognised and rewarded for their time (be that time in lieu or additional pay). While people have accepted that they will have to put in periods of heavy work, it’s very important that companies don’t abuse that acceptance. It’s a fine line between being a hard working environment and a sweatshop.”

Key Survey Questions:

Have you been asked by your employer to work overtime/weekend work with no financial reward or time in lieu?

Yes – up to 10 hrs/month - 23.1%
Yes – up to 20 hrs/month - 14.8%
Yes – up to 30 hrs/month - 3.9%
Yes – up to 40 hrs/month - 2.9%
(Total of Yes) - 50.7%
No - 49.3%

If you are not in a management position, what keeps you awake at night?

Stability of my role - 38.2%
My pay - 38.9%
Workplace Culture/Management - 39.6%
Workload – unrealistic deadlines - 51.8%
Career growth/lack of challenges - 50.2%',
0,
getdate(),
null)
go

update resourcearticle
set text = '<p>'+replace(cast(text as varchar(8000)), char(13) + char(10),'</p>'+char(13) + char(10)+'<p>') +'</p>'
where id = 'E89F2F11-13E3-451E-BD5B-A59230F36FC3'
go

update resourcearticle
set shortUrl = 'http://bit.ly/I7QaT8'
where id = 'E89F2F11-13E3-451E-BD5B-A59230F36FC3'
go
