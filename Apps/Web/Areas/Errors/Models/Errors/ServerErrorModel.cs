namespace LinkMe.Web.Areas.Errors.Models.Errors
{
    public class ServerErrorModel
    {
        public bool ShowDetails { get; set; }
        public ErrorReport Report { get; set; }
    }
}