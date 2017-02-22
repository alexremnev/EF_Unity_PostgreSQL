using System.Data.Entity;
using EF_Unity_PostgreSQL.Models.DAL;

namespace EF_Unity_PostgreSQL.Models.Context
{
    public interface IReportContext
    {
        DbSet<Report> Reports { get; set; }
        DbContext GetContext { get; }
    }
}