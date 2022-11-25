using System;
using System.Threading.Tasks;
using Truck.Management.Test.Domain.Utils;

namespace Truck.Management.Test.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        int SaveChanges();
    }
}
