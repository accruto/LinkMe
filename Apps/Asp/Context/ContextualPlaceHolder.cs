using System.Web.UI;

namespace LinkMe.Apps.Asp.Context
{
    public interface IContextualControl
    {
        object GetContent();
        void SetContent(object content);
    }

    public class ContextualPlaceHolder
        : Control, INamingContainer
    {
        public string Vertical { get; set; }

        public object GetContent()
        {
            foreach (var control in Controls)
            {
                if (control is IContextualControl)
                    return ((IContextualControl) control).GetContent();
            }

            return null;
        }

        public void SetContent(object content)
        {
            foreach (var control in Controls)
            {
                if (control is IContextualControl)
                {
                    ((IContextualControl) control).SetContent(content);
                }
            }
        }
    }
}
