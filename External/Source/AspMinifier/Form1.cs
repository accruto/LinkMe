using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace AspMinifier
{
    public partial class Form1 : Form
    {
        private int _saved = 0;
        public Form1()
        {
            InitializeComponent();
            txt_folder.Text = Application.StartupPath;
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = txt_folder.Text;
            folderBrowserDialog.ShowDialog();

            txt_folder.Text = folderBrowserDialog.SelectedPath;
        }

        private void btn_minify_Click(object sender, EventArgs e)
        {
            if (txt_folder.Text.Length < 10)
            {
                if (MessageBox.Show("Are you sure? Looks dangerous", "Danger Danger", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
            }

            btn_minify.Enabled = false;
            btn_browse.Enabled = false;
            txt_folder.Enabled = false;
            chk_backup.Enabled = false;

//            execute_compression("*.js");
//            execute_compression("*.css");
            execute_compression("*.aspx", "*.master", "*.ascx", "*.html", "*.htm");

            lbl_status.Text = "Done";
            MessageBox.Show("Done! Saved " + (_saved.ToString("0,0")) + " bytes");
        }

        void execute_compression(params string[] extensions)
        {
            List<String> files = new List<string>();

            lbl_status.Text = "Collecting all " + String.Join(" ", extensions) + " files";
            Application.DoEvents();

            foreach (string ext in extensions)
                Utilities.getFiles(txt_folder.Text, files, ext);

            lbl_status.Text = "Minifying " + files.Count + " " + String.Join(" ", extensions) + " files";
            Application.DoEvents();

            progress.Value = 0;
            progress.Maximum = files.Count;

            Minifier mini = new Minifier() { Backup = chk_backup.Checked };
            foreach (string file in files)
            {
                _saved += mini.Minify(file);
                progress.Value = progress.Value + 1;
                Application.DoEvents();
            }
        }


    }
}
