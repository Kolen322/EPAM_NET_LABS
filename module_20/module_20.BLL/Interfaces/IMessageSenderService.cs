
namespace module_20.BLL.Interfaces
{
    /// <summary>
    /// Specifies the contract for a message sender service
    /// </summary>
    public interface IMessageSenderService
    {
        /// <summary>
        /// Send a message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="recipient">Recipient of message</param>
        /// <returns>Message</returns>
        public string Send(string message, string recipient);
    }
}
