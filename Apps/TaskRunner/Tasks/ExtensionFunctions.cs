using System;
using System.Collections.Generic;

namespace LinkMe.TaskRunner.Tasks
{
    public abstract class ExtensionFunctions
    {
        private string userName = string.Empty;

        public ExtensionFunctions(string userName)
        {
            this.userName = userName;
        }

        public string GetUserName()
        {
            return userName;
        }

        protected static string GetValueSafe(string dmiKey, Dictionary<string, string> collection)
        {
            dmiKey = dmiKey.Trim(' ');
            if (!collection.ContainsKey(dmiKey))
            {
                return String.Empty;
            }
            return collection[dmiKey];
        }

        public abstract string GetPostCode(string location);

        public abstract string GetJobType(string dmiJobType);

        public abstract string GetEmail(string dmiEmail);

        public abstract string GetIndustry(string industry);
    }
}
