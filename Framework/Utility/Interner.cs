using System;
using System.Collections;

namespace LinkMe.Framework.Utility
{
	public interface IInternable
	{
		void Intern(Interner interner);
	}

	/// <summary>
	/// Allows strings to be interned in an instance of a class, which can be garbage collected when no longer
	/// required (unlike the system intern pool, which is not released until the AppDomain is destroyed).
	/// </summary>
	public sealed class Interner
	{
		private readonly Hashtable m_values = new Hashtable(StringComparer.InvariantCulture);
		private readonly bool m_useSystemInterner;

		public Interner()
			: this(true)
		{
		}

		public Interner(bool useSystemInterner)
		{
			m_useSystemInterner = useSystemInterner;
		}

        public void Intern(ref string value)
        {
            value = Intern(value);
        }

	    public string Intern(string value)
		{
			if (value == null)
				return null;
			if (value.Length == 0)
				return string.Empty;

			string interned = null;
			
			if (m_useSystemInterner)
			{
				interned = string.IsInterned(value);
				if (interned != null)
					return interned;
			}

			interned = (string)m_values[value];
			if (interned != null)
				return interned;

			m_values.Add(value, value);
			return value;
		}

		public string[] Intern(string[] values)
		{
			if (values == null)
				return null;

			string[] interned = new string[values.Length];

			for (int index = 0; index < values.Length; index++)
			{
				interned[index] = Intern(values[index]);
			}

			return interned;
		}

		public void Intern(IInternable internable)
		{
			if (internable != null)
			{
				internable.Intern(this);
			}
		}

        public IDictionary Intern(IDictionary dictionary)
        {
            if (dictionary == null || dictionary.Count == 0)
                return dictionary;

            // Use Reflection to create another dictionary of the same type. A bit ugly, but
            // probably the least ugly way.
            IDictionary interned = (IDictionary)Activator.CreateInstance(dictionary.GetType());

            IDictionaryEnumerator enumerator = dictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object key = enumerator.Key;

                string stringKey = key as string;
                if (stringKey != null)
                {
                    key = Intern(stringKey);
                }

                object value = enumerator.Value;
                TryIntern(ref value);

                interned.Add(key, value);
            }

            return interned;
        }

        public bool TryIntern(ref object value)
        {
            string str = value as string;
            if (str != null)
            {
                value = Intern(str);
                return true;
            }

            string[] array = value as string[];
            if (array != null)
            {
                value = Intern(array);
                return true;
            }

            IDictionary dictionary = value as IDictionary;
            if (dictionary != null)
            {
                value = Intern(dictionary);
                return true;
            }

            return false;
        }

	    public void Clear()
		{
			m_values.Clear();
		}
	}
}
