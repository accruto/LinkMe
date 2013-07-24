using System.Web.UI;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    class FileViewThumbnailsRender : FileViewRender
    {
        internal FileViewThumbnailsRender(FileView fileView) : base(fileView) { }

        internal override void RenderItem(System.Web.UI.HtmlTextWriter output, FileViewItem item)
        {
            output.AddStyleAttribute(HtmlTextWriterStyle.Margin, "1px");
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "120px");
            output.AddStyleAttribute(HtmlTextWriterStyle.Height, "126px");
            output.AddStyleAttribute("float", fileView.Controller.CurrentUICulture.TextInfo.IsRightToLeft ? "right" : "left");
            output.RenderBeginTag(HtmlTextWriterTag.Div);

            fileView.RenderItemBeginTag(output, item);

            output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            output.RenderBeginTag(HtmlTextWriterTag.Table);

            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "120px");
            output.AddStyleAttribute(HtmlTextWriterStyle.Height, "96px");
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "13px");
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "13px");
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingTop, "2px");
            output.RenderBeginTag(HtmlTextWriterTag.Td);

            output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            output.RenderBeginTag(HtmlTextWriterTag.Table);
            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.AddStyleAttribute(HtmlTextWriterStyle.BorderColor, "#ACA899");
            output.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "solid");
            output.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "1px");
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "92px");
            output.AddStyleAttribute(HtmlTextWriterStyle.Height, "92px");
            output.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "center");
            output.AddStyleAttribute(HtmlTextWriterStyle.VerticalAlign, "middle");
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            output.AddAttribute(HtmlTextWriterAttribute.Src, item.ThumbnailImage);
            output.AddAttribute(HtmlTextWriterAttribute.Alt, item.Info);
            output.RenderBeginTag(HtmlTextWriterTag.Img);
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();

            output.RenderEndTag();
            output.RenderEndTag();

            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            output.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "default");
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "120px");
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
