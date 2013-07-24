insert into resourceAnsweredQuestion
values ('C531330B-F8ED-4998-BD1A-AF59BFFCD7E4',
'8F508E2C-C7BB-46FE-8BD6-AA812054DC03',
'Does your resume Pass or Fail?',
'Does your professional resume stand out from the competition? Will your resume meet the expectations of employers or hiring managers or will it end up in the deleted items folder?
 
Resume writing is an art form and while there are no exact rules that will ensure you get the job, there are plenty of mistakes that you can make that will prevent your resume from being noticed. No matter if you are a recent student or a high flying executive, a compelling written resume that highlights your skills and promotes your talents to the hiring manager will ensure that you give yourself every opportunity of making it to the interview stage. 
 
<b>What will make my resume stand out from the competition?</b> 
The most important aspect of resume writing is marketing yourself correctly. Do not waste the reader’s time with irrelevant information that will not aid your job description. Statistically your resume has less than 30 seconds to shine so make sure the most important information is placed on the front page. If you require a certain qualification or licence make sure this is easily located on your resume. The more you make the recruiter or hiring manager work to find your information, the greater chance they will delete your resume. 
 
<b>Replace the Objective Statement with a Qualifications Profile</b> 
Do you have a generic, obsolete and boring objective statement at the top of your resume? Something that reads like: “Self-motivated professional seeking a position with a company where I can develop my career and skills”. If so, remove this immediately and replace it with a qualifications profile or career summary. Rather than telling the reader what you want, tell the reader what you can offer their organisation and therefore why you would be a good fit for this role.
 
<b>Target the Reader</b> 
As a hiring manager, when I read job candidate resumes, I want to instantly find certain requirements that I am looking for in prospective candidates. For example, if I am hiring a computer programmer and one of the requirements for the job is to be proficient in a range of computing languages such as C, Java, Perl etc. then I expect to see this on page 1 of the resume. By hiding this important information on page 3 is not beneficial for this job application. Employers can often receive hundreds of resumes for a particular job. The harder you make their life, the greater the chance that your resume will be deleted.
 
<b>Strategic Keywords</b> 
With demand for jobs so competitive, many firms now use software programs as a way of performing “first round interviews”. Using selected keywords will ensure that your resume passes the first stage of selection and will not be deleted before a hiring manager has even had a chance to read your application. The best way to find these keywords is by simply reading the job positions. See what the company is looking for and make sure your resume is full of these keywords!',
getdate(),
null)
go

update resourceAnsweredQuestion
set text = '<p>'+replace(cast(text as varchar(8000)), char(13) + char(10),'</p>'+char(13) + char(10)+'<p>') +'</p>'
where id = 'C531330B-F8ED-4998-BD1A-AF59BFFCD7E4'
go

update resourceAnsweredQuestion
set shortUrl = 'http://bit.ly/QeiXdD'
where id = 'C531330B-F8ED-4998-BD1A-AF59BFFCD7E4'
go
