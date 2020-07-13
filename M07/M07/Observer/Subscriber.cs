using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M07.Observer
{
    /// <summary>
    /// Class which that describes the subscriber
    /// </summary>
    public class Subscriber
    {
        private readonly string _name;
        /// <summary>
        /// Initializes a new instance of the Subscriber class that has a specified parameter
        /// </summary>
        /// <param name="name">Name of subscriber</param>
        public Subscriber( string name)
        {
            _name = name;
        }
        /// <summary>
        /// Subscribe to mailing
        /// </summary>
        /// <param name="countdown">Event</param>
        public void Subscribe(Countdown countdown)
        {
            countdown.SendMessage += GetInfo;
        }
        /// <summary>
        /// Unsubscribe to mailing
        /// </summary>
        /// <param name="countdown">Event</param>
        public void UnSubscribe(Countdown countdown)
        {
            countdown.SendMessage -= GetInfo;
        }

        private void GetInfo(object sender, string message)
        {
            Console.WriteLine($"Subscriber with name:{_name} get '{message}' ");
        }
    }
}
