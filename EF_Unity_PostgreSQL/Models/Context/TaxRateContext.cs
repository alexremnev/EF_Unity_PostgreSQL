using System.Data.Entity;
using EF_Unity_PostgreSQL.Models.DAL;

namespace EF_Unity_PostgreSQL.Models.Context
{
    public class TaxRateContext : DbContext, ITaxRateContext
    {
        public TaxRateContext() : base("QuickBooks") { }
        public DbSet<TaxRate> TaxRates { get; set; }
        public DbContext GetContext => this;
    }
}