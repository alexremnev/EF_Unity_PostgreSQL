using System;
using System.Data.Entity;
using Common.Logging;

namespace EF_Unity_PostgreSQL.Models.Repository
{
    public class GenereicRepository<TEntity> : IGenereicRepository<TEntity> where TEntity : class
    {
        public GenereicRepository(DbContext context, string entity)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _entityName = entity;
            Log = LogManager.GetLogger(GetType());
            _context.Database.Log = (dbLog => Log.Debug(dbLog));
        }

        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly ILog Log;
        private readonly string _entityName;
        private string _entityId = "";

        public void Save(TEntity entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));
                _dbSet.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error($"Exception occured when application tried to create an {_entityName}", e);
                throw;
            }
        }

        public TEntity Get(string id)
        {
            try
            {
                _entityId = id;
                if (string.IsNullOrEmpty(id)) throw new Exception("Id can not be null or empty");
                //                var key = int.Parse(id);
                var key = id;
                return _dbSet.Find(key);
            }
            catch (Exception e)
            {
                Log.Error(
                        $"Exception occured when application tried to get entity with id = {_entityId} from database", e);
                throw;
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error($"Exception occured when application tried to update {_entityName}", e);
                throw;
            }
        }

        public void Delete(string id)
        {
            try
            {
                var entity = _dbSet.Find(id);
                if (entity == null) return;
                _entityId = id;
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error($"Exception occured when application tried to delete the object with id = {_entityId}", e);
                throw;
            }
        }
    }
}