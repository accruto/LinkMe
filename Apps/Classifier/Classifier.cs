using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Windows.Forms;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Utility.Sql;

namespace Classifier
{
    public partial class Classifier : Form
    {
        private static readonly IDbConnectionFactory ConnectionFactory = LinkMe.Framework.Utility.Unity.Container.Current.Resolve<IDbConnectionFactory>();

        private readonly IJobAdsQuery _jobAdsQuery = LinkMe.Framework.Utility.Unity.Container.Current.Resolve<IJobAdsQuery>();
        private Guid _currentId;
        private int _ratedCount;
 
        private const string TrainingDataDirectory = "c:\\TrainingData\\";
        private const string GetIdCommandText = "select top 1 c.id from dbo.jobAdClassification c " +
            "where classifiedCount < 3 " +
            "and not exists (select 1 " +
            "from jobAdClassified d " +
            "where classifier = @classifierName " +
            "and d.id = c.id) ";

        private const string MarkClassifiedCommandText = "insert into jobAdClassified values (@id, @classifierName, @classification)";

        private const string UpdateClassifiedCountCommandText = "update jobAdClassification set classifiedCount = classifiedCount + 1 where id = @id";

        private const string GetRatedCountCommandText = "select count(*) from jobAdClassified where classifier = @classifierName";

        public Classifier()
        {
            InitializeComponent();
            _ratedCount = GetRatedCount();
        }

        private void Classifier_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(TrainingDataDirectory))
                Directory.CreateDirectory(TrainingDataDirectory);
 
            MoveToNext();
        }

        public void RewriteClassifications()
        {
            foreach (var directory in new DirectoryInfo(TrainingDataDirectory).GetDirectories())
            {
                foreach (var file in directory.GetFiles())
                {
                    _currentId = new Guid(file.Name.Replace(file.Extension, string.Empty));
                    WriteJobAd(directory.Name);
                }
            }
        }

        private void WriteJobAd(string directory)
        {
            var job = _jobAdsQuery.GetJobAd<JobAd>(_currentId);
            var path = Path.Combine(TrainingDataDirectory, directory);
            var filename =  Path.Combine(path, job.Id + ".txt");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using (var file = new StreamWriter(filename))
            {
                file.WriteLine(job.Title);

                if (job.Description.Salary != null && !job.Description.Salary.IsEmpty)
                file.WriteLine("Salary: {0:C} to {1:C}", job.Description.Salary.LowerBound, job.Description.Salary.UpperBound);

                if (job.Description.BulletPoints != null)
                    foreach (var bulletPoint in job.Description.BulletPoints)
                    {
                        file.WriteLine(bulletPoint);
                    }
                file.WriteLine(job.Description.Content);
            }
        }

        private void MoveToNext()
        {
            _currentId = GetNextId();

            if (_currentId == new Guid())
            {
                //reached the end
                MessageBox.Show("Thanks!", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }

            var job = _jobAdsQuery.GetJobAd<JobAd>(_currentId);

            title.Text = job.Title;
            webContent.DocumentText = WebUtility.HtmlDecode(job.Description.Content);

            if (job.Description.Salary.IsEmpty)
            {
                maxSalary.Text = minSalary.Text = string.Empty;
            }
            else
            {
                minSalary.Text = string.Format("{0:C}", job.Description.Salary.LowerBound);
                maxSalary.Text = string.Format("{0:C}", job.Description.Salary.UpperBound);
            }

            ratedText.Text = string.Format("You've rated {0} jobs", _ratedCount);
        }

        private void one_Click(object sender, EventArgs e)
        {
            WriteJobAd("1");
            MarkJobAdClassified(1);
            MoveToNext();
        }

        private void two_Click(object sender, EventArgs e)
        {
            WriteJobAd("2");
            MarkJobAdClassified(2);
            MoveToNext();
        }

        private void three_Click(object sender, EventArgs e)
        {
            WriteJobAd("3");
            MarkJobAdClassified(3);
            MoveToNext();
        }

        private void four_Click(object sender, EventArgs e)
        {
            WriteJobAd("4");
            MarkJobAdClassified(4);
            MoveToNext();
        }

        private void five_Click(object sender, EventArgs e)
        {
            WriteJobAd("5");
            MarkJobAdClassified(5);
            MoveToNext();
        }

        private void six_Click(object sender, EventArgs e)
        {
            WriteJobAd("6");
            MarkJobAdClassified(6);
            MoveToNext();
        }

        private void seven_Click(object sender, EventArgs e)
        {
            WriteJobAd("7");
            MarkJobAdClassified(7);
            MoveToNext();
        }

        private void eight_Click(object sender, EventArgs e)
        {
            WriteJobAd("8");
            MarkJobAdClassified(8);
            MoveToNext();
        }

        private void nine_Click(object sender, EventArgs e)
        {
            WriteJobAd("9");
            MarkJobAdClassified(9);
            MoveToNext();
        }

        private void ten_Click(object sender, EventArgs e)
        {
            WriteJobAd("10");
            MarkJobAdClassified(10);
            MoveToNext();
        }

        private void unclass_Click(object sender, EventArgs e)
        {
            WriteJobAd("99");
            MarkJobAdClassified(99);
            MoveToNext();
        }

        private static Guid GetNextId()
        {

            var results = new List<Guid>();

            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (var command = DatabaseHelper.CreateTextCommand(connection, GetIdCommandText, 20, null))
                {
                    DatabaseHelper.AddParameter(command, "@classifierName", DbType.AnsiString, Environment.MachineName);
                    using (var rd = command.ExecuteReader())
                        while (rd.Read())
                            results.Add(rd.GetGuid(0));
                }
            }

            if (results.Count > 0)
                return results[0];

            return new Guid();
        }

        private void MarkJobAdClassified(int classification)
        {
            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (var command = DatabaseHelper.CreateTextCommand(connection, MarkClassifiedCommandText, 20, null))
                {
                    DatabaseHelper.AddParameter(command, "@classifierName", DbType.AnsiString, Environment.MachineName);
                    DatabaseHelper.AddParameter(command, "@id", DbType.Guid, _currentId);
                    DatabaseHelper.AddParameter(command, "@classification", DbType.Int16, classification);

                    command.ExecuteNonQuery();
                }

                using (var command = DatabaseHelper.CreateTextCommand(connection, UpdateClassifiedCountCommandText, 20, null))
                {
                    DatabaseHelper.AddParameter(command, "@id", DbType.Guid, _currentId);

                    command.ExecuteNonQuery();
                }
            }

            _ratedCount++;
        }

        private int GetRatedCount()
        {
            var ratedCount = 0;

            using (var connection = ConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (var command = DatabaseHelper.CreateTextCommand(connection, GetRatedCountCommandText, 20, null))
                {
                    DatabaseHelper.AddParameter(command, "@classifierName", DbType.AnsiString, Environment.MachineName);

                    int.TryParse(command.ExecuteScalar().ToString(), out ratedCount);
                }
            }

            return ratedCount;
        }
    }
}
