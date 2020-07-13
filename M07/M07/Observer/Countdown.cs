using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace M07.Observer
{
    /// <summary>
    /// Class which transmit a message to any subscriber
    /// </summary>
    public class Countdown
    {
        private string _message;
        /// <summary>
        /// The message which need to send
        /// </summary>
        public EventHandler<string> SendMessage;
        /// <summary>
        /// Delay of transmit
        /// </summary>
        public int Delay { get; private set; }
        /// <summary>
        /// Initializes a new instance of the Countdown class that has a specified parameters
        /// </summary>
        /// <param name="message">Message which need to send</param>
        /// <param name="delay">Delay of transmit</param>
        public Countdown(string message, int delay)
        {
            Delay = delay;
            _message = message;
        }
        /// <summary>
        /// Set the new value to message
        /// </summary>
        /// <param name="message">New value</param>
        public void SetMessage(string message)
        {
            _message = message;
        }
        /// <summary>
        /// Send the message to subscribers
        /// </summary>
        public void SendMessageToSubs()
        {
            Thread.Sleep(Delay);
            SendMessage?.Invoke(this, _message);
        }
    }
}
