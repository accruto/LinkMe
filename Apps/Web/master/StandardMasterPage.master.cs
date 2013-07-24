namespace LinkMe.Web.Master
{
    public partial class StandardMasterPage : LinkMeNestedMasterPage
    {
        public void HideHeader()
        {
            Master.HideHeader();
        }
    }
}
