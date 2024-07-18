using AuthApi.Interfaces;

namespace AuthApi.Repository
{
    public class EntityRepository : IEntityRepository
    {
        public ApplicationDbContext _db { get; set; }

        public EntityRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add<T>(T entity) where T : class
        {
            _db.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _db.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _db.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _db.Update(entity);
        }

    }
}
