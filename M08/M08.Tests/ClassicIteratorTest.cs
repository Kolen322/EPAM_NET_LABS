using System;
using NUnit.Framework;
using M08.Library.Collections;

namespace M08.Tests
{
    class ClassicIteratorTest
    {
        [Test]
        public void MoveNext_NormalConditions_Test()
        {
            ClassicIterator<int> testClassicIterator = new ClassicIterator<int>(new int[] { 1, 2 });
            Assert.That(testClassicIterator.MoveNext(), Is.EqualTo(true));
        }

        [Test]
        public void Reset_NormalConditions_Test()
        {
            ClassicIterator<int> testClassicIterator = new ClassicIterator<int>(new int[] { 1, 2 });
            testClassicIterator.MoveNext();
            testClassicIterator.MoveNext();
            testClassicIterator.Reset();
            testClassicIterator.MoveNext();
            Assert.That(testClassicIterator.Current, Is.EqualTo(1));
        }

        [Test]
        public void Current_InvalidOperationException_Test()
        {
            int? testCurrent = null;
            ClassicIterator<int> testClassicIterator = new ClassicIterator<int>(new int[] { 1, 2 });
            Assert.Throws<InvalidOperationException>(() => testCurrent = testClassicIterator.Current);
        }

    }
}
