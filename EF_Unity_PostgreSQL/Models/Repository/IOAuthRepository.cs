using EF_Unity_PostgreSQL.Models.DAL;

namespace EF_Unity_PostgreSQL.Models.Repository
{
    public interface IOAuthRepository : IGenereicRepository<OAuth>
    {
        /// <summary>
        /// Get Oauth.
        /// </summary>
        /// <returns>found entity or null in case there's no entity.</returns>
        OAuth Get();
        /// <summary>
        /// Delete an entity.
        /// </summary>
        void Delete();
    }
}
