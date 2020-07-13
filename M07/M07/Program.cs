using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M07.Observer;
namespace M07
{
    class Program
    {
        static void Main(string[] args)
        {
            var testCountdown = new Countdown("Hello", 2000);
            var firstSubscriber = new Subscriber("Andrew");
            var secondSubscriber = new Subscriber("Kolya");
            var firstLuckySubscriber = new LuckySubscriber("LuckyKolya");

            firstSubscriber.Subscribe(testCountdown);
            secondSubscriber.Subscribe(testCountdown);
            firstLuckySubscriber.Subscribe(testCountdown);

            testCountdown.SendMessageToSubs();

            testCountdown.SetMessage("Bye");

            testCountdown.SendMessageToSubs();

            Console.ReadKey();

            SortMatrix matrix = new SortMatrix(OrderType.Asc, ComparisonType.MaxElement);
        }
    }
}
