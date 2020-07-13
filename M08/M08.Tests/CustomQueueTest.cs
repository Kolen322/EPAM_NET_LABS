using NUnit.Framework;
using System;
using M08.Library.Collections;
using System.Collections.Generic;

namespace M08.Tests
{
    class CustomQueueTest
    {
        [Test]
        public void IsEmpty_NormalConditions_Test()
        {
            CustomQueue<int> testQueue = new CustomQueue<int>();
            var empty = testQueue.IsEmpty();
            Assert.That(empty, Is.EqualTo(true));
        }

        [Test]
        public void Peek_NormalConditions_Test()
        {
            CustomQueue<string> testQueue = new CustomQueue<string>();
            testQueue.Enqueue("Test");
            testQueue.Enqueue("Test2");
            Assert.That(testQueue.Peek(), Is.EqualTo("Test"));
        }

        [Test]
        public void Enqueue_NormalConditions_Test()
        {
            CustomQueue<string> testQueue = new CustomQueue<string>();
            testQueue.Enqueue("Test");
            testQueue.Enqueue("Test2");
            Assert.That(testQueue.Peek(), Is.EqualTo("Test"));
        }

        [Test]
        public void Dequeue_NormalConditions_Test()
        {
            CustomQueue<string> testQueue = new CustomQueue<string>();
            testQueue.Enqueue("Test");
            testQueue.Enqueue("Test2");
            testQueue.Dequeue();
            Assert.That(testQueue.Peek(), Is.EqualTo("Test2"));
        }

        [Test]
        public void Peek_InvalidOperationException_Test()
        {
            CustomQueue<string> testQueue = new CustomQueue<string>();
            Assert.Throws<InvalidOperationException>(() => testQueue.Peek());
        }

        [Test]
        public void Dequeue_InvalidOperationException_Test()
        {
            CustomQueue<string> testQueue = new CustomQueue<string>();
            Assert.Throws<InvalidOperationException>(() => testQueue.Dequeue());
        }

        [Test]
        public void Enqueue_ArgumentNullException_Test()
        {
            CustomQueue<string> testQueue = new CustomQueue<string>();
            Assert.Throws<ArgumentNullException>(() => testQueue.Enqueue(null));
        }

    }
}
