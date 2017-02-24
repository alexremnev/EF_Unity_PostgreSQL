using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EF_Unity_PostgreSQL.Models.Business;

namespace EF_Unity_PostgreSQL.Models.DAL
{
    [Table("OAuths", Schema = "public")]
    public class OAuth
    {
        [Key]
        public string RealmId { get; set; }
        [Encrypted]
        public string AccessToken { get; set; }
        [Encrypted]
        public string AccessTokenSecret { get; set; }
    }
}