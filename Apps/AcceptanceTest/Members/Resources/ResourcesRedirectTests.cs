using System;
using System.Linq;
using LinkMe.AcceptanceTest.Navigation;
using LinkMe.Apps.Presentation;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Resources
{
    [TestClass]
    public class ResourcesRedirectTests
        : RedirectTests
    {
        private readonly IResourcesQuery _resourcesQuery = Resolve<IResourcesQuery>();

        [TestMethod]
        public void TestOldArticleUrls()
        {
            var url = new ReadOnlyApplicationUrl("~/members/resources/articles");
            AssertRedirect(new ReadOnlyApplicationUrl("~/members/resources/articles/all"), url, url);

            url = new ReadOnlyApplicationUrl("~/members/resources/articles/recent");
            AssertRedirect(new ReadOnlyApplicationUrl("~/members/resources/recentlyviewed/article"), url, url);

            var categories = _resourcesQuery.GetCategories();
            const string categorySegment = "job-search";
            var subcategory = categories.SelectMany(c => c.Subcategories).Single(s => s.Name == "Job seeking tips");
            const string subcategorySegment = "job-seeking-tips";

            var article = _resourcesQuery.GetArticles().First(a => a.SubcategoryId == subcategory.Id);
            url = new ReadOnlyApplicationUrl("~/members/resources/articles/" + categorySegment + "-" + subcategorySegment + "-" + GetUrlSegmentTitle(article.Title) + "/" + article.Id);
            AssertRedirect(new ReadOnlyApplicationUrl("~/members/resources/article/" + categorySegment + "-" + subcategorySegment + "-" + GetUrlSegmentTitle(article.Title) + "/" + article.Id), url, url);
        }

        [TestMethod]
        public void TestOldVideoUrls()
        {
            var url = new ReadOnlyApplicationUrl("~/members/resources/videos");
            AssertRedirect(new ReadOnlyApplicationUrl("~/members/resources/videos/all"), url, url);

            url = new ReadOnlyApplicationUrl("~/members/resources/videos/recent");
            AssertRedirect(new ReadOnlyApplicationUrl("~/members/resources/recentlyviewed/video"), url, url);

            var categories = _resourcesQuery.GetCategories();
            const string categorySegment = "job-search";
            var subcategory = categories.SelectMany(c => c.Subcategories).Single(s => s.Name == "Job seeking tips");
            const string subcategorySegment = "job-seeking-tips";

            var video = _resourcesQuery.GetVideos().First(v => v.SubcategoryId == subcategory.Id);
            url = new ReadOnlyApplicationUrl("~/members/resources/videos/" + categorySegment + "-" + subcategorySegment + "-" + GetUrlSegmentTitle(video.Title) + "/" + video.Id);
            AssertRedirect(new ReadOnlyApplicationUrl("~/members/resources/video/" + categorySegment + "-" + subcategorySegment + "-" + GetUrlSegmentTitle(video.Title) + "/" + video.Id), url, url);
        }

        [TestMethod]
        public void TestOldQnAUrls()
        {
            var url = new ReadOnlyApplicationUrl("~/members/resources/qnas");
            AssertRedirect(new ReadOnlyApplicationUrl("~/members/resources/answeredquestions/all"), url, url);

            url = new ReadOnlyApplicationUrl("~/members/resources/qnas/recent");
            AssertRedirect(new ReadOnlyApplicationUrl("~/members/resources/recentlyviewed/answeredquestion"), url, url);

            var categories = _resourcesQuery.GetCategories();
            var category = categories.Single(c => c.Name == "Job search");
            const string categorySegment = "job-search";
            var subcategory = categories.SelectMany(c => c.Subcategories).Single(s => s.Name == "Job seeking tips");
            const string subcategorySegment = "job-seeking-tips";

            url = new ReadOnlyApplicationUrl("~/members/resources/qnas/" + categorySegment, new ReadOnlyQueryString("categoryId", category.Id.ToString()));
            AssertRedirect(new ReadOnlyApplicationUrl("~/members/resources/answeredquestions/" + categorySegment, new ReadOnlyQueryString("categoryId", category.Id.ToString())), url, url);

            url = new ReadOnlyApplicationUrl("~/members/resources/qnas/" + categorySegment + "-" + subcategorySegment, new ReadOnlyQueryString("subcategoryId", subcategory.Id.ToString()));
            AssertRedirect(new ReadOnlyApplicationUrl("~/members/resources/answeredquestions/" + categorySegment + "-" + subcategorySegment, new ReadOnlyQueryString("subcategoryId", subcategory.Id.ToString())), url, url);

            var qna = _resourcesQuery.GetQnAs().First(q => q.SubcategoryId == subcategory.Id);
            url = new ReadOnlyApplicationUrl("~/members/resources/qnas/" + categorySegment + "-" + subcategorySegment + "-" + GetUrlSegmentTitle(qna.Title) + "/" + qna.Id);
            AssertRedirect(new ReadOnlyApplicationUrl("~/members/resources/answeredquestion/" + categorySegment + "-" + subcategorySegment + "-" + GetUrlSegmentTitle(qna.Title) + "/" + qna.Id), url, url);

            // No category id.

            url = new ReadOnlyApplicationUrl("~/members/resources/qnas/" + categorySegment);
            AssertRedirect(new ReadOnlyApplicationUrl("~/members/resources/answeredquestions/" + categorySegment), url, url);
        }

        [TestMethod]
        public void TestVideo()
        {
            var redirectUrl = new ReadOnlyApplicationUrl("~/members/resources");

            // Google has a hold of this old url so redirect it to the new one.

            var url = new ReadOnlyApplicationUrl("~/ui/unregistered/video/Videos.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);

            url = new ReadOnlyApplicationUrl("~/videos/index.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }

        [TestMethod]
        public void TestOldUrls()
        {
            var redirectUrl = new ReadOnlyApplicationUrl("~/members/resources");

            var url = new ReadOnlyApplicationUrl("~/ui/unregistered/ResumeStyleGuide.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);

            url = new ReadOnlyApplicationUrl("~/ui/unregistered/ResumeTips.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);

            url = new ReadOnlyApplicationUrl("~/members/resources/current");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }

        [TestMethod]
        public void TestMedia()
        {
            var url = new ReadOnlyApplicationUrl("~/media/index.aspx");
            AssertRedirect(url, HomeUrl, HomeUrl);

            url = new ReadOnlyApplicationUrl("~/media/media/testimonials/employers/index.aspx");
            AssertRedirect(url, HomeUrl, HomeUrl);
        }

        [TestMethod]
        public void TestCareer()
        {
            var redirectUrl = new ReadOnlyApplicationUrl("~/members/resources");

            var url = new ReadOnlyApplicationUrl("~/career/index.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);

            url = new ReadOnlyApplicationUrl("~/career/findingjob.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);

            url = new ReadOnlyApplicationUrl("~/career/resumes.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);

            url = new ReadOnlyApplicationUrl("~/career/interviews.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }

        [TestMethod]
        public void TestPartners()
        {
            var url = new ReadOnlyApplicationUrl("~/Partners.aspx");
            AssertRedirect(url, HomeUrl, HomeUrl);

            url = new ReadOnlyApplicationUrl("~/ui/unregistered/common/Partners.aspx");
            AssertRedirect(url, HomeUrl, HomeUrl);
        }

        [TestMethod]
        public void TestAffiliates()
        {
            var url = new ReadOnlyApplicationUrl("~/Affiliates.aspx");
            AssertRedirect(url, HomeUrl, HomeUrl);

            url = new ReadOnlyApplicationUrl("~/ui/unregistered/common/Affiliates.aspx");
            AssertRedirect(url, HomeUrl, HomeUrl);
        }

        [TestMethod, Ignore]
        public void TestWelcomePack()
        {
            var url = new ReadOnlyApplicationUrl("~/employers/resources/EmployerWelcomePack.pdf");
            var redirectUrl = new ReadOnlyApplicationUrl("~/resources/employer/search/SearchManual.pdf");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }

        private static string GetUrlSegmentTitle(string title)
        {
            // To avoid extra long segments cut off at end of first statement.

            var fullstop = title.IndexOf('.');
            var question = title.IndexOf('?');
            if (fullstop == -1 && question == -1)
                return title.EncodeUrlSegment();

            var index = fullstop == -1
                ? question
                : question == -1
                    ? fullstop
                    : Math.Min(fullstop, question);

            return title.Substring(0, index).EncodeUrlSegment();
        }
    }
}
