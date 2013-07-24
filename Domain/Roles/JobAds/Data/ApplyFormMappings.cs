using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Roles.JobAds.Data
{
    // For historical reasons the apply form is a JobG8 specific format.  Maintain this for now.

    [Serializable]
    [XmlType(AnonymousType = true)]
    public enum QuestionType
    {
        TEXTBOX,
        TEXTAREA,
        COMBOBOX,
        LISTBOX,
        CHECKBOX,
        RADIOBUTTON,
        FILEUPLOAD,
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class QuestionLanguage
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string Name;
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string Validation;
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string ValidationMessage;
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string Text;
        [XmlAttribute(DataType = "integer")]
        public string Locale;
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class QuestionAnswerLanguage
    {
        [XmlAttribute(DataType = "integer")]
        public string Locale;
        [XmlText]
        public string Value;
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class QuestionAnswer
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string Text;

        [XmlArray(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem("Language", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public QuestionAnswerLanguage[] Languages;

        [XmlAttribute]
        public short ID;
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class Question
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public bool Required;
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string Name;
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string Text;
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public QuestionType Type;
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public short Size;
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public short Width;
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public short Rows;
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string Validation;
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string ValidationMessage;

        [XmlArray(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem("Language", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public QuestionLanguage[] Languages;

        [XmlArray(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem("Answer", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public QuestionAnswer[] Answers;

        [XmlAttribute]
        public string ID;
        [XmlAttribute]
        public int FormatID;
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "Application")]
    public class ApplicationForm
    {
        [XmlElement("Question", Form = XmlSchemaForm.Unqualified)]
        public Question[] Questions;
        [XmlAttribute]
        public string JobReference;
        [XmlAttribute]
        public int JobBoardID;
        [XmlAttribute]
        public string ResponseURI;
    }

    internal static class ApplyFormMappings
    {
        private static readonly string[] KnownQuestionIDs = { "2", "3", "11", "CV", "CoverLetter" };

        public static void MapTo(this IHaveApplicationRequirementsEntity entity, JobAdIntegration integration)
        {
            integration.ApplicationRequirements = null;
            if (entity == null || string.IsNullOrEmpty(entity.jobg8ApplyForm))
                return;

            var serializer = new XmlSerializer(typeof(ApplicationForm));
            var form = (ApplicationForm)serializer.Deserialize(new StringReader(entity.jobg8ApplyForm));

            // The form contains the response url in this case so set it now.

            integration.ExternalApplyApiUrl = form.ResponseURI;
            integration.JobBoardId = form.JobBoardID.ToString();

            integration.ApplicationRequirements = new ApplicationRequirements
            {
                IncludeResume = false,
                IncludeCoverLetter = false,
                Questions = null,
            };

            // Questions.

            if (form.Questions != null && form.Questions.Length > 0)
            {
                if (form.Questions.Select(q => q.ID).Contains("CV"))
                    integration.ApplicationRequirements.IncludeResume = true;
                if (form.Questions.Select(q => q.ID).Contains("CoverLetter"))
                    integration.ApplicationRequirements.IncludeCoverLetter = true;

                var specificQuestions = form.Questions.Where(q => !KnownQuestionIDs.Contains(q.ID));
                if (specificQuestions.Count() > 0)
                    integration.ApplicationRequirements.Questions = (from q in specificQuestions select q.Map()).ToList();
            }
        }

        private static ApplicationQuestion Map(this Question question)
        {
            if (question.Type == QuestionType.COMBOBOX)
            {
                return new MultipleChoiceQuestion
                {
                    Id = question.ID,
                    FormatId = question.FormatID.ToString(),
                    IsRequired = question.Required,
                    Text = question.Text,
                    Answers = question.Answers.Map(),
                };
            }

            return new TextQuestion
            {
                Id = question.ID,
                FormatId = question.FormatID.ToString(),
                IsRequired = question.Required,
                Text = question.Text,
            };
        }

        private static IList<MultipleChoiceQuestionAnswer> Map(this IEnumerable<QuestionAnswer> answers)
        {
            return answers.IsNullOrEmpty()
                ? null
                : (from a in answers select a.Map()).ToList();
        }

        private static MultipleChoiceQuestionAnswer Map(this QuestionAnswer answer)
        {
            return new MultipleChoiceQuestionAnswer
            {
                Value = answer.ID.ToString(),
                Text = answer.Text,
            };
        }
    }
}
