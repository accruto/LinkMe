using System;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class JoinToApplySteps : LinkMeUserControl
    {
        public enum ActiveStep
        {
            None,
            One,
            Two
        }

        private ActiveStep _activeStep = ActiveStep.None;

        public ActiveStep Step
        {
            get { return _activeStep; }
            set { _activeStep = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string imgDir = ApplicationPath + "/ui/img/";

            imgStepOne.ImageUrl = imgDir + "one.gif";
            imgStepTwo.ImageUrl = imgDir + "two.gif";

            switch (Step)
            {
                case ActiveStep.One:
                    imgStepOne.ImageUrl = imgDir + "oneOn.gif";
                    phHighlightOne.Visible = true;
                    break;
                case ActiveStep.Two:
                    imgStepTwo.ImageUrl = imgDir + "twoOn.gif";
                    phHighlightTwo.Visible = true;
                    break;
                default:
                    phJoinToApplyStepsWrapper.Visible = false;
                    break;
            }
        }
    }
}