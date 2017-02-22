using EF_Unity_PostgreSQL.Models.Context;
using EF_Unity_PostgreSQL.Models.DAL;

namespace EF_Unity_PostgreSQL.Models.Repository
{
    public class TaxRateRepository : GenereicRepository<TaxRate>, ITaxRateRepository
    {
        public TaxRateRepository(ITaxRateContext context) : base(context.GetContext)
        {
        }
    }
}