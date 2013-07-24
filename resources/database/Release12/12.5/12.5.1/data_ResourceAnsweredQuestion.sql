insert into resourceAnsweredQuestion
values ('A87D15CD-4813-420E-B64B-D68A5349DDC0',
'90441A84-28BF-42C8-AC7C-01A6C5D54AAC',
'I''ve arrived in Australia and applied for 50+ jobs with no luck. How can I make my CV stand out?',
'In order to succeed in the Australian job market your Australian resume needs to be written, structured, formatted and presented in the correct way. With competition for jobs in Australia at such a high demand, and in order to stand out from the competition, you need an Australian resume that adequately highlights your skills, achievements and value that you can bring to your next role. Below are my 3 most important tips for making your resume stand out.
 
<b>Include a Qualifications Profile</b>
Remember the number 1 rule about resume writing – highlight your achievements. Your resume is a marketing document and as such you need to emphasise to the reader your skills and expertise. Many times, job candidates will begin their resume with an objective statement where they describe to the reader the type of job that they are interested in. Unfortunately, a hiring manager is not interested in the types of positions you wish to apply for, but they are interested in knowing that you are passionate about the job position that they are advertising for. By replacing an objective statement with a qualifications profile will ensure that your resume begins with a powerful profile that highlights your value-added skills and qualifications.
 
<b>Strategic Keywords</b>
The use of Strategic keywords is essential for a modern day resume. When applying for a certain position, your resume may be one of a thousand that the company will receive. Rather than reading through every single resume, companies now use software programs as a way of performing the first round selection. By using strategic keywords you will not only make it through the first round of selection but you will give yourself every opportunity of standing out above your competition.

<b>Demonstrate Flexibility, Adaptability and Innovation</b>
Although you may not have local experience do not despair! In my experience employers want to hire job seekers who can demonstrate flexibility, adaptability and innovation as well as a passion for wanting to gain knowledge and experience. Think about extra-curricular activities, work experience, community service or volunteer work that you have performed and exhibited skills of flexibility, adaptability and innovation. Remember that an employer wants to know that you are going to bring value to the organisation. Use your resume to demonstrate that you are a forward-thinking self-starter with vision and desire to implement innovative solutions to any problem that may arise.',
getdate(),
null)
go

update resourceAnsweredQuestion
set text = '<p>'+replace(cast(text as varchar(8000)), char(13) + char(10),'</p>'+char(13) + char(10)+'<p>') +'</p>'
where id = 'A87D15CD-4813-420E-B64B-D68A5349DDC0'
go

update resourceAnsweredQuestion
set shortUrl = 'http://bit.ly/LpLi0i'
where id = 'A87D15CD-4813-420E-B64B-D68A5349DDC0'
go
