using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Truck.Management.Test.Domain.Interfaces;
using Truck.Management.Test.Infra.Data.Context;

namespace Truck.Management.Test.Infra.Data.Repository
{
    public class TruckRepository : Repository<Domain.Models.Truck>, ITruckRepository
    {
        public TruckRepository(DataBaseContext context) : base(context)
        {

        }

        public async Task<Domain.Models.Truck> GetTruckById(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Domain.Models.Truck>> ListAllTrucks()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
