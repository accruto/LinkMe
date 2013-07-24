using System;
using LinkMe.Utility.Utilities;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Applications.Ajax
{
    public enum AjaxResultCode
    {
        FAILURE = 0,
        SUCCESS = 1,
        EMPTY = 2
    }

    public class AjaxResult
    {
        public AjaxResultCode ResultCode;
        public string Message;
        public object UserData;

        internal AjaxResult(AjaxResultCode resultCode)
            : this(resultCode, null, null)
        {
        }

        internal AjaxResult(AjaxResultCode resultCode, string message)
            : this(resultCode, message, null)
        {
        }

        internal AjaxResult(AjaxResultCode resultCode, string[] messages)
            : this(resultCode, (messages == null ? null : string.Join(StringUtils.HTML_LINE_BREAK, messages)),
                null)
        {
        }

        internal AjaxResult(AjaxResultCode resultCode, string message, AjaxResultUserData userData)
        {
            ResultCode = resultCode;
            Message = message;
            UserData = userData;
        }
    }

    public abstract class AjaxResultUserData
    {
        internal AjaxResultUserData()
        {
        }
    }

    public class ElementValuesUserData : AjaxResultUserData
    {
        public string[] ElementNames;
        public string[] ElementValues; // Note that you can set an element value to NULL to leave it as is (skip updating).

        internal ElementValuesUserData(string[] elementNames, string[] elementValues)
        {
            ElementNames = elementNames;
            ElementValues = elementValues;
        }
    }

    public class IdArrayUserData : AjaxResultUserData
    {
        public string[] IDs;

        internal IdArrayUserData(Guid?[] ids)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");

            IDs = new string[ids.Length];

            for (int i = 0; i < ids.Length; i++)
            {
                Guid? id = ids[i];
                IDs[i] = (id == null ? null : id.Value.ToString("n"));
            }
        }
    }
}