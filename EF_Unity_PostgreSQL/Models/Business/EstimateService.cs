using System.Collections.Generic;
using EF_Unity_PostgreSQL.Models.Repository;
using Intuit.Ipp.Data;

namespace EF_Unity_PostgreSQL.Models.Business
{
    public class EstimateService : BaseService<Estimate>, IEstimateService
    {
        public EstimateService(IReportRepository reportRepository, ITaxRepository repository, IOAuthService oAuthService) : base(reportRepository, repository, oAuthService, "Estimate")
        {
        }

        public override void Save(IList<Estimate> list = null)
        {
        }
    }
}