using System.Collections.Generic;
using System.Threading.Tasks;
using Truck.Management.Test.Application.Models;

namespace Truck.Management.Test.Application.Interfaces
{
    public interface ITruckApplication
    {
        Task<TruckResultDto> AddTruck(TruckDto truckDto);
        Task<TruckResultDto> UpdateTruck(TruckUpdateDto truckDto);
        Task<TruckResultDto> GetTruckById(int id);
        Task<IEnumerable<TruckResultDto>> ListAllTrucks();
        Task<bool> RemoveTruck(int id);
    }
}
