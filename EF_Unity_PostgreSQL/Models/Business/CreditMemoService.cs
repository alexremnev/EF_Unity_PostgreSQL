using EF_Unity_PostgreSQL.Models.Repository;
using Intuit.Ipp.Data;

namespace EF_Unity_PostgreSQL.Models.Business
{
    public class CreditMemoService : BaseService<CreditMemo>, ICreditMemoService
    {
        public CreditMemoService(IReportRepository reportRepository, ITaxRepository repository, IOAuthService oAuthService) : base(reportRepository, repository, oAuthService, "CreditMemo")
        {
        }
    }
}