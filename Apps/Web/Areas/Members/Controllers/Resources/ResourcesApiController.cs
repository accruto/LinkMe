using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources;
using LinkMe.Domain.Resources.Commands;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Web.Areas.Members.Models.Resources;

namespace LinkMe.Web.Areas.Members.Controllers.Resources
{
    public class ResourcesApiController
        : MembersApiController
    {
        private readonly IResourcesCommand _resourcesCommand;
        private readonly IResourcesQuery _resourcesQuery;
        private readonly IPollsCommand _pollsCommand;
        private readonly IPollsQuery _pollsQuery;

        private string ControllerNameOverride { get; set; }

        public ResourcesApiController(IResourcesCommand resourcesCommand, IResourcesQuery resourcesQuery, IPollsCommand pollsCommand, IPollsQuery pollsQuery)
        {
            _resourcesCommand = resourcesCommand;
            _resourcesQuery = resourcesQuery;
            _pollsCommand = pollsCommand;
            _pollsQuery = pollsQuery;
            ControllerNameOverride = "Resources";
        }

        [HttpPost, ApiEnsureAuthorized(UserType.Member)]
        public ActionResult RateArticle(Guid id, byte rating)
        {
            try
            {
                _resourcesCommand.RateArticle(CurrentMember.Id, id, rating);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult ViewArticle(Guid id)
        {
            try
            {
                var article = _resourcesQuery.GetArticle(id);
                if (article == null)
                    return JsonNotFound("article");

                var userId = CurrentMember == null ? CurrentAnonymousUser.Id : CurrentMember.Id;
                _resourcesCommand.ViewArticle(userId, article.Id);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult ViewVideo(Guid id)
        {
            try
            {
                var video = _resourcesQuery.GetVideo(id);
                if (video == null)
                    return JsonNotFound("video");

                var userId = CurrentMember == null ? CurrentAnonymousUser.Id : CurrentMember.Id;
                _resourcesCommand.ViewVideo(userId, video.Id);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult ViewAnsweredQuestion(Guid id)
        {
            try
            {
                var qna = _resourcesQuery.GetQnA(id);
                if (qna == null)
                    return JsonNotFound("QnA");
                
                var userId = CurrentMember == null ? CurrentAnonymousUser.Id : CurrentMember.Id;
                _resourcesCommand.ViewQnA(userId, qna.Id);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        public ActionResult YouTubeVideoDetail(string externalVideoId)
        {
            try
            {
                var request = WebRequest.Create(string.Format("https://gdata.youtube.com/feeds/api/videos/{0}?v=2&alt=jsonc", externalVideoId));
                request.Timeout = 500;      //short timeout so as not to hold up execution
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();

                using (var reader = new StreamReader(responseStream))
                {
                    var contents = reader.ReadToEnd();

                    return Json(contents, JsonRequestBehavior.AllowGet);
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel{Success = false});
        }

        [HttpPost]
        public ActionResult Vote(byte voteId)
        {
            try
            {
                var poll = _pollsQuery.GetActivePoll();
                if (poll != null)
                {
                    if (voteId < poll.Answers.Count)
                    {
                        var userId = CurrentMember == null ? CurrentAnonymousUser.Id : CurrentMember.Id;
                        _pollsCommand.CreatePollAnswerVote(new PollAnswerVote { AnswerId = poll.Answers[voteId].Id, UserId = userId });
                    }
                }

                return PartialView("ActivePoll", new PollModel { Poll = poll, Votes = poll != null ? _pollsQuery.GetPollAnswerVotes(poll.Id) : null });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost, ApiEnsureAuthorized(UserType.Member)]
        public ActionResult AskQuestion(Guid? categoryId, string questionText)
        {
            try
            {
                var question = new Question
                                   {
                                       CategoryId = categoryId,
                                       Text = questionText,
                                       AskerId = CurrentMember.Id,
                                   };

                _resourcesCommand.CreateQuestion(question);

            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        private new PartialViewResult PartialView(string viewName, object model)
        {
            SetControllerName();
            return base.PartialView(viewName, model);
        }

        private void SetControllerName()
        {
            if (!string.IsNullOrEmpty(ControllerNameOverride))
                ControllerContext.RouteData.Values["Controller"] = ControllerNameOverride;
        }
    }
}
