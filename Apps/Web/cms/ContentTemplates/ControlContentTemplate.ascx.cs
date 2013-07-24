using LinkMe.Framework.Content.ContentItems;
using LinkMe.Framework.Content.UI;

namespace LinkMe.Web.Cms.ContentTemplates
{
    public partial class ControlContentTemplate
        : UserControl<ControlContentItem>
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

//            var control = LoadControl(Item.ControlPath);
  //          Controls.Add(control);
        }
    }
}