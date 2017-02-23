using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Unity_PostgreSQL.Models.DAL
{
    [Table("LineItems", Schema = "public")]
    public class LineItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string ReportId { get; set; }
        //[Required]
        //public virtual Report Report { get; set; }
    }
}