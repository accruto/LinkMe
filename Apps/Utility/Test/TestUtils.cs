using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Utility.Test
{
    public static class TestUtils
    {
        private static readonly TimeSpan TimeOfDayCutoff = new TimeSpan(23, 59, 0);

        public static void SaveToFile(string filePath, string value)
        {
            using (var writer = new StreamWriter(filePath, false))
            {
                writer.Write(value);
            }
        }
    
        /// <summary>
        /// Throws an NUnit IgnoreException if run in the last minute before midnight. Used for tests that rely
        /// on the calendar day to stay the same throughout the test.
        /// </summary>
        public static void IgnoreTestIfCloseToMidnight()
        {
            if (DateTime.Now.TimeOfDay > TimeOfDayCutoff)
                throw new AssertInconclusiveException("This just isn't the right time to run this particular test.");
        }

        /// <summary>
        /// Checks that the time is approximately the expected time within a range of tolerance.
        /// </summary>
        public static void AssertApproxTime(DateTime expected, DateTime actual)
        {
            Assert.IsTrue(Math.Abs(expected.Ticks - actual.Ticks) <= 50000, "Expected DateTime: " + expected + ", actual DateTime: " + actual);
        }

        /// <summary>
        /// Asserts that the types of the two objects are equal, then asserts that all their public properties
        /// are equal using reflection and comparing using the object.Equals() method.
        /// </summary>
        public static void AssertPropertiesEqual(object expected, object actual)
        {
            if (ReferenceEquals(expected, actual))
                return;

            if (expected == null)
            {
                Assert.Fail("NULL was expected, but the actual object was of type " + actual.GetType().FullName);
            }
            else if (actual == null)
            {
                Assert.Fail("An object of type " + expected.GetType().FullName
                    + " was expected, but the actual object was NULL");
            }
            else if (expected.GetType() != actual.GetType())
            {
                Assert.Fail("An object of type " + expected.GetType().FullName
                    + " was expected, but the actual object was of type " + actual.GetType().FullName);
            }
            else
            {
                PropertyInfo[] properties = expected.GetType().GetProperties(
                    BindingFlags.Instance | BindingFlags.Public);
                if (properties.Length == 0)
                {
                    throw new ArgumentException("The expected object was of type " + expected.GetType().FullName
                        + ", which has no public properties to compare.");
                }

                foreach (PropertyInfo property in properties)
                {
                    object expectedValue = property.GetValue(expected, null);
                    object actualValue = property.GetValue(actual, null);
                    if (!Equals(expectedValue, actualValue))
                    {
                        Assert.Fail("The value of the '" + property.Name + "' property differs. Expected: "
                            + (expectedValue == null ? "NULL" : expectedValue.ToString()) + ", actual: "
                                + (actualValue == null ? "NULL" : actualValue.ToString()));
                    }
                }
            }
        }

        public static void AssertCollectionsEqual<T>(ICollection expected, ICollection actual, Func<T, T, bool> comparer)
        {
            AssertCollectionsEqual(ToGenericList<T>(expected), ToGenericList<T>(actual), comparer);
        }

        public static void AssertCollectionsEqual<T>(ICollection<T> expected, ICollection<T> actual)
        {
            AssertCollectionsEqual(expected, actual, null);
        }

        public static void AssertCollectionsEqual<T>(ICollection<T> expected, ICollection<T> actual, Func<T, T, bool> comparer)
        {
            if (ReferenceEquals(expected, actual))
                return;

            if (expected == null)
            {
                Assert.Fail("NULL was expected, but the actual collection contained " + actual.Count + " items");
            }
            else if (actual == null)
            {
                Assert.Fail("A collection of " + expected.Count
                    + " items was expected, but the actual collection was NULL");
            }
            else if (expected.Count != actual.Count)
            {
                Assert.Fail("A collection of " + expected.Count
                    + " items was expected, but the actual collection contained " + actual.Count + " items");
            }
            else
            {
                IEnumerator<T> enumExpected = expected.GetEnumerator();
                IEnumerator<T> enumActual = actual.GetEnumerator();
                int index = 0;

                while (enumExpected.MoveNext())
                {
                    Assert.IsTrue(enumActual.MoveNext(), "Actual collection contains " + index
                        + " items, even though Count returns " + actual.Count);

                    if (comparer == null)
                    {
                        Assert.AreEqual(enumExpected.Current, enumActual.Current,
                            "The items at index {0} are different.", index);
                    }
                    else
                    {
                        Assert.IsTrue(comparer(enumExpected.Current, enumActual.Current),
                            "The items at index {0} are different.\r\nExpected: {1}\r\nactual: {2}",
                            index, enumExpected.Current, enumActual.Current);
                    }

                    index++;
                }
            }
        }

        public static void SleepForDifferentSqlTimestamp()
        {
            Thread.Sleep(20);
        }

        private static IList<T> ToGenericList<T>(ICollection collection)
        {
            if (collection == null)
                return null;

            var generic = new List<T>(collection.Count);
            generic.AddRange(collection.Cast<T>());
            return generic;
        }
    }
}