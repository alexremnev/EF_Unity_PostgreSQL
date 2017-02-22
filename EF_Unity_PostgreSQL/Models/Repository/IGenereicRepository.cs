namespace EF_Unity_PostgreSQL.Models.Repository
{
   public interface IGenereicRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Save entity in database.
        /// </summary>
        /// <param name="entity">the entity which must be saved in database. if entity is null occurs ArgumentNullException.</param>
        void Save(TEntity entity);
        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id">entity id.</param>
        /// <returns>found entity or null in case there's no entity with passed id found.</returns>
        TEntity Get(string id);
        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="entity">persistant entity.</param>
        void Update(TEntity entity);
        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="id">entity id.</param>
        void Delete(string id);
    }
}
