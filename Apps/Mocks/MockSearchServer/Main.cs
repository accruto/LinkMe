using System;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Apps.Mocks.Services.JobG8;
using LinkMe.Framework.Tools.Controls;
using Form=System.Windows.Forms.Form;

namespace LinkMe.Apps.MockSearchServer
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            chkSynchroniseIndex.Checked = true;
            chkRebuildIndex.Checked = true;

            Start();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Start()
        {
            try
            {
                MemberSearchHost.Start(chkSynchroniseIndex.Checked, chkRebuildIndex.Checked);
                JobAdSearchHost.Start(chkSynchroniseIndex.Checked, chkRebuildIndex.Checked);
                JobAdSortHost.Start(chkSynchroniseIndex.Checked, chkRebuildIndex.Checked);
                ResourceSearchHost.Start(chkSynchroniseIndex.Checked, chkRebuildIndex.Checked);
                JobG8Host.Start();

                chkSynchroniseIndex.Enabled = false;
                chkRebuildIndex.Enabled = false;
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            catch (Exception ex)
            {
                new ExceptionDialog(ex, "Exception").ShowDialog();
            }
        }

        private void Stop()
        {
            try
            {
                MemberSearchHost.Stop();
                JobAdSearchHost.Stop();
                JobAdSortHost.Stop();
                ResourceSearchHost.Stop();

                chkSynchroniseIndex.Enabled = true;
                chkRebuildIndex.Enabled = true;
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
            catch (Exception ex)
            {
                new ExceptionDialog(ex, "Exception").ShowDialog();
            }
        }
    }
}