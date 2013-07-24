using System;
using System.Web;

namespace LinkMe.Apps.Asp.Sessions
{
    public class SessionWrapper
    {
        private readonly HttpSessionStateBase _session;

        public SessionWrapper(HttpSessionStateBase session)
        {
            _session = session;
        }

        public void Set<TValue>(string key, TValue value)
        {
            _session[key] = value;
        }

        public TValue Get<TValue>(string key)
            where TValue : class
        {
            return _session[key] as TValue;
        }

        public void Set(string key, bool value)
        {
            if (value)
                _session[key] = true;
            else
                _session.Remove(key);
        }

        public bool GetBoolean(string key)
        {
            var value = _session[key];
            if (value == null)
                return false;
            if (!(value is bool))
                return false;
            return (bool)value;
        }

        public int GetInt32(string key)
        {
            var value = _session[key];
            if (value == null)
                return 0;
            if (!(value is int))
                return 0;
            return (int)value;
        }

        public Guid? GetGuid(string key)
        {
            var value = _session[key];
            if (value == null)
                return null;
            if (!(value is Guid))
                return null;
            return (Guid)value;
        }
    }
}
