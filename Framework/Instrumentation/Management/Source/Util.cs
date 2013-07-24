using System.Diagnostics;

using LinkMe.Framework.Utility;

namespace LinkMe.Framework.Instrumentation.Management
{
	internal sealed class Util
	{
		private Util()
		{
		}

		internal static string[] InternStringArray(Interner interner, string[] input)
		{
			if (input == null || input.Length == 0)
				return null;
			else if (interner == null)
				return (string[]) input.Clone();
			else
				return interner.Intern(input);
		}

		/// <summary>
		/// Adds two string arrays together, ensuring that there are no duplicates. Each of the arrays is
		/// assumed to have no duplicates in it on input.
		/// </summary>
		internal static string[] AddRangeUnique(string[] existing, string[] toAdd)
		{
			if (toAdd == null || toAdd.Length == 0)
				return existing;

			if (existing == null)
				return (string[])toAdd.Clone();

			// Create a new array big enough to hold all the data and copy data in the existing array to it.

			string[] tempArray = new string[existing.Length + toAdd.Length];
			System.Array.Copy(existing, 0, tempArray, 0, existing.Length);
			int newIndex = existing.Length;

			// Iterate over array toAdd and copy each element that is not in array existing.

			for (int index = 0; index < toAdd.Length; index++)
			{
				string current = toAdd[index];
				if (System.Array.IndexOf(existing, current) == -1)
				{
					tempArray[newIndex++] = current;
				}
			}

			if (newIndex == existing.Length)
				return existing; // Nothing added.
			else if (newIndex == tempArray.Length)
				return tempArray; // Everything in toAdd added.
			else
			{
				// Some items added - create an array of exactly the right length.

				string[] added = new string[newIndex];
				System.Array.Copy(tempArray, 0, added, 0, newIndex);

				return added;
			}
		}

		internal static string[] RemoveRange(string[] existing, string[] toRemove)
		{
			if (existing == null)
				return null;

			if (toRemove == null || toRemove.Length == 0)
				return existing;

			string[] tempArray = new string[existing.Length];
			int newIndex = 0;

			// Iterate over the existing array and copy each element that is not removed to a new array.

			for (int index = 0; index < existing.Length; index++)
			{
				string current = existing[index];
				if (System.Array.IndexOf(toRemove, current) == -1)
				{
					tempArray[newIndex++] = current;
				}
			}

			if (newIndex == existing.Length)
				return existing; // Nothing removed.
			else if (newIndex == 0)
				return null; // Everything removed.
			else

			{
				// Create a new array of the correct length.

				string[] subtracted = new string[newIndex];
				System.Array.Copy(tempArray, 0, subtracted, 0, newIndex);

				return subtracted;
			}
		}

		/// <summary>
		/// Returns true if "containing" contains all the values in "contains", otherwise false.
		/// </summary>
		internal static bool ArrayContainsEntireArray(string[] containing, string[] contains)
		{
			Debug.Assert(contains != null && contains.Length > 0, "contains != null && contains.Length > 0");

			if (containing == null)
				return false;

			foreach (string s in contains)
			{
				if (System.Array.IndexOf(containing, s) == -1)
					return false;
			}

			return true;
		}

		/// <summary>
		/// Returns string values that are in both of the input arrays.
		/// </summary>
		internal static string[] GetArrayIntersection(string[] one, string[] two)
		{
			if (one == null || two == null || one.Length == 0 || two.Length == 0)
				return null;

			string[] intersection = new string[System.Math.Min(one.Length, two.Length)];
			int count = 0;

			for (int index = 0; index < one.Length; index++)
			{
				if (System.Array.IndexOf(two, one[index]) != -1)
				{
					intersection[count++] = one[index];
				}
			}

			if (count == 0)
				return null;
			if (intersection.Length == count)
				return intersection;

			string[] trimmed = new string[count];
			System.Array.Copy(intersection, 0, trimmed, 0, count);

			return trimmed;
		}
	}
}
