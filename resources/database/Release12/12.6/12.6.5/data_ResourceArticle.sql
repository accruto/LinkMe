insert into resourcearticle
values ('D9121EF3-99E3-4F44-AC0A-1656127CD6B0',
'33A21456-AD77-4DCD-9749-3EF37E909CA1',
'Tips on how to get back into the job market',
'Have you been out of work for an extended period of time? Perhaps you have been on maternity leave and now ready to jump back into the job force. Maybe you took a “time out” to travel and explore different parts of the world. Whatever the reason, it is never too late to get back into the job market successfully, and we have a few suggestions for helping you to do so. 
<b>Research your options:</b>
One of the first things you might consider doing is actually thinking about what it is you want to do. Do you have a specific field or industry in mind? Do you know what relevant qualifications/skills are required for that position? What have you done in your past that is relevant to that position? These are all good things to think about, and will help you with our next step.
<b>Re-write your resume:</b>
If you’ve been out of the workforce for an extended period of time, chances are that your resume is probably a bit out of date and needs to be more relevant to the present. Now that you have a good idea of what you want to do, you can now re-write your resume so it is more targeted toward that industry. Research the skills that are in demand for that industry, and make sure your resume includes these (if they are relevant to your experience). Think of the transferrable skills that you may have learned in your previous positions and incorporate these into your resume as well. 
You might also want to think about addressing your employment gap somewhere in your resume. A good tip is to write a “qualifications profile” at the very start of their resume, where you can summarize and promote your experience and qualifications. 
Most importantly don’t forget to include keywords. A well resume will include the use of strategic keywords. Many times recruiters or hiring managers will often skim over the resume looking for keywords or use software programs to find key words.  These key words can be changed depending on the job you are applying for. A keyword rich resume will help highlight your resume and cover letter.
<b>Learn new skills:</b>
It is never too late to learn something new that might assist you in a job position, so you might want to consider taking some classes that are relevant to the industry you’re looking to get into. Look into some relevant certificate programs or brush up your skills in customer service or sales (industry dependant, of course). If you don’t have time, there are always online courses available that you can take at your own pace.
<b>Apply!</b>
You will never know unless you try! After you’ve considered the above steps, start applying for jobs and see what happens... you never know. If you find that you aren’t finding success, you can at least get a better idea into what hiring managers are looking for specifically, and you can work toward meeting those qualifications.',
0,
getdate(),
null)
go

insert into resourcearticle
values ('9B6F2C10-376D-46AF-90B9-C1B4138DD8EC',
'3F0BED40-388E-450C-8849-EE47A68B8335',
'Is Your Professional Resume A Gold Medal Document?',
'With the Olympic Games fast approaching, athletes from across the world will all be vying to be the very best in their chosen sport and take home a gold medal. In events such as swimming and athletics it is usually only seconds that separate first and last and with competition so strong, a tiny mistake can often be the difference between winning gold or missing out on a medal. 
 
Just as an athlete will have to prepare and train in order to win gold, a job seeker needs to ensure that their resume is worthy of winning a gold medal! Although competition for a job may not be as fierce as competition for a gold medal, the same rules apply. A simple spelling mistake could lead to your resume being deleted and you missing out on your dream job.
 
Preparation is key for any athlete and the same applies for any job seeker. Going online and using an out dated resume template which you complete in 10 minutes will not stand you out from your job seeking competitors. Before you even begin writing your resume, you need to have an understanding of the type of positions you are going to be applying for, and the type of skills and experience that will be required for that particular position. With this understanding, you will be in a far greater position to target your resume towards the types of jobs you are applying for. 
 
<b>Market your Skills on Page 1 of the Resume:</b>
 
Reports suggest that hiring managers spend between 10-20 seconds when first reading through your resume. In this short time you need to grab the reader’s attention. Introducing a qualifications profile or career summary is a great way to show off your skills to the reader within the first 2-3 lines of the resume. Rather than opening your resume with an objective statement (telling the reader what you want out of the job) -introduce a qualifications profile where you tell the reader the value-added skills that you can offer the business. From a hiring managers perspective which resume would you rather read?
 
<b>Highlight your Achievements:</b>
 
Your resume is your marketing document. Don’t be afraid to highlight your achievements, awards, skills and expertise. If you are a manager include how many people you manage. If you received a promotion or award, point these out in the resume. The more quantitative examples you can provide the greater. Remember that your resume may be competing against hundreds of other resumes. Although you may be the most qualified or the most talented, if you are unable to portray your achievements throughout your resume than you greatly reduce your chances of being selected for the interview stage. 
 
Just as an athlete needs everything to go right on their day in order to win gold, a job seeker is the same. There is not one most important aspect that makes a professional resume but a lot of smaller details that goes into preparing a gold medal winning resume. Marketing your skills and highlighting your achievements will give you a strong advantage over your competition and help you stand out from the crowd.',
0,
getdate(),
null)
go

update resourcearticle
set text = '<p>'+replace(cast(text as varchar(8000)), char(13) + char(10),'</p>'+char(13) + char(10)+'<p>') +'</p>'
where id = 'D9121EF3-99E3-4F44-AC0A-1656127CD6B0'
or id = '9B6F2C10-376D-46AF-90B9-C1B4138DD8EC'
go

update resourcearticle
set shortUrl = 'http://bit.ly/QdI6rq'
where id = 'D9121EF3-99E3-4F44-AC0A-1656127CD6B0'
go

update resourcearticle
set shortUrl = 'http://bit.ly/NX71NN'
where id = '9B6F2C10-376D-46AF-90B9-C1B4138DD8EC'
go
