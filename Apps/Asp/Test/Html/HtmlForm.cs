using System.Collections.Specialized;

namespace LinkMe.Apps.Asp.Test.Html
{
    public class HtmlForm
    {
        private readonly string _id;
        private readonly string _method;
        private readonly string _action;
        private readonly NameValueCollection _formVariables = new NameValueCollection();
        private readonly NameValueCollection _fileVariables = new NameValueCollection();

        public HtmlForm(string id, string method, string action)
        {
            _id = id;
            _method = method;
            _action = action;
        }

        public string Id
        {
            get { return _id; }
        }

        public string Method
        {
            get { return _method; }
        }

        public string Action
        {
            get { return _action; }
        }

        public NameValueCollection FormVariables
        {
            get { return _formVariables; }
        }

        public NameValueCollection FileVariables
        {
            get { return _fileVariables; }
        }

        public void SetFormVariable(string name, string value, bool isFile)
        {
            if (isFile)
                _fileVariables[name] = value;
            else
                _formVariables[name] = value;
        }

        public void AddFormVariable(string name, string value, bool isFile)
        {
            if (isFile)
                _fileVariables.Add(name, value);
            else
                _formVariables.Add(name, value);
        }

        public void ClearFormVariable(string name)
        {
            _formVariables.Remove(name);
        }
    }
}
