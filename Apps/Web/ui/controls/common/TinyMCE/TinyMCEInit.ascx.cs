﻿using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Controls.Common.TinyMCE
{
    public enum TinyMCEMode
    {
        Default,
        SimpleHtml
    }

    public partial class TinyMCEInit
        : LinkMeUserControl
    {
        private TinyMCEMode _mode;

        public TinyMCEMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        protected string GetTinyMCEJavaScriptUrl()
        {
            // I don't know why it has to be relative.

            return JavaScripts.TinyMce.Url.GetRelativeUrl(GetClientUrl());
        }

        protected string GetValidElements()
        {
            switch (_mode)
            {
                case TinyMCEMode.SimpleHtml:
                    return "a[href|title|target=_blank],strong/b,em,div[style],br,ol,ul,li,span[style=text-decoration: underline;],p[style]";

                default:
                    return "@[id|class|style|title|dir<ltr?rtl|lang|xml::lang|onclick|ondblclick|"
                        + "onmousedown|onmouseup|onmouseover|onmousemove|onmouseout|onkeypress|"
                        + "onkeydown|onkeyup],a[alt|rel|rev|charset|hreflang|tabindex|accesskey|type|"
                        + "name|href|target|title|class|onfocus|onblur],strong/b,em/i,strike,u,"
                        + "#p[align],-ol[type|compact],-ul[type|compact],-li,br,img[longdesc|usemap|"
                        + "src|border|alt=|title|hspace|vspace|width|height|align],-sub,-sup,"
                        + "-blockquote,-table[border=0|cellspacing|cellpadding|width|frame|rules|"
                        + "height|align|summary|bgcolor|background|bordercolor],-tr[rowspan|width|"
                        + "height|align|valign|bgcolor|background|bordercolor],tbody,thead,tfoot,"
                        + "#td[colspan|rowspan|width|height|align|valign|bgcolor|background|bordercolor"
                        + "|scope|nowrap],#th[colspan|rowspan|width|height|align|valign|scope],caption,div[align|nowrap],"
                        + "-span,-code,-pre,address,-h1,-h2,-h3,-h4,-h5,-h6,hr[size|noshade],-font[face"
                        + "|size|color],dd,dl,dt,cite,abbr,acronym,del[datetime|cite],ins[datetime|cite],"
                        + "object[classid|width|height|codebase|*],param[name|value|_value],embed[type|width"
                        + "|height|src|*],script[src|type],map[name],area[shape|coords|href|alt|target],bdo,"
                        + "button,col[align|char|charoff|span|valign|width],colgroup[align|char|charoff|span|"
                        + "valign|width],dfn,fieldset,form[action|accept|accept-charset|enctype|method],"
                        + "input[accept|alt|checked|disabled|maxlength|name|readonly|size|src|type|value],"
                        + "kbd,label[for],legend,noscript,optgroup[label|disabled],option[disabled|label|selected|value],"
                        + "q[cite],samp,select[disabled|multiple|name|size],small,"
                        + "textarea[cols|rows|disabled|name|readonly],tt,var,big,"
                        + "meta[name|content],link[href|rel<stylesheet],style";
            }
        }
    }
}