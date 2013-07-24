using System;
using System.Web.UI.WebControls;
using LinkMe.Domain.Contacts;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class VisibilityCheckBoxRow : LinkMeUserControl
    {
        public string DisplayName = "Unspecified Setting!!";

        public bool AlwaysFirstDegree = false;
        public bool NeverHidden = false;
        public bool NeverFirstDegree = false;
        public bool NeverSecondDegree = false;
        public bool NeverPublic = false;

        public CheckBox chkSecondDegree;
        public CheckBox chkPublic;

        public PersonalVisibility ContactAccessFlag = PersonalVisibility.None;

        public PersonalContactDegree ContactDegree
        {
            get
            {
                if (chkHidden.Checked)
                    return PersonalContactDegree.Self;
                if (chkPublic.Checked)
                    return PersonalContactDegree.Public;
                if (chkSecondDegree.Checked)
                    return PersonalContactDegree.SecondDegree;
                if (chkFirstDegree.Checked)
                    return PersonalContactDegree.FirstDegree;

                return PersonalContactDegree.Self;
            }

            set
            {
                chkHidden.Checked = (value == PersonalContactDegree.Self);
                chkFirstDegree.Checked = (value >= PersonalContactDegree.FirstDegree);
                chkSecondDegree.Checked = (value >= PersonalContactDegree.SecondDegree);
                chkPublic.Checked = (value == PersonalContactDegree.Public);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetVisibleCheckBoxes();
        }
        
        private void SetVisibleCheckBoxes()
        {
            if (NeverPublic)
                chkPublic.Visible = false;
            else
                chkPublic.Attributes["onclick"] = "AdvancedVisibilityCheckboxClick(this);";

            if (NeverHidden)
                chkHidden.Visible = false;
            else
                chkHidden.Attributes["onclick"] = "AdvancedVisibilityCheckboxClick(this);";

            if (NeverSecondDegree)
                chkSecondDegree.Visible = false;
            else
                chkSecondDegree.Attributes["onclick"] = "AdvancedVisibilityCheckboxClick(this);";
    
            if (AlwaysFirstDegree)
            {
                chkHidden.Visible = false;
                chkFirstDegree.Visible = false;
                AlwaysFirstDegreeIndicator.Visible = true;
                AlwaysFirstDegreeIndicator.ImageUrl = ApplicationPath + "/ui/img/green-tick.png";
            }
            else if (NeverFirstDegree)
                chkFirstDegree.Visible = false;
            else
                chkFirstDegree.Attributes["onclick"] = "AdvancedVisibilityCheckboxClick(this);";
        }
    }
}