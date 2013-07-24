<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="Javascript.aspx.cs" Inherits="LinkMe.Web.Js.Javascript" Trace="false" MasterPageFile="~/master/BlankMasterPage.master" %>
<%@ Import Namespace="LinkMe.Web.Applications.Ajax"%>
<%@ OutputCache duration="604800" varyByParam="none" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">

var aaaBBBaaa = '<script>';

var NameSpacesRegistrator = {
    RegisterNameSpace : function(ns) {
        var nsParts = ns.split('.');
        var root = window;

        for(var i = 0; i < nsParts.length; i++) {
            if(typeof root[nsParts[i]] == 'undefined') {
                root[nsParts[i]] = new Object();
            }
            root = root[nsParts[i]];
        }
    },
    
    RegisterNameSpaces : function() {
        this.RegisterNameSpace('LinkMeUI.Editor');
        this.RegisterNameSpace('LinkMeUI.Ajax');
        this.RegisterNameSpace('LinkMeUI.Utils');

        LinkMeUI.ApplicationPath = '<%=ApplicationPath%>';
        LinkMeUI.ContentPath = '<%= _contentUrl %>';
        LinkMeUI.IE6CssExpressionAlphaBegin = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='<%=ApplicationPath%>";
        LinkMeUI.IE6CssExpressionAlphaEnd = "', sizingMethod='crop')";
        LinkMeUI.IE6CssExpressionAlphaEndScaled = "', sizingMethod='scale')";
        
        LinkMeUI.IE6Alpha = function(path) {
            return LinkMeUI.IE6CssExpressionAlphaBegin + path + LinkMeUI.IE6CssExpressionAlphaEnd;
        }
        
        LinkMeUI.IE6AlphaScaled = function(path) {
            return LinkMeUI.IE6CssExpressionAlphaBegin + path + LinkMeUI.IE6CssExpressionAlphaEndScaled;
        }
        
        LinkMeUI.IE6MaxWidth = function(maxWidthPx, element) {
            if (element.widthExpressionTriggered == undefined) {
	            element.maxWidthPx = maxWidthPx;
	            element.widthExpressionTriggered = true;
	            element.initialWidth = element.currentStyle.width;
	            return element.initialWidth;
	        }
	        
            if (element.clientWidthDetected == undefined && element.clientWidth > 0) {
                element.clientWidthDetected = true;
                if (element.offsetWidth > element.maxWidthPx) {
                    element.correctedWidth = element.maxWidthPx + "px";
                    return element.correctedWidth;
                }
                return element.initialWidth;
            }
            
            if (element.correctedWidth != undefined)
                return element.correctedWidth;
            
            return element.initialWidth;
        }
        
        LinkMeUI.Ajax.SUCCESS_CODE = <%= (int)AjaxResultCode.SUCCESS %>;
        LinkMeUI.Ajax.EMPTY_CODE = <%= (int)AjaxResultCode.EMPTY %>;
        LinkMeUI.Ajax.FAILURE = <%= (int)AjaxResultCode.FAILURE %>;
        LinkMeUI.Ajax.LOADING_IMAGE_PATH = LinkMeUI.ApplicationPath + '/ui/images/universal/loading3.gif';

        LinkMeUI.Locations = {            
            LOCATE_HELPER : LinkMeUI.ApplicationPath + '/js/LinkMeUI/LocateHelper.js',
            STRING_UTILS: LinkMeUI.ApplicationPath + '/js/LinkMeUI/StringUtils.js',
            SECTIONS_EDITOR : LinkMeUI.ApplicationPath + '/js/LinkMeUI/SectionsEditor.js',
            PROTOTYPE : LinkMeUI.ApplicationPath + '/js/prototype.js',
            SCRIPTACULOUS : LinkMeUI.ApplicationPath + '/js/scriptaculous.js', 
            SCRIPTACULOUS_EFFECTS : LinkMeUI.ApplicationPath + '/js/effects.js',            
            VALIDATION_LIB : LinkMeUI.ApplicationPath + '/js/validation.js',
            OVERLAY_POPUP : LinkMeUI.ApplicationPath + '/js/OverlayPopup.js',
            SCROLL_TRACKER : LinkMeUI.ApplicationPath + '/js/LinkMeUI/ScrollTracker.js',
            LOCATION : LinkMeUI.ApplicationPath + '/js/controls/Location.js',
            DISAPPEARING_LABEL : LinkMeUI.ApplicationPath + '/js/DisappearingLabel.js',
            IMPORT_CONTACTS : LinkMeUI.ApplicationPath + '/js/ImportContacts.js',
            CLEAR_TEXT_BOXES : LinkMeUI.ApplicationPath + '/js/TextBoxClearer.js',
            GROUP_MESSAGE_POPUP : LinkMeUI.ApplicationPath + '/js/controls/GroupMessagePopup.js',
            USER_CONTENT : LinkMeUI.ApplicationPath + '/js/UserContent.js',
            MENU_HIERARCHY_BEHAVIOUR : LinkMeUI.ApplicationPath + '/js/LinkMeUI/behaviours/MenuHierarchyBehaviour.js',
            CHECKBOXES_HIERARCHY_BEHAVIOUR : LinkMeUI.ApplicationPath + '/js/LinkMeUI/behaviours/CheckboxesHierarchyBehaviour.js',
            TOOLTIP_BEHAVIOUR : LinkMeUI.ApplicationPath + '/js/LinkMeUI/behaviours/TooltipBehaviour.js',
            JSON : LinkMeUI.ContentPath + 'js/jQuery/plugins/jquery.json2.js'
        };

        LinkMeUI.JSLoadHelper = {           
            <%--
            /*
                Important comment on browser's behaviour 
                
                AS: Please keep in mind that loading occurs synchronously,
                which means that script, loaded with function LoadJavaScript 
                will be evalueted straight after currently running <script> block.
                
                So you should not put loading of scripts and invocation of 
                newely loaded functions into the same <script> block - 
                it's enough to create just one more block on the page, for example
                
                //First one - loads scripts
                <script type="text/javascript" language="javascript">    
                    LinkMeUI.JSLoadHelper.LoadScriptaculous();    
                    LinkMeUI.JSLoadHelper.LoadLocateHelper();
                < / script>
                //Second one - calls function from newely loaded script
                <script type="text/javascript" language="javascript">    
                    LinkMeUI.LocateHelper.TestFunction();
                < / script>
            */
            --%>
            LoadJavaScriptSafe : function(scriptURL) {
                document.write('<script type="text/javascript" src="' + scriptURL + '">' + '<' + '/script>');
            },

            LoadJavaScript : function(scriptURL) {
                <%-- 
                /*
                    This needs to be here because we've modified scriptaculous to support 
                    that type of loading
                 */
                --%>
                LinkMeUI.JSLoadHelper.CurrentlyLoadedScript = scriptURL;
                
                var scripts = document.getElementsByTagName('script');                
                var found = false;
                for(var i = 0; i < scripts.length; i++) {
                    if(scripts[i].src.toLowerCase().indexOf(scriptURL.toLowerCase()) != -1) {
                        found = true;
                        break;
                    }            
                }                
                
                //Already loaded, do nothing...
                if(found) 
                    return;
                
                LinkMeUI.JSLoadHelper.LoadJavaScriptSafe(scriptURL);
                
            },
            
            LoadLocateHelper : function() {
                this.LoadJavaScript(LinkMeUI.Locations.LOCATE_HELPER);                    
            },
            
            LoadSectionsEditor : function() {
                this.LoadJavaScript(LinkMeUI.Locations.SECTIONS_EDITOR);            
            },

            LoadPrototype : function() {
                this.LoadJavaScript(LinkMeUI.Locations.PROTOTYPE);
            },
            
            LoadScriptaculous : function() {
                this.LoadJavaScript(LinkMeUI.Locations.SCRIPTACULOUS);
            },
            
            LoadScriptaculousEffects : function() {
                this.LoadJavaScript(LinkMeUI.Locations.SCRIPTACULOUS);
            },
            
            LoadValidationLibrary : function() {
                this.LoadJavaScript(LinkMeUI.Locations.VALIDATION_LIB);
            },
            
            LoadMenuHierarchyBehaviour : function() {
                this.LoadJavaScript(LinkMeUI.Locations.MENU_HIERARCHY_BEHAVIOUR);
            },
            
            LoadCheckboxesHierarchyBehaviour : function() {
                this.LoadJavaScript(LinkMeUI.Locations.CHECKBOXES_HIERARCHY_BEHAVIOUR);
            },

            LoadTooltipBehaviour : function() {
                this.LoadJavaScript(LinkMeUI.Locations.TOOLTIP_BEHAVIOUR);
            },

            LoadStringUtils : function() {
                this.LoadJavaScript(LinkMeUI.Locations.STRING_UTILS);
            },

            LoadScrollTracker : function() {
                this.LoadJavaScript(LinkMeUI.Locations.SCROLL_TRACKER);
            },

            LoadOverlayPopup : function() {
                this.LoadJavaScript(LinkMeUI.Locations.OVERLAY_POPUP);
            },
            
            LoadLocation : function() {
                this.LoadJavaScript(LinkMeUI.Locations.LOCATION);
            },

            LoadDisappearingLabel : function() {
                this.LoadJavaScript(LinkMeUI.Locations.DISAPPEARING_LABEL);
            },

            LoadCandidateNotes : function() {
                this.LoadJavaScript(LinkMeUI.Locations.CANDIDATE_NOTES);
            },
            
            LoadImportContacts : function() {
                this.LoadJavaScript(LinkMeUI.Locations.IMPORT_CONTACTS);
            },
            
            LoadTextBoxClearer : function() {
                this.LoadJavaScript(LinkMeUI.Locations.CLEAR_TEXT_BOXES);
            },
            
            LoadGroupMessagePopup : function() {
                this.LoadJavaScript(LinkMeUI.Locations.GROUP_MESSAGE_POPUP);
            },
            
            LoadUserContentHelper : function() {
                this.LoadJavaScript(LinkMeUI.Locations.USER_CONTENT);
            }
        }
    }                
}

NameSpacesRegistrator.RegisterNameSpaces();    

</asp:Content>