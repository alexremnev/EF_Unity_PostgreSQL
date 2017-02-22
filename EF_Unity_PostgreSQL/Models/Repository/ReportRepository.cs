using EF_Unity_PostgreSQL.Models.Context;
using EF_Unity_PostgreSQL.Models.DAL;

namespace EF_Unity_PostgreSQL.Models.Repository
{
    public class ReportRepository : GenereicRepository<Report>, IReportRepository
    {
        public ReportRepository(IReportContext context) : base(context.GetContext)
        {
        }
    }
}