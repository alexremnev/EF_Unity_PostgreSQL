using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Unity_PostgreSQL.Models.DAL
{
    [Table("OAuths", Schema = "public")]
    public class OAuth
    {
        [Key]
        public string RealmId { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
    }
}