using System;
using NUnit.Framework;
using M08.Library.Collections;

namespace M08.Tests
{
    class CustomSetTest
    {
        [Test]
        public void IsEmpty_NormalConditions_Test()
        {
            CustomSet<string> testSet = new CustomSet<string>();
            var result = testSet.IsEmpty();
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void Add_NormalConditions_Test()
        {
            CustomSet<string> testSet = new CustomSet<string>();
            CustomSet<string> expectedSet = new CustomSet<string> { "Test", "Test2" };
            testSet.Add("Test");
            testSet.Add("Test");
            testSet.Add("Test2");
            CollectionAssert.AreEqual(testSet, expectedSet);
        }

        [Test]
        public void Remove_NormalConditions_Test()
        {
            CustomSet<string> testSet = new CustomSet<string>();
            CustomSet<string> expectedSet = new CustomSet<string> { "Test", "Test3" };
            testSet.Add("Test");
            testSet.Add("Test2");
            testSet.Add("Test3");
            testSet.Remove("Test2");
            CollectionAssert.AreEqual(testSet, expectedSet);
        }

        [Test]
        public void Union_NormalConditions_Test()
        {
            CustomSet<string> testSet = new CustomSet<string> { "Test", "Test3" };
            CustomSet<string> unionSet = new CustomSet<string> { "Test3", "Test4" };
            CustomSet<string> expectedSet = new CustomSet<string> { "Test", "Test3", "Test4" };
            testSet.UnionWith(unionSet);
            CollectionAssert.AreEqual(testSet, expectedSet);
        }

        [Test]
        public void Intersection_NormalConditions_Test()
        {
            CustomSet<string> testSet = new CustomSet<string> { "Test", "Test3", "Test4" };
            CustomSet<string> intersectionSet = new CustomSet<string> { "Test3", "Test4" };
            CustomSet<string> expectedSet = new CustomSet<string> { "Test3", "Test4" };
            testSet.IntersectionWith(intersectionSet);
            CollectionAssert.AreEqual(testSet, expectedSet);
        }

        [Test]
        public void Difference_NormalConditions_Test()
        {
            CustomSet<string> testSet = new CustomSet<string> { "Test", "Test3", "Test4", "Test5" };
            CustomSet<string> differenceSet = new CustomSet<string> { "Test3", "Test4", };
            CustomSet<string> expectedSet = new CustomSet<string> { "Test", "Test5" };
            testSet.DifferenceWith(differenceSet);
            CollectionAssert.AreEqual(testSet, expectedSet);
        }

        [Test]
        public void Subset_NormalCOnditions_Test()
        {
            CustomSet<string> testSet = new CustomSet<string> { "Test", "Test3", "Test4", "Test5" };
            CustomSet<string> subSet = new CustomSet<string> { "Test3", "Test4", };
            var result = subSet.SubsetWith(testSet);
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void Add_ArgumentNullException_Test()
        {
            CustomSet<string> testSet = new CustomSet<string> { "Test", "Test3", "Test4", "Test5" };
            Assert.Throws<ArgumentNullException>(() => testSet.Add(null));
        }

        [Test]
        public void Remove_ArgumentNullException_Test()
        {
            CustomSet<string> testSet = new CustomSet<string> { "Test", "Test3", "Test4", "Test5" };
            Assert.Throws<ArgumentNullException>(() => testSet.Remove(null));
        }

        [Test]
        public void Union_ArgumentNullException_Test()
        {
            CustomSet<string> testSet = new CustomSet<string> { "Test", "Test3", "Test4", "Test5" };
            Assert.Throws<ArgumentNullException>(() => testSet.UnionWith(null));
        }

        [Test]
        public void Intersection_ArgumentNullException_Test()
        {
            CustomSet<string> testSet = new CustomSet<string> { "Test", "Test3", "Test4", "Test5" };
            Assert.Throws<ArgumentNullException>(() => testSet.IntersectionWith(null));
        }

        [Test]
        public void Difference_ArgumentNullException_Test()
        {
            CustomSet<string> testSet = new CustomSet<string> { "Test", "Test3", "Test4", "Test5" };
            Assert.Throws<ArgumentNullException>(() => testSet.DifferenceWith(null));
        }

        [Test]
        public void Subset_ArgumentNullException_Test()
        {
            CustomSet<string> testSet = new CustomSet<string> { "Test", "Test3", "Test4", "Test5" };
            Assert.Throws<ArgumentNullException>(() => testSet.SubsetWith(null));
        }
    }
}
