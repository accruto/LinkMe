insert into resourcearticle
values ('A1432CC9-D8E7-45F4-9FB5-3FD057A15118',
'3F0BED40-388E-450C-8849-EE47A68B8335',
'Including Key Achievements in Your Resume',
'If I had a dollar for every resume I saw that did not include “key achievements”, I would be a very wealthy resume writer!
Failing to include key achievements throughout your resume is a recipe for disaster and will cause your resume to be put straight in the deleted folder and never to be seen again.
The job market is competitive, and if you’re going to prove to the hiring manager that you are the best candidate for a job, you need to show off every key achievement and skill that will stand you out against all the other job candidates.  Remember the golden rule of resume writing – your resume is a marketing document and, as such, needs to market all the great things that you can bring to a potential job. Providing achievements that are backed up with quantitative evidence will guarantee that you will stand out from the other job seekers.
The best written resumes adequately sell the person’s achievements, skills and personality. Do this correctly and I guarantee that you will find success.
<b>What Types of Achievements should you include in your resume?</b>
Employers want to know the value you are going to add to the business and therefore want to see examples of your past behaviours to indicate your future behaviours. Types of achievements to include are:
•	Ways you saved the company money
•	Examples of how you reduced costs
•	Examples of new ideas or implementations that resulted in positive outcomes
•	Special awards or recognitions you received (e.g. voted #1 salesperson for two consecutive years)
•	Training, hiring, mentoring, leading, managing staff
•	Resolution of problems or issues that led to a positive outcome
•	Training courses, seminars, workshops that you successfully completed

<b>Tricks and Tips to turn your resume into a selling tool:</b>
Use strategic keywords throughout your resume to catch the reader’s eye. Strategic keywords will ensure that your resume will be picked up by employers using software programs that help eliminate candidate resumes
Go through the job requirements to find out exactly what the employer is looking for in the right candidate and incorporate these directly into your resume. For example, if the job is looking for someone with leadership skills, make sure you provide examples about the leadership you performed either in your past jobs or through community involvement or extra-curricular activities
Including responsibilities and duties in your resume are important because it shows the reader what you actually do on a day to day basis. However, in order to take your resume to the next level and stand out against the competition (and get the highest possible salary!), you need to focus on value added achievements.
',
0,
getdate(),
null)
go

update resourcearticle
set text = '<p>'+replace(cast(text as varchar(8000)), char(13) + char(10),'</p>'+char(13) + char(10)+'<p>') +'</p>'
where id = 'A1432CC9-D8E7-45F4-9FB5-3FD057A15118'
go

update resourcearticle
set shortUrl = 'http://bit.ly/MyJrpR'
where id = 'A1432CC9-D8E7-45F4-9FB5-3FD057A15118'
go