using System.Data.Entity;

namespace EF_Unity_PostgreSQL.Models
{
    public class TaxRateContext : DbContext, ITaxRateContext
    {
        public TaxRateContext() : base("QuickBooks") { }
        public DbSet<TaxRate> TaxRates { get; set; }
    }
}