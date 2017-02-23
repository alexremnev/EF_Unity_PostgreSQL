using System.Collections.Generic;
using Intuit.Ipp.Data;

namespace EF_Unity_PostgreSQL.Models.Business
{
    public interface ISalesReceiptService : IBaseService<SalesReceipt>
    {
        /// <summary>
        /// Save the list of Sales Receipt in database.
        /// </summary>
        /// <param name="list">the list of sales receipt.</param>
        void Save(IList<SalesReceipt> list = null);
    }
}