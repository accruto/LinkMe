using System;
using System.Web.SessionState;

namespace LinkMe.Web.Applications.Facade
{
    public class HttpSessionWrapper
    {
        private HttpSessionState session;
        private Type callingType;

        public HttpSessionWrapper(Type callingType, HttpSessionState session)
        {
            this.session = session;
            this.callingType = callingType;
        }

        public void SetValue(string keyName, object value)
        {
            session[GetKeyName(keyName)] = value;
        }

        public object GetValue(string keyName)
        {
            return session[GetKeyName(keyName)];
        }

        public void RemoveValue(string keyName)
        {
            session.Remove(GetKeyName(keyName));
        }

        private string GetKeyName(string keyName)
        {
            return callingType.Name + "_" + keyName;
        }
    }
}
