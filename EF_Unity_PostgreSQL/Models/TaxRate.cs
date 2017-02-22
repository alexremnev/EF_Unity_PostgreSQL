using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Unity_PostgreSQL.Models
{
    [Table("TaxRates", Schema = "public")]
    public class TaxRate
    {
        public int Id { get; set; }
        public string CountrySubDivision { get; set; }
        public decimal Tax { get; set; }
    }
}