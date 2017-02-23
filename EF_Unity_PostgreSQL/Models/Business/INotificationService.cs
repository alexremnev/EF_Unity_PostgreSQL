using Intuit.Ipp.WebhooksService;

namespace EF_Unity_PostgreSQL.Models.Business
{
    public interface INotificationService : IWebhooksService
    {
        /// <summary>
        /// Process data.
        /// </summary>
        /// <param name="notifications">incoming notifications.</param>
        void Process(string notifications);
    }
}
