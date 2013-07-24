using System.Web;
using System.Web.UI;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    abstract class FileViewRender
    {
        //protected SortMode sort;
        //protected SortDirection sortDirection;
        protected FileManagerController controller;
        protected FileView fileView;

        protected FileViewRender(FileView fileView)
        {
            this.fileView = fileView;
            this.controller = fileView.Controller;
            //this.sort = fileView.Sort;
            //this.sortDirection = fileView.SortDirection;
        }


        internal virtual void RenderBeginList(HtmlTextWriter output)
        {
        }
        internal virtual void RenderEndList(HtmlTextWriter output)
        {
        }

        internal virtual void RenderBeginGroup(HtmlTextWriter output, GroupInfo group)
        {
            output.AddStyleAttribute("clear", "both");
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            output.Write(HttpUtility.HtmlEncode(group.Name));
            output.RenderEndTag();
        }
        internal virtual void RenderEndGroup(HtmlTextWriter output, GroupInfo group)
        {
        }

        //internal virtual void RenderUpDirectory(HtmlTextWriter output, System.IO.DirectoryInfo dir)
        //{
        //}
        internal virtual void RenderItem(HtmlTextWriter output, FileViewItem item)
        {
        }
        internal static FileViewRender GetRender(FileView fileView)
        {
            switch (fileView.View)
            {
                case FileViewRenderMode.Details:
                    return new FileViewDetailsRender(fileView);
                case FileViewRenderMode.Icons:
                    return new FileViewIconsRender(fileView);
                case FileViewRenderMode.Thumbnails:
                    if (fileView.Controller.SupportThumbnails)
                        return new FileViewThumbnailsRender(fileView);
                    return new FileViewIconsRender(fileView);
                default:
                    return new FileViewDetailsRender(fileView);
            }
        }

        protected void RenderItemName(HtmlTextWriter output, FileViewItem item)
        {
            if (fileView.UseLinkToOpenItem)
            {
                string href = item.IsDirectory ?
                    "javascript:WFM_" + fileView.Controller.ClientID + ".OnExecuteCommand(WFM_" + fileView.ClientID + ",\'0:0\')" :
                    (VirtualPathUtility.AppendTrailingSlash(fileView.CurrentDirectory.VirtualPath) + item.FileSystemInfo.Name);
                if (!item.IsDirectory && !string.IsNullOrEmpty(fileView.LinkToOpenItemTarget))
                    output.AddAttribute(HtmlTextWriterAttribute.Target, fileView.LinkToOpenItemTarget);
                output.AddAttribute(HtmlTextWriterAttribute.Href, href, true);
                output.AddAttribute(HtmlTextWriterAttribute.Class, fileView.LinkToOpenItemClass);
                output.RenderBeginTag(HtmlTextWriterTag.A);
                output.Write(HttpUtility.HtmlEncode(item.Name));
                output.RenderEndTag();
            }
            else
            {
                output.Write(HttpUtility.HtmlEncode(item.Name));
            }
        }
    }

    public enum FileViewRenderMode
    {
        Icons,
        Details,
        Thumbnails
    }

}
