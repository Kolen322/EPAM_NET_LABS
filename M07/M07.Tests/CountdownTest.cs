using System;
using NUnit.Framework;
using M07.Observer;
namespace M07.Tests
{
    class CountdownTest
    {
        [Test]
        public void SendMessageToSubs_SendMessageEvent_Test()
        {
            var wasCalled = false;
            var testCountdown = new Countdown("Hello", 2000);

            testCountdown.SendMessage += (o, e) => wasCalled = true;

            testCountdown.SendMessageToSubs();

            Assert.IsTrue(wasCalled);
        }
    }
}
