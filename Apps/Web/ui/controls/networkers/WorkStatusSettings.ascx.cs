using System;
using System.Web.UI.WebControls;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Utility;
using LinkMe.Web.Applications.Facade;

namespace LinkMe.Web.UI.Controls.Networkers
{
	public partial class WorkStatusSettings
        : LinkMeUserControl
	{
	    protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            rdoAvailable.Text = NetworkerFacade.GetCandidateStatusText(CandidateStatus.AvailableNow);
            rdoActive.Text = NetworkerFacade.GetCandidateStatusText(CandidateStatus.ActivelyLooking);
            rdoOpen.Text = NetworkerFacade.GetCandidateStatusText(CandidateStatus.OpenToOffers);
            rdoNotLooking.Text = NetworkerFacade.GetCandidateStatusText(CandidateStatus.NotLooking);
        }

        public void Display(Candidate candidate)
        {
            SetSelectedStatus(candidate.Status);
        }

        public void Update(Candidate candidate)
        {
            if (rdoAvailable.Checked)
                candidate.Status = CandidateStatus.AvailableNow;
            else if (rdoActive.Checked)
                candidate.Status = CandidateStatus.ActivelyLooking;
            else if (rdoOpen.Checked)
                candidate.Status = CandidateStatus.OpenToOffers;
            else if (rdoNotLooking.Checked)
                candidate.Status = CandidateStatus.NotLooking;
        }

	    private void SetSelectedStatus(CandidateStatus status)
	    {
	        RadioButton button;

	        switch (status)
	        {
                case CandidateStatus.AvailableNow:
                    button = rdoAvailable;
                    break;

                case CandidateStatus.ActivelyLooking:
	                button = rdoActive;
	                break;

                case CandidateStatus.OpenToOffers:
	                button = rdoOpen;
	                break;

                case CandidateStatus.NotLooking:
	                button = rdoNotLooking;
	                break;

                case CandidateStatus.Unspecified:
	                button = null;
	                break;

                default:
                    throw new LinkMeApplicationException("Local variable 'button' should have been initialised by now.");
	        }

            rdoAvailable.Checked = false;
            rdoActive.Checked = false;
            rdoOpen.Checked = false;
            rdoNotLooking.Checked = false;

            if (button != null)
    	        button.Checked = true;
	    }
	}
}
