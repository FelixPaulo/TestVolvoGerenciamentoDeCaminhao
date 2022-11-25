using Truck.Management.Test.Domain.Interfaces;
using Truck.Management.Test.Infra.Data.Context;

namespace Truck.Management.Test.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;

        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
