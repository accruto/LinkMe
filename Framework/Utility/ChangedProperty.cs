using System;

namespace LinkMe.Framework.Utility
{
    [Serializable]
    public class ChangedProperty<T>
    {
        public T From { get; set; }
        public T To { get; set; }
    }

}
