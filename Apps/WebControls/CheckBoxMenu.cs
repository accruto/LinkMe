using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{
    public class CheckBoxMenuItem
    {
        private readonly string _value;
        private readonly bool _checkboxVisible;
        private readonly CheckBoxMenuItemCollection _childMenuItems = new CheckBoxMenuItemCollection();
        private bool _enabled = true;

        public CheckBoxMenuItem(string text, string value, bool checkboxVisible)
        {
            Text = text;
            _value = value;
            _checkboxVisible = checkboxVisible;
        }

        public CheckBoxMenuItem(string text, string value) : this(text, value, true)
        {}

        public CheckBoxMenuItemCollection ChildMenuItems
        {
            get { return _childMenuItems; }
        }

        public string Value
        {
            get { return _value; }
        }

        public bool CheckboxVisible
        {
            get { return _checkboxVisible; }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public string Text { get; set; }
        public bool Checked { get; set; }
    }

    internal class CheckBoxMenuItemState
    {
        private readonly IList<int> _vector = new List<int>();
        private readonly bool _checked;
        
        public CheckBoxMenuItemState(IList<int> vector, bool isChecked)
        {
            _vector = vector;
            _checked = isChecked;
        }

        public IList<int> Vector
        {
            get { return _vector; }
        }

        public bool Checked
        {
            get { return _checked; }
        }
    }

    internal class CheckBoxPanelItem
    {
        private readonly string _tag;
        private readonly CheckBoxMenuItem _item;

        public CheckBoxPanelItem(string tag, CheckBoxMenuItem item)
        {
            _tag = tag;
            _item = item;
        }

        public string Tag
        {
            get { return _tag; }
        }

        public CheckBoxMenuItem MenuItem
        {
            get { return _item; }
        }
    }

    internal class CheckBoxPanel
    {
        private readonly string _text;
        private readonly string _tag;
        private readonly IList<CheckBoxPanelItem> _items = new List<CheckBoxPanelItem>();

        public CheckBoxPanel(string text, string tag)
        {
            _text = text;
            _tag = tag;
        }

        public IList<CheckBoxPanelItem> PanelItems
        {
            get { return _items; }
        }

        public string Text
        {
            get { return _text; }
        }

        public string Tag
        {
            get { return _tag; }
        }
    }

    public class CheckBoxMenuItemCollection
        : IEnumerable<CheckBoxMenuItem>
    {
        private readonly IList<CheckBoxMenuItem> _menuItems = new List<CheckBoxMenuItem>();

        public void Add(CheckBoxMenuItem menuItem)
        {
            _menuItems.Add(menuItem);
        }

        public int Count
        {
            get { return _menuItems.Count; }
        }

        public CheckBoxMenuItem this[int index]
        {
            get { return _menuItems[index]; }
        }

        IEnumerator<CheckBoxMenuItem> IEnumerable<CheckBoxMenuItem>.GetEnumerator()
        {
            return _menuItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _menuItems.GetEnumerator();
        }
    }

    public class CheckBoxMenu
        : WebControl, IPostBackDataHandler
    {
        private readonly CheckBoxMenuItemCollection _menuItems = new CheckBoxMenuItemCollection();

        public CheckBoxMenuItemCollection MenuItems
        {
            get { return _menuItems; }
        }

        public string GetMenuPanelId(string value)
        {
            return GetMenuPanelId(value, MenuItems, string.Empty);
        }

        private string GetMenuPanelId(string value, CheckBoxMenuItemCollection menuItems, string parentTag)
        {
            for (var index = 0; index < menuItems.Count; ++index)
            {
                var menuItem = menuItems[index];
                var tag = CreateTag(parentTag, index);

                if (menuItem.Value == value)
                    return GetPanelClientId(tag);

                // Iterate.

                var id = GetMenuPanelId(value, menuItems[index].ChildMenuItems, tag);
                if (!string.IsNullOrEmpty(id))
                    return id;
            }

            return null;
        }

        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            // Find all checkboxes that are turned on by looking for matching post data prefixes.

            var keyPrefix = GetCheckboxUniqueId("");
            var menuItemStates = from k in postCollection.AllKeys
                                 where k.StartsWith(keyPrefix) && postCollection[k] == "on"
                                 select CreateCheckBoxMenuItemState(keyPrefix, k, true);

            // Clear the state of every menu item first and then explicitly set the ones that have been selected.

            UncheckAll(_menuItems);
            foreach (var menuItemState in menuItemStates)
                UpdateState(_menuItems, menuItemState);

            return true;
        }

        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            Page.RegisterRequiresPostBack(this);
            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            WriteBeginControl(writer);

            // Need to gather all the menu items into appropriate panels for display.

            var columnPanels = GetColumnPanels();
            for (var column = 0; column < columnPanels.Count; ++column)
                WriteColumn(writer, column, columnPanels[column]);

            WriteEndControl(writer);
        }

        private IList<IList<CheckBoxPanel>> GetColumnPanels()
        {
            var columnPanels = new List<IList<CheckBoxPanel>>();

            // The first column has a single panel consisting of the top level menu items.

            var panel = new CheckBoxPanel("", "");
            GetPanels(columnPanels, 0).Add(panel);
            GetPanelItems(0, panel, _menuItems, columnPanels);

            return columnPanels;
        }

        private static void GetPanelItems(int column, CheckBoxPanel panel, CheckBoxMenuItemCollection menuItems, IList<IList<CheckBoxPanel>> columnPanels)
        {
            for (var index = 0; index < menuItems.Count; ++index)
            {
                // Add a panel item for each menu item.

                var menuItem = menuItems[index];
                var tag = CreateTag(panel.Tag, index);
                panel.PanelItems.Add(new CheckBoxPanelItem(tag, menuItem));

                // If this menu item has children, then create a new panel for it in the next column.

                if (menuItem.ChildMenuItems.Count > 0)
                {
                    var childPanel = new CheckBoxPanel(menuItem.Text, tag);
                    GetPanels(columnPanels, column + 1).Add(childPanel);
                    GetPanelItems(column + 1, childPanel, menuItem.ChildMenuItems, columnPanels);
                }
            }
        }

        private static IList<CheckBoxPanel> GetPanels(IList<IList<CheckBoxPanel>> columnPanels, int column)
        {
            // Find or create new.

            IList<CheckBoxPanel> panels;
            if (columnPanels.Count > column)
            {
                panels = columnPanels[column];
            }
            else
            {
                panels = new List<CheckBoxPanel>();
                columnPanels.Add(panels);
            }

            return panels;
        }

        private static string CreateTag(string parentTag, int index)
        {
            return string.IsNullOrEmpty(parentTag) ? index.ToString() : parentTag + "_" + index;
        }

        private void WriteBeginControl(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("div");
            writer.WriteAttribute("class", "checkboxmenu_ascx forms_v2");
            writer.WriteAttribute("id", ClientID);
            writer.WriteAttribute("name", UniqueID);
            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteLine();
        }

        private static void WriteEndControl(HtmlTextWriter writer)
        {
            writer.WriteEndTag("div");
            writer.WriteLine();
        }

        private void WriteColumn(HtmlTextWriter writer, int column, IEnumerable<CheckBoxPanel> panels)
        {
            WriteBeginColumn(writer, column);
            foreach (var panel in panels)
                WriteColumnPanel(writer, column, panel);
            WriteEndColumn(writer);
        }

        private static void WriteBeginColumn(HtmlTextWriter writer, int column)
        {
            // <div class="column0 column">

            writer.WriteBeginTag("div");
            writer.WriteAttribute("class", "column" + column + " column");
            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteLine();

            // <fieldset>

            writer.WriteBeginTag("fieldset");
            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteLine();
        }

        private static void WriteEndColumn(HtmlTextWriter writer)
        {
            writer.WriteEndTag("fieldset");
            writer.WriteLine();
            writer.WriteEndTag("div");
            writer.WriteLine();
        }

        private void WriteColumnPanel(HtmlTextWriter writer, int column, CheckBoxPanel panel)
        {
            WriteBeginPanel(writer, column, panel);
            for (var index = 0; index < panel.PanelItems.Count; ++index)
                WritePanelItem(writer, column, index, panel.PanelItems[index]);
            WriteEndPanel(writer);
        }

        private void WriteBeginPanel(HtmlTextWriter writer, int column, CheckBoxPanel panel)
        {
            // The single panel in the first column is a little different.

            // <div id="js_panel1" class="checkboxes_field field minilist">

            writer.WriteBeginTag("div");
            if (column != 0)
                writer.WriteAttribute("id", GetPanelClientId(panel.Tag));
            var classText = "checkboxes_field field minilist"
                + (column == 0 ? " js_root-menu-panel" : "");
            writer.WriteAttribute("class", classText);
            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteLine();

            // <div class="minilist-header">
            //   <a class="close_button button js_close-menu" title="Close this sub-menu" href="javascript:void(0);">Close</a>
            //   Education
            // </div>

            if (!string.IsNullOrEmpty(panel.Text))
            {
                writer.WriteBeginTag("div");
                writer.WriteAttribute("class", "minilist-header");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.WriteLine();

                writer.WriteBeginTag("a");
                writer.WriteAttribute("class", "close_button button js_close-menu");
                writer.WriteAttribute("title", "Close this sub-menu");
                writer.WriteAttribute("href", "javascript:void(0);");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.WriteEncodedText("Close");
                writer.WriteEndTag("a");
                writer.WriteLine();

                writer.WriteEncodedText(panel.Text);
                writer.WriteEndTag("div");
                writer.WriteLine();
            }

            // <div id="js_checkboxes1" class="checkboxes_control control js_menu-items">

            writer.WriteBeginTag("div");
            if (column != 0)
                writer.WriteAttribute("id", GetCheckboxesClientId(panel.Tag));
            classText = "checkboxes_control control js_menu-items"
                + (column == 0 ? " js_root-checkboxes" : "");
            writer.WriteAttribute("class", classText);
            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteLine();
        }

        private static void WriteEndPanel(HtmlTextWriter writer)
        {
            writer.WriteEndTag("div");
            writer.WriteLine();
            writer.WriteEndTag("div");
            writer.WriteLine();
        }

        private void WritePanelItem(HtmlTextWriter writer, int column, int index, CheckBoxPanelItem panelItem)
        {
            // <div class="checkbox_control control item_odd item item_has-children js_menu-item" for="js_panel1">

            writer.WriteBeginTag("div");
            var classText = "checkbox_control control"
                + (index%2 == 0 ? " item_odd" : " item_even")
                + " item"
                + (panelItem.MenuItem.ChildMenuItems.Count > 0 ? " item_has-children" : "")
                + " js_menu-item"
                + (!panelItem.MenuItem.Enabled ? " disabled_item" : "");
            writer.WriteAttribute("class", classText);
            if (panelItem.MenuItem.ChildMenuItems.Count > 0)
                writer.WriteAttribute("for", GetPanelClientId(panelItem.Tag));
            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteLine();

            // <input type="checkbox" id="chk1" name="chk1" class="checkbox" for="js_checkboxes1">

            var id = GetCheckboxClientId(panelItem.Tag);
            writer.WriteBeginTag("input");
            writer.WriteAttribute("type", "checkbox");
            writer.WriteAttribute("id", id);
            writer.WriteAttribute("name", GetCheckboxUniqueId(panelItem.Tag));
            writer.WriteAttribute("class", "checkbox");
            if (panelItem.MenuItem.ChildMenuItems.Count > 0)
                writer.WriteAttribute("for", GetCheckboxesClientId(panelItem.Tag));
            if (panelItem.MenuItem.Checked)
                writer.WriteAttribute("checked", "checked");
            if (!panelItem.MenuItem.Enabled)
                writer.WriteAttribute("disabled", "disabled");

            // Checkbox hiding conditions
            // MF (2009-10-11): removed hard-coded condition for "hide first column checkboxes".
            //
            if (!panelItem.MenuItem.CheckboxVisible)  
                writer.WriteAttribute("style", "display:none");

            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteLine();

            // <label for="chk1">Education</label>

            writer.WriteBeginTag("label");
            if (panelItem.MenuItem.ChildMenuItems.Count == 0)
                writer.WriteAttribute("for", id);
            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteEncodedText(panelItem.MenuItem.Text);
            writer.WriteEndTag("label");
            writer.WriteLine();

            // </div>

            writer.WriteEndTag("div");
            writer.WriteLine();
        }

        private static CheckBoxMenuItemState CreateCheckBoxMenuItemState(string checkBoxUniqueIdPrefix, string checkBoxUniqueId, bool isChecked)
        {
            return new CheckBoxMenuItemState(GetVector(checkBoxUniqueIdPrefix, checkBoxUniqueId), isChecked);
        }

        private static IList<int> GetVector(string checkBoxUniqueIdPrefix, string checkBoxUniqueId)
        {
            // Remove the prefix.

            checkBoxUniqueId = checkBoxUniqueId.Substring(checkBoxUniqueIdPrefix.Length);

            // Parse the indices into the menu items.

            var vector = new List<int>();
            while (checkBoxUniqueId.Length > 0)
            {
                var pos = checkBoxUniqueId.IndexOf("_");
                if (pos == -1)
                {
                    if (!GetVectorCoordinate(checkBoxUniqueId, vector))
                        return null;
                    checkBoxUniqueId = string.Empty;
                }
                else
                {
                    if (!GetVectorCoordinate(checkBoxUniqueId.Substring(0, pos), vector))
                        return null;
                    checkBoxUniqueId = checkBoxUniqueId.Substring(pos + 1);
                }
            }

            return vector;
        }

        private static bool GetVectorCoordinate(string coordinate, ICollection<int> vector)
        {
            int value;
            if (int.TryParse(coordinate, out value))
            {
                vector.Add(value);
                return true;
            }

            return false;
        }

        private static void UncheckAll(IEnumerable<CheckBoxMenuItem> menuItems)
        {
            foreach (var menuItem in menuItems)
            {
                menuItem.Checked = false;
                UncheckAll(menuItem.ChildMenuItems);
            }
        }

        private static void UpdateState(CheckBoxMenuItemCollection menuItems, CheckBoxMenuItemState state)
        {
            if (state.Vector.Count > 0)
                UpdateState(menuItems, 0, state);
        }

        private static void UpdateState(CheckBoxMenuItemCollection menuItems, int index, CheckBoxMenuItemState state)
        {
            var coordinate = state.Vector[index];
            if (coordinate >= menuItems.Count)
                return;

            var menuItem = menuItems[coordinate];
            if (index == state.Vector.Count - 1)
            {
                // Reached the end so set it.

                menuItem.Checked = state.Checked;
            }
            else
            {
                // Keep going.

                UpdateState(menuItem.ChildMenuItems, index + 1, state);
            }
        }

        private string GetClientId(string id)
        {
            return ClientID + ClientIDSeparator + id;
        }

        private string GetUniqueId(string id)
        {
            return UniqueID + IdSeparator + id;
        }

        private string GetPanelClientId(string tag)
        {
            return GetClientId("js_panel" + tag);
        }

        private string GetCheckboxesClientId(string tag)
        {
            return GetClientId("js_checkboxes" + tag);
        }

        private string GetCheckboxClientId(string tag)
        {
            return GetClientId("chk" + tag);
        }

        private string GetCheckboxUniqueId(string tag)
        {
            return GetUniqueId("chk" + tag);
        }
    }
}
