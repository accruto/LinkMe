using System;
using System.Linq;
using LinkMe.Domain.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Resources
{
    [TestClass]
    public class QnAsTests
        : ResourcesTests
    {
        private const string Category1 = "Resume writing";
        private const string Subcategory1 = "Resume writing tips";
        private const string Title1 = "Can you use the same resume for every job application?";
        private const string Text1 = @"<p>Every job you apply for will be different and as such you need to adapt your resume accordingly. Writing the perfect resume will be different depending on the job and each resume needs to be tailored and specific towards that position. </p>  <p></p>  <p>No matter what position you are applying for the more targeted your resume the greater your chances of finding success. With competition for jobs so intense in order to stand out from the crowd you need to ensure that your skills tick all the boxes that the hiring manger is looking for in the perfect candidate.</p>  <p></p>  <p>Don&#39;t be alarmed at the prospect of having to design a new resume for every job application. There are certain parts of the resume that will always stay the same however there are certain parts that need to be adapted. A powerful, eye-catching qualifications summary at the top of your resume is the best way to attract a hiring manager&#39;s eye and let them immediately of your skills and experience. The other main area of your resume to change is keywords. Many times recruiters or hiring managers will often skim over the resume looking for keywords or use software programs to find key words.  These key words can be changed depending on the job you are applying for. </p>  <p></p>  <p>Remember one thing - the aim of the resume is to attract the reader&#39;s attention. By targeting your resume you will give yourself the greatest opportunity of finding success.</p>";
        private const string ShortUrl1 = null;

        private const string Category2 = "Australian job market";
        private const string Subcategory2 = "Australian resume writing";
        private const string Title2 = "I&#39;ve arrived in Australia and applied for 50+ jobs with no luck. How can I make my CV stand out?";
        private const string Text2 = @"<p>In order to succeed in the Australian job market your Australian resume needs to be written, structured, formatted and presented in the correct way. With competition for jobs in Australia at such a high demand, and in order to stand out from the competition, you need an Australian resume that adequately highlights your skills, achievements and value that you can bring to your next role. Below are my 3 most important tips for making your resume stand out.</p>  <p> </p>  <p><b>Include a Qualifications Profile</b></p>  <p>Remember the number 1 rule about resume writing - highlight your achievements. Your resume is a marketing document and as such you need to emphasise to the reader your skills and expertise. Many times, job candidates will begin their resume with an objective statement where they describe to the reader the type of job that they are interested in. Unfortunately, a hiring manager is not interested in the types of positions you wish to apply for, but they are interested in knowing that you are passionate about the job position that they are advertising for. By replacing an objective statement with a qualifications profile will ensure that your resume begins with a powerful profile that highlights your value-added skills and qualifications.</p>  <p> </p>  <p><b>Strategic Keywords</b></p>  <p>The use of Strategic keywords is essential for a modern day resume. When applying for a certain position, your resume may be one of a thousand that the company will receive. Rather than reading through every single resume, companies now use software programs as a way of performing the first round selection. By using strategic keywords you will not only make it through the first round of selection but you will give yourself every opportunity of standing out above your competition.</p>  <p></p>  <p><b>Demonstrate Flexibility, Adaptability and Innovation</b></p>  <p>Although you may not have local experience do not despair! In my experience employers want to hire job seekers who can demonstrate flexibility, adaptability and innovation as well as a passion for wanting to gain knowledge and experience. Think about extra-curricular activities, work experience, community service or volunteer work that you have performed and exhibited skills of flexibility, adaptability and innovation. Remember that an employer wants to know that you are going to bring value to the organisation. Use your resume to demonstrate that you are a forward-thinking self-starter with vision and desire to implement innovative solutions to any problem that may arise.</p>";
        private const string ShortUrl2 = "http://bit.ly/LpLi0i";

        [TestMethod]
        public void TestQnAs()
        {
            var categories = _resourcesQuery.GetCategories();
            var qnas = (from q in _resourcesQuery.GetQnAs() orderby q.CreatedTime select q).ToList();
            Assert.AreEqual(11, qnas.Count);

            var qna1 = qnas[4];
            var subcategory1 = categories.Single(c => c.Name == Category1).Subcategories.Single(s => s.Name == Subcategory1);
            AssertQnA(Title1, Text1, subcategory1.Id, ShortUrl1, qna1);
            AssertQnA(Title1, Text1, subcategory1.Id, ShortUrl1, _resourcesQuery.GetQnA(qna1.Id));

            var qna2 = qnas[7];
            var subcategory2 = categories.Single(c => c.Name == Category2).Subcategories.Single(s => s.Name == Subcategory2);
            AssertQnA(Title2, Text2, subcategory2.Id, ShortUrl2, qna2);
            AssertQnA(Title2, Text2, subcategory2.Id, ShortUrl2, _resourcesQuery.GetQnA(qna2.Id));

            qnas = _resourcesQuery.GetQnAs(new[] { qna1.Id, qna2.Id }).ToList();
            Assert.AreEqual(2, qnas.Count);
            AssertQnA(Title1, Text1, subcategory1.Id, ShortUrl1, (from a in qnas where a.Id == qna1.Id select a).Single());
            AssertQnA(Title2, Text2, subcategory2.Id, ShortUrl2, (from a in qnas where a.Id == qna2.Id select a).Single());
        }

        private static void AssertQnA(string title, string text, Guid subcategoryId, string shortUrl, Resource qna)
        {
            Assert.AreEqual(title, qna.Title);
            Assert.AreEqual(text, qna.Text);
            Assert.AreEqual(subcategoryId, qna.SubcategoryId);
            Assert.AreEqual(shortUrl, qna.ShortUrl);
        }
    }
}
