using System.Collections.Generic;
using EF_Unity_PostgreSQL.Models.DAL;

namespace EF_Unity_PostgreSQL.Models.Repository
{
    public interface ITaxRepository : IGenereicRepository<TaxRate>
    {
        /// <summary>
        /// Get tax rate depends on state.
        /// </summary>
        /// <param name="state">entity state.</param>
        /// <returns>returns the entity of taxRate.</returns>
        TaxRate GetByCountrySubDivisionCode(string state);
        /// <summary>
        /// Get list of taxRate.
        /// </summary>
        /// <returns>returns list of taxRate or null in case there's no entity found.</returns>
        IList<TaxRate> List();
    }
}
