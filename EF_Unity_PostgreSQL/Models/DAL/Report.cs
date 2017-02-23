using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Unity_PostgreSQL.Models.DAL
{
    [Table("Reports", Schema = "public")]
    public class Report : BaseEntity
    {
    }
}