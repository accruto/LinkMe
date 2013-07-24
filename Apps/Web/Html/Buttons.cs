using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Apps.Asp.Mvc.Html;
using LinkMe.Apps.Asp.Routing;

namespace LinkMe.Web.Html
{
    public class PreviewRouteButton
        : Button
    {
        private readonly RouteReference _route;
        private readonly object _routeValues;

        public PreviewRouteButton(RouteReference route, object routeValues)
        {
            _route = route;
            _routeValues = routeValues;
        }

        public override string GetHtml(HtmlHelper html)
        {
            return html.PreviewButton(_route, _routeValues, "Preview", "preview_button button");
        }
    }

    public class ClearButton
        : Button
    {
        private readonly string _fieldName;

        public ClearButton(string fieldName)
        {
            _fieldName = fieldName;
        }

        public override string GetHtml(HtmlHelper html)
        {
            return html.ClearButton("Clear", "clear_button button", _fieldName);
        }
    }

    public class SaveButton
        : SubmitButton
    {
        public SaveButton(string name)
            : base(name, "Save", "save-button button")
        {
        }

        public SaveButton()
            : this("save")
        {
        }
    }

    public class VerifyButton
        : SubmitButton
    {
        public VerifyButton(string name)
            : base(name, "Verify", "verify-button button")
        {
        }

        public VerifyButton()
            : this("verify")
        {
        }
    }

    public class PreviewButton
        : SubmitButton
    {
        public PreviewButton(string name)
            : base(name, "Preview", "preview-button button")
        {
        }

        public PreviewButton()
            : this("preview")
        {
        }
    }

    public class PurchaseButton
        : SubmitButton
    {
        public PurchaseButton(string name)
            : base(name, "Purchase", "purchase-button button")
        {
        }

        public PurchaseButton()
            : this("purchase")
        {
        }
    }

    public class PublishButton
        : SubmitButton
    {
        public PublishButton(string name)
            : base(name, "Publish", "publish-button button")
        {
        }

        public PublishButton()
            : this("publish")
        {
        }
    }

    public class ReopenButton
        : SubmitButton
    {
        public ReopenButton(string name)
            : base(name, "Reopen", "reopen-button button")
        {
        }

        public ReopenButton()
            : this("reopen")
        {
        }
    }

    public class RepostButton
        : SubmitButton
    {
        public RepostButton(string name)
            : base(name, "Repost", "repost-button button")
        {
        }

        public RepostButton()
            : this("repost")
        {
        }
    }

    public class EditButton
        : SubmitButton
    {
        public EditButton(string name)
            : base(name, "Edit", "edit-button button")
        {
        }

        public EditButton()
            : this("edit")
        {
        }
    }

    public class AddButton
        : SubmitButton
    {
        public AddButton()
            : base("add", "Add", "add-button button")
        {
        }
    }

    public class CreateButton
        : SubmitButton
    {
        public CreateButton()
            : base("create", "Create", "create-button button")
        {
        }
    }

    public class CancelButton
        : SubmitButton
    {
        public CancelButton(string name)
            : base(name, "Cancel", "cancel-button button")
        {
        }

        public CancelButton()
            : this("cancel")
        {
        }
    }

    public class DoneButton
        : SubmitButton
    {
        public DoneButton(string name)
            : base(name, "Done", "done-button button")
        {
        }

        public DoneButton()
            : this("done")
        {
        }
    }

    public class BackButton
        : SubmitButton
    {
        public BackButton(string name)
            : base(name, "Back", "back-button button")
        {
        }

        public BackButton()
            : this("back")
        {
        }
    }

    public class SearchButton
        : SubmitButton
    {
        public SearchButton()
            : base("search", "Search", "search_button button")
        {
        }
    }

    public class GoButton
        : Button
    {
        private readonly string _fieldName;

        public GoButton(string fieldName)
        {
            _fieldName = fieldName;
        }

        public override string GetHtml(HtmlHelper html)
        {
            return html.GoButton("go", _fieldName, "Go", "go_button button");
        }
    }

    public class NewSearchButton
        : SubmitButton
    {
        public NewSearchButton()
            : base("search", "Search", "new_search_button button")
        {
        }
    }

    public class NextButton
        : SubmitButton
    {
        public NextButton()
            : base("next", "Next", "next_button button")
        {
        }
    }

    public class PreviousButton
        : SubmitButton
    {
        public PreviousButton()
            : base("previous", "Previous", "previous_button button")
        {
        }
    }

    public class PreviousAsBackButton
        : SubmitButton
    {
        public PreviousAsBackButton()
            : base("previous", "Back", "back_button button")
        {
        }
    }

    public class AcceptButton
        : SubmitButton
    {
        public AcceptButton()
            : base("accept", "Accept", "accept_button button")
        {
        }
    }

    public class SendButton
        : SubmitButton
    {
        public SendButton()
            : base("send", "", "send-button button")
        {
        }
    }

    public class UnsubscribeButton
        : SubmitButton
    {
        public UnsubscribeButton()
            : base("Unsubscribe", "", "unsubscribe-button button")
        {
        }
    }

    public class ApplyButton
        : SubmitButton
    {
        public ApplyButton()
            : base("apply", "Apply", "apply-button button")
        {
        }
    }

    public class ApplyNowButton
        : SubmitButton
    {
        public ApplyNowButton()
            : base("applynow", "Apply Now", "apply-now_button button")
        {
        }
    }

    public class DownloadButton
        : SubmitButton
    {
        public DownloadButton()
            : base("download", "Download", "download-button button")
        {
        }
    }

    public class DownloadPdfButton
        : SubmitButton
    {
        public DownloadPdfButton()
            : base("downloadpdf", "DownloadPdf", "download-pdf-button button")
        {
        }
    }

    public class LoginButton
        : SubmitButton
    {
        public LoginButton()
            : base("login", "Log in", "login-button button")
        {
        }
    }

    public class JoinButton
        : SubmitButton
    {
        public JoinButton()
            : base("join", "Join", "join-button button")
        {
        }
    }

    public class DisableButton
        : SubmitButton
    {
        public DisableButton(string name)
            : base(name, "Disable", "disable-button button")
        {
        }

        public DisableButton()
            : this("disable")
        {
        }
    }

    public class EnableButton
        : SubmitButton
    {
        public EnableButton(string name)
            : base(name, "Enable", "enable-button button")
        {
        }

        public EnableButton()
            : this("enable")
        {
        }
    }

    public class ActivateButton
        : SubmitButton
    {
        public ActivateButton()
            : base("activate", "Activate", "activate-button button")
        {
        }
    }

    public class DeactivateButton
        : SubmitButton
    {
        public DeactivateButton()
            : base("deactivate", "Deactivate", "deactivate-button button")
        {
        }
    }

    public class ChangeButton
        : SubmitButton
    {
        public ChangeButton(string name)
            : base(name, "Change", "change-button button")
        {
        }

        public ChangeButton()
            : this("change")
        {
        }
    }
}
