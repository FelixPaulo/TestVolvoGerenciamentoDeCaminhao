using System.Collections.Generic;
using System.Threading.Tasks;

namespace Truck.Management.Test.Domain.Interfaces
{
    public interface ITruckRepository : IRepository<Models.Truck>
    {
        Task<Models.Truck> GetTruckById(int id);

        Task<IEnumerable<Models.Truck>> ListAllTrucks();
    }
}
