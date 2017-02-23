using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Unity_PostgreSQL.Models.DAL
{
    [Table("TaxRates", Schema = "public")]
    public class TaxRate
    {
        [Key]
        public string CountrySubDivisionCode { get; set; }
        public decimal Tax { get; set; }
    }
}