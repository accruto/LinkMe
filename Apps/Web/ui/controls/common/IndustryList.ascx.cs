using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI.WebControls;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility.Validation;

namespace LinkMe.Web.UI.Controls.Common
{
	public partial class IndustryList : EarlyInitControl
	{
        private readonly IIndustriesQuery _industriesQuery = Container.Current.Resolve<IIndustriesQuery>();

		protected IndustryList()
		{
		}

		#region Static methods

		private static int GetIndexOfIndustryId(IList<Industry> industries, Guid id)
		{
            Debug.Assert(industries != null, "industries != null");

			for (int i = 0; i < industries.Count; i++)
			{
				if (industries[i].Id == id)
					return i;
			}

			return -1;
		}

		#endregion

		public bool Required
		{
			get { return reqIndustry.Enabled; }
			set { reqIndustry.Enabled = value; }
		}

        public ListSelectionMode SelectionMode
        {
            get { return lbIndustry.SelectionMode; }
            set { lbIndustry.SelectionMode = value; }
        }

        public int Rows
        {
            get { return lbIndustry.Rows; }
            set { lbIndustry.Rows = value; }
        }

        public string ListBoxClientID
        {
            get { return lbIndustry.ClientID; }
        }

        public string CssClass
        {
            get { return lbIndustry.CssClass; }
            set { lbIndustry.CssClass = value; }
        }

        internal ListBox ListBox
        {
            get { return lbIndustry; }
        }

		public void DisplayAllIndustries()
		{
            DisplayIndustries(_industriesQuery.GetIndustries());
		}

		public void DisplayIndustries(IList<Industry> industries)
		{
            lbIndustry.Items.Clear();

            foreach (Industry industry in industries)
            {
                lbIndustry.Items.Add(new ListItem(industry.Name, industry.Id.ToString()));
            }

            SetInitialised();
		}

        /// <summary>
        /// Gets or sets all the selected industries.
        /// </summary>
		public IList<Industry> SelectedIndustries
		{
			get
			{
                CheckInitialised();

			    List<Guid> selectedIndustryIds = new List<Guid>();

				foreach (ListItem item in lbIndustry.Items)
				{
					if (item.Selected)
					{
						selectedIndustryIds.Add(new Guid(item.Value));
					}
				}

                return _industriesQuery.GetIndustries(selectedIndustryIds);
			}
			set { SetSelectedIndustries(value); }
		}

	    /// <summary>
        /// Gets or sets the selected industry when the selection mode is 'Single'. Throws an exception if
        /// the selection mode is 'Multiple'.
        /// </summary>
        public Industry SelectedIndustry
        {
            get
            {
                CheckInitialised();

                if (SelectionMode != ListSelectionMode.Single)
                {
                    throw new InvalidOperationException("The SelectedIndustry property can only be used when"
                        + " the selection mode is 'Single'.");
                }

                ListItem selected = lbIndustry.SelectedItem;
                if (selected == null)
                    return null;

                return _industriesQuery.GetIndustry(new Guid(selected.Value));
            }
            set
            {
                CheckInitialised();

                if (SelectionMode != ListSelectionMode.Single)
                {
                    throw new InvalidOperationException("The SelectedIndustry property can only be used when"
                        + " the selection mode is 'Single'.");
                }

                lbIndustry.ClearSelection();

                if (value == null)
                    return;

                foreach (ListItem item in lbIndustry.Items)
                {
                    if (new Guid(item.Value) == value.Id)
                    {
                        item.Selected = true;
                        return;
                    }
                }

                if (lbIndustry.Items.Count == 0)
                {
                    throw new InvalidOperationException("Unable to select the '" + value.Name
                        + "' industry, because the industry list has not been initialised with any industries.");
                }
                else
                {
                    throw new InvalidOperationException("Unable to select the '" + value.Name
                        + "' industry, because it is not in the industry list.");
                }
            }
        }

        /// <summary>
        /// A weakly-typed version of set_SelectedIndustries, which is needed because JobAd.Industries
        /// must be weakly-typed.
        /// </summary>
		public void SetSelectedIndustries(IEnumerable value)
		{
            CheckInitialised();

            if (value == null)
            {
                lbIndustry.ClearSelection();
                return;
            }

            List<Industry> toSelect = new List<Industry>();
            foreach (Industry industry in value)
            {
                toSelect.Add(industry);
            }

            if (toSelect.Count == 0)
            {
                lbIndustry.ClearSelection();
                return;
            }

		    foreach (ListItem item in lbIndustry.Items)
			{
                int index = GetIndexOfIndustryId(toSelect, new Guid(item.Value));

                if (index != -1)
                {
                    item.Selected = true;
                    toSelect.RemoveAt(index);
                }
                else
                {
                    item.Selected = false;
                }
			}

            if (toSelect.Count > 0)
            {
                if (lbIndustry.Items.Count == 0)
                {
                    throw new InvalidOperationException("Unable to select industries, because the"
                        + " industry list has not been populated.");
                }
                else
                {
                    throw new InvalidOperationException("Unable to select industries, because "
                        + toSelect.Count + " of them are not in the industry list. The first of these is '"
                        + toSelect[0].Name + "'.");
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            reqIndustry.ErrorMessage = ValidationErrorMessages.REQUIRED_FIELD_REQUEST_INDUSTRY;
        }
    }
}
