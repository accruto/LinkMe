using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Query.Search;
using LinkMe.Web.Areas.Api.Models.Search;

namespace LinkMe.Web.Areas.Api.Controllers
{
    public class SearchApiController
        : ApiController
    {
        [HttpPost]
        public ActionResult SplitKeywords(string keywords)
        {
            string allKeywords;
            string exactPhrase;
            string anyKeywords;
            string withoutKeywords;
            SearchCriteria.SplitKeywords(false, keywords, out allKeywords, out exactPhrase, out anyKeywords, out withoutKeywords);

            return Json(new SplitKeywordsModel
            {
                Keywords = keywords,
                AllKeywords = allKeywords,
                ExactPhrase = exactPhrase,
                AnyKeywords = anyKeywords,
                WithoutKeywords = withoutKeywords
            });
        }

        [HttpPost]
        public ActionResult CombineKeywords(string allKeywords, string exactPhrase, string anyKeywords, string withoutKeywords)
        {
            var expression = SearchCriteria.CombineKeywords(false, allKeywords, exactPhrase, anyKeywords, withoutKeywords);
            var keywords = expression == null ? null : expression.GetUserExpression();

            return Json(new SplitKeywordsModel
            {
                Keywords = keywords,
                AllKeywords = allKeywords,
                ExactPhrase = exactPhrase,
                AnyKeywords = anyKeywords,
                WithoutKeywords = withoutKeywords
            });
        }
    }
}
