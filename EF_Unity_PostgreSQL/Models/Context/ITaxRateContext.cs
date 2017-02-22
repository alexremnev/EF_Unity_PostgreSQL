using System.Data.Entity;
using EF_Unity_PostgreSQL.Models.DAL;

namespace EF_Unity_PostgreSQL.Models.Context
{
    public interface ITaxRateContext
    {
        DbSet<TaxRate> TaxRates { get; set; }
        DbContext GetContext { get; }
    }
}