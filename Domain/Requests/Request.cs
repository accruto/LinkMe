using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Requests
{
    [Serializable]
    public abstract class Request
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime? FirstSentTime { get; set; }
        public DateTime? LastSentTime { get; set; }
        public DateTime? ActionedTime { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as Request);
        }

        public bool Equals(Request obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj.Id.Equals(Id)
                && Equals(obj.Text, Text)
                && Equals(obj.Status, Status);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Id.GetHashCode();
                result = (result*397) ^ (Text != null ? Text.GetHashCode() : 0);
                result = (result*397) ^ Status.GetHashCode();
                return result;
            }
        }
    }
}
