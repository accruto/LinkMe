using System.Globalization;
using System.Web.UI;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    class FileViewIconsRender : FileViewRender
    {
        internal FileViewIconsRender(FileView fileView) : base(fileView) { }

        internal override void RenderItem(System.Web.UI.HtmlTextWriter output, FileViewItem item)
        {
            output.AddStyleAttribute(HtmlTextWriterStyle.Margin, "2px");
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "70px");
            output.AddStyleAttribute(HtmlTextWriterStyle.Height, "71px");
            output.AddStyleAttribute("float", fileView.Controller.CurrentUICulture.TextInfo.IsRightToLeft ? "right" : "left");
            output.RenderBeginTag(HtmlTextWriterTag.Div);

            fileView.RenderItemBeginTag(output, item);

            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "70px");
            output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            output.RenderBeginTag(HtmlTextWriterTag.Table);

            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "center");
            output.AddStyleAttribute(HtmlTextWriterStyle.VerticalAlign, "middle");
            output.AddStyleAttribute(HtmlTextWriterStyle.Height, "41px");
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            //output.AddStyleAttribute(HtmlTextWriterStyle.Width, "70px");
            //output.RenderBeginTag(HtmlTextWriterTag.Div);
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, FileManagerController.LargeImageWidth.ToString(CultureInfo.InstalledUICulture));
            output.AddStyleAttribute(HtmlTextWriterStyle.Height, FileManagerController.LargeImageHeight.ToString(CultureInfo.InstalledUICulture));
            output.AddAttribute(HtmlTextWriterAttribute.Src, item.LargeImage);
            output.AddAttribute(HtmlTextWriterAttribute.Alt, item.Info);
            output.RenderBeginTag(HtmlTextWriterTag.Img);
            output.RenderEndTag();
            //output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();

            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            output.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "default");
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "70px");
            output.AddStyleAttribute(HtmlTextWriterStyle.Height, "30px");
            output.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "hidden");
            output.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "center");
            output.AddAttribute(HtmlTextWriterAttribute.Id, item.ClientID + "_Name");
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            RenderItemName(output, item);
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();

            output.RenderEndTag();

            fileView.RenderItemEndTag(output);

            output.RenderEndTag();
        }
    }
}
