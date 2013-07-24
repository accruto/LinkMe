using System;

namespace LinkMe.Framework.Utility.Expressions
{
    [Flags]
    public enum ModificationFlags
    {
        None = 0,
        /// <summary>
        /// Allow shingling of words in the query.
        /// </summary>
        AllowShingling = 2
    }
}