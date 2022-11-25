using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Truck.Management.Test.Domain.Interfaces;
using Truck.Management.Test.Domain.Utils;
using Truck.Management.Test.Infra.Data.Context;

namespace Truck.Management.Test.Infra.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected DataBaseContext _context;
        protected DbSet<TEntity> _dbSet;

        protected Repository(DataBaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
