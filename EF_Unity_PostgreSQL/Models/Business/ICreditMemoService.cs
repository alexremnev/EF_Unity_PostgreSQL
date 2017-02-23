using System.Collections.Generic;
using Intuit.Ipp.Data;

namespace EF_Unity_PostgreSQL.Models.Business
{
    public interface ICreditMemoService : IBaseService<CreditMemo>
    {
        /// <summary>
        /// Save the list of Credit Memo in database.
        /// </summary>
        /// <param name="list">the list of Credit Memo.</param>
        void Save(IList<CreditMemo> list = null);
    }
}
