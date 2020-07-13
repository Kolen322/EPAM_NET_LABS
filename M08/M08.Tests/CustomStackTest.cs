using System;
using NUnit.Framework;
using M08.Library.Collections;

namespace M08.Tests
{
    class CustomStackTest
    {
        [Test]
        public void IsEmpty_NormalConditions_Test()
        {
            CustomStack<int> testStack = new CustomStack<int>();
            var result = testStack.IsEmpty();
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void Peek_NormalConditions_Test()
        {
            CustomStack<int> testStack = new CustomStack<int>();
            testStack.Push(5);
            testStack.Push(10);
            Assert.That(testStack.Peek, Is.EqualTo(10));
        }

        [Test]
        public void Push_NormalConditions_Test()
        {
            CustomStack<int> testStack = new CustomStack<int>();
            testStack.Push(5);
            testStack.Push(6);
            Assert.That(testStack.Peek(), Is.EqualTo(6));
        }

        [Test]
        public void Pop_NormalConditions_Test()
        {
            CustomStack<int> testStack = new CustomStack<int>();
            testStack.Push(5);
            testStack.Push(10);
            testStack.Pop();
            Assert.That(testStack.Peek(), Is.EqualTo(5));
        }

        [Test]
        public void Pop_InvalidOperationException_Test()
        {
            CustomStack<int> testStack = new CustomStack<int>();
            Assert.Throws<InvalidOperationException>(() => testStack.Pop());
        }

        [Test]
        public void Peek_InvalidOperationException_Test()
        {
            CustomStack<int> testStack = new CustomStack<int>();
            Assert.Throws<InvalidOperationException>(() => testStack.Peek());
        }

        [Test]
        public void Push_ArgumentNullException_Test()
        {
            CustomStack<string> testStack = new CustomStack<string>();
            Assert.Throws<ArgumentNullException>(() => testStack.Push(null));
        }
    }
}
