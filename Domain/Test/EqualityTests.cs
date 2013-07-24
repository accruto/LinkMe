using System;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test
{
    [TestClass]
    public class EqualityTests
    {
        [TestMethod]
        public void TestCollectionEqual()
        {
            Assert.IsTrue(EqualityExtensions.NullableSequenceEqual<object>(null, null));
            Assert.IsTrue(EqualityExtensions.NullableSequenceEqual(null, new object[0]));
            Assert.IsTrue(new object[0].NullableSequenceEqual(null));
            Assert.IsTrue(EqualityExtensions.NullableCollectionEqual<object>(null, null));
            Assert.IsTrue(EqualityExtensions.NullableCollectionEqual(null, new object[0]));
            Assert.IsTrue(new object[0].NullableCollectionEqual(null));

            Assert.IsTrue(new object[0].NullableSequenceEqual(new object[0]));
            Assert.IsTrue(new string[0].NullableSequenceEqual(new object[0]));
            Assert.IsFalse(new object[0].NullableSequenceEqual(new object[1]));
            Assert.IsTrue(new object[0].NullableCollectionEqual(new object[0]));
            Assert.IsTrue(new string[0].NullableCollectionEqual(new object[0]));
            Assert.IsFalse(new object[0].NullableCollectionEqual(new object[1]));

            var one = new[] { "one", "two", "three" };
            var two = new[] { "one", "two", "three" };
            Assert.IsTrue(one.NullableSequenceEqual(one)); // Reference equality shortcut.
            Assert.IsTrue(one.NullableCollectionEqual(one)); // Reference equality shortcut.
            Assert.IsTrue(one.NullableSequenceEqual(two));
            Assert.IsTrue(one.NullableCollectionEqual(two));

            Array.Reverse(two);
            Assert.IsFalse(one.NullableSequenceEqual(two));
            Assert.IsTrue(one.NullableCollectionEqual(two));

            var ione = new[] { 1, 2, 2, 3 };
            var itwo = new[] { 1, 2, 3, 4 };
            Assert.IsFalse(ione.NullableSequenceEqual(itwo));
            Assert.IsFalse(itwo.NullableSequenceEqual(ione));
            Assert.IsFalse(ione.NullableCollectionEqual(itwo));
            Assert.IsFalse(itwo.NullableCollectionEqual(ione));

            itwo = new[] { 6, 7, 8, 9 };
            Assert.IsFalse(ione.NullableSequenceEqual(itwo));
            Assert.IsFalse(ione.NullableCollectionEqual(itwo));
        }
    }
}