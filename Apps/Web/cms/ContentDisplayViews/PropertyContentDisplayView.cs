using System.Reflection;
using LinkMe.Framework.Content;
using LinkMe.Web.Cms.ContentDisplayers;

namespace LinkMe.Web.Cms.ContentDisplayViews
{
    public class PropertyContentDisplayView
        : ContentDisplayView
    {
        private string _childName;

        public string ChildName
        {
            get { return _childName; }
            set { _childName = value; }
        }

        protected override IContentDisplayer GetContentDisplayer()
        {
            if (string.IsNullOrEmpty(_childName))
                return null;

            // The content to display will be a child of the containing content so get that.

            var parentItem = GetParentContentItem();
            if (parentItem == null)
                return null;

            // Get the displayer.

            return GetContentDisplayer(parentItem, _childName);
        }

        private static IContentDisplayer GetContentDisplayer(ContentItem parentItem, string propertyName)
        {
            // The property on the parent can define how to display the child.

            PropertyInfo propertyInfo = parentItem.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                IContentDisplayer displayer = GetContentDisplayer(parentItem, propertyName, propertyInfo);
                if (displayer != null)
                    return displayer;
            }

            // Look for a property on the parent.

            var item = parentItem.Children[propertyName];
            return CreateDisplayer(item);
        }

        private static IContentDisplayer GetContentDisplayer(ContentItem parentItem, string propertyName, PropertyInfo propertyInfo)
        {
            // The parent defines how to display the child through an attribute.

//            IDisplayable[] attributes = (IDisplayable[])propertyInfo.GetCustomAttributes(typeof(IDisplayable), false);
  //          if (attributes.Length == 0)
    //            return null;
      //      else 
        //        return new DisplayableContentDisplayer(attributes[0], parentItem, propertyName);

            return null;
        }
    }
}
