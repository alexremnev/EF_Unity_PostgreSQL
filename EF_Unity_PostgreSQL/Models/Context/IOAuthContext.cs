using System.Data.Entity;
using EF_Unity_PostgreSQL.Models.DAL;

namespace EF_Unity_PostgreSQL.Models.Context
{
    public interface IOAuthContext
    {
        DbSet<OAuth> OAuths { get; set; }
        DbContext GetContext { get; }
    }
}