using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    class FileViewDetailsRender : FileViewRender
    {

        internal FileViewDetailsRender(FileView fileView) : base(fileView) { }

        internal override void RenderBeginList(System.Web.UI.HtmlTextWriter output)
        {
            BorderedPanel panel = new BorderedPanel();
            panel.Page = fileView.Page;
            if (fileView.DetailsColumnHeaderStyle.HorizontalAlign == HorizontalAlign.NotSet)
                fileView.DetailsColumnHeaderStyle.HorizontalAlign = fileView.Controller.CurrentUICulture.TextInfo.IsRightToLeft ? HorizontalAlign.Right : HorizontalAlign.Left;
            panel.ControlStyle.CopyFrom(fileView.DetailsColumnHeaderStyle);

            output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            output.RenderBeginTag(HtmlTextWriterTag.Table);
            output.RenderBeginTag(HtmlTextWriterTag.Thead);
            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.RenderBeginTag(HtmlTextWriterTag.Th);

            output.AddAttribute(HtmlTextWriterAttribute.Onclick, fileView.GetSortEventReference(SortMode.Name));
            output.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "default");
            output.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            output.AddAttribute(HtmlTextWriterAttribute.Id, fileView.ClientID + "_Thead_Name");

            panel.RenderBeginTag(output);
            output.Write(HttpUtility.HtmlEncode(controller.GetResourceString("Name", "Name")));
            panel.RenderEndTag(output);

            output.RenderEndTag();
            output.RenderBeginTag(HtmlTextWriterTag.Th);

            output.AddAttribute(HtmlTextWriterAttribute.Onclick, fileView.GetSortEventReference(SortMode.Size));
            output.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "default");
            output.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            output.AddAttribute(HtmlTextWriterAttribute.Id, fileView.ClientID + "_Thead_Size");

            panel.RenderBeginTag(output);
            output.Write(HttpUtility.HtmlEncode(controller.GetResourceString("Size", "Size")));
            panel.RenderEndTag(output);

            output.RenderEndTag();
            output.RenderBeginTag(HtmlTextWriterTag.Th);

            output.AddAttribute(HtmlTextWriterAttribute.Onclick, fileView.GetSortEventReference(SortMode.Type));
            output.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "default");
            output.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            output.AddAttribute(HtmlTextWriterAttribute.Id, fileView.ClientID + "_Thead_Type");

            panel.RenderBeginTag(output);
            output.Write(HttpUtility.HtmlEncode(controller.GetResourceString("Type", "Type")));
            panel.RenderEndTag(output);

            output.RenderEndTag();
            output.RenderBeginTag(HtmlTextWriterTag.Th);

            output.AddAttribute(HtmlTextWriterAttribute.Onclick, fileView.GetSortEventReference(SortMode.Modified));
            output.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "default");
            output.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            output.AddAttribute(HtmlTextWriterAttribute.Id, fileView.ClientID + "_Thead_Modified");

            panel.RenderBeginTag(output);
            output.Write(HttpUtility.HtmlEncode(controller.GetResourceString("Date_Modified", "Date Modified")));
            panel.RenderEndTag(output);

            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();

            output.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
            output.RenderBeginTag(HtmlTextWriterTag.Tbody);

        }
        internal override void RenderEndList(System.Web.UI.HtmlTextWriter output)
        {
            output.RenderEndTag();
            output.RenderEndTag();
        }

        internal override void RenderItem(HtmlTextWriter output, FileViewItem item)
        {
            output.RenderBeginTag(HtmlTextWriterTag.Tr);

            // Name Collumn
            if (fileView.Sort == SortMode.Name)
                fileView.DetailsSortedColumnStyle.AddAttributesToRender(output);
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "6px");
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "6px");
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingBottom, "1px");
            output.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "default");
            output.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            output.RenderBeginTag(HtmlTextWriterTag.Td);

            fileView.RenderItemBeginTag(output, item);

            output.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            output.RenderBeginTag(HtmlTextWriterTag.Table);
            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.RenderBeginTag(HtmlTextWriterTag.Td);

            output.AddStyleAttribute(HtmlTextWriterStyle.Width, FileManagerController.SmallImageWidth.ToString(CultureInfo.InstalledUICulture));
            output.AddStyleAttribute(HtmlTextWriterStyle.Height, FileManagerController.SmallImageHeight.ToString(CultureInfo.InstalledUICulture));
            output.AddAttribute(HtmlTextWriterAttribute.Src, item.SmallImage);
            output.AddAttribute(HtmlTextWriterAttribute.Alt, item.Info);
            output.RenderBeginTag(HtmlTextWriterTag.Img);
            output.RenderEndTag();

            output.RenderEndTag();
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            output.RenderBeginTag(HtmlTextWriterTag.Td);

            output.AddAttribute(HtmlTextWriterAttribute.Id, item.ClientID + "_Name");
            output.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            output.Write("&nbsp;");
            RenderItemName(output, item);
            output.RenderEndTag();

            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();

            fileView.RenderItemEndTag(output);

            output.RenderEndTag();

            // Size Collumn
            if (fileView.Sort == SortMode.Size)
                fileView.DetailsSortedColumnStyle.AddAttributesToRender(output);
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "6px");
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "6px");
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingBottom, "1px");
            output.AddStyleAttribute(HtmlTextWriterStyle.Direction, "ltr");
            output.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            output.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "default");
            output.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            output.Write(item.Size);
            output.RenderEndTag();

            // Type Collumn
            if (fileView.Sort == SortMode.Type)
                fileView.DetailsSortedColumnStyle.AddAttributesToRender(output);
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "6px");
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "6px");
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingBottom, "1px");
            output.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "default");
            output.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            output.Write(HttpUtility.HtmlEncode(item.Type));
            output.RenderEndTag();

            // Modified Collumn
            if (fileView.Sort == SortMode.Modified)
                fileView.DetailsSortedColumnStyle.AddAttributesToRender(output);
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "6px");
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "6px");
            output.AddStyleAttribute(HtmlTextWriterStyle.PaddingBottom, "1px");
            output.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "default");
            output.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            output.Write(HttpUtility.HtmlEncode(item.Modified));
            output.RenderEndTag();


            output.RenderEndTag();
        }

        internal override void RenderBeginGroup(HtmlTextWriter output, GroupInfo group)
        {
            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.AddAttribute(HtmlTextWriterAttribute.Colspan, "4");
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            base.RenderBeginGroup(output, group);
            output.RenderEndTag();
            output.RenderEndTag();
        }

        //internal override void RenderEndGroup(HtmlTextWriter output, GroupInfo group)
        //{
        //    base.RenderEndGroup(output, group);
        //}
    }
}
