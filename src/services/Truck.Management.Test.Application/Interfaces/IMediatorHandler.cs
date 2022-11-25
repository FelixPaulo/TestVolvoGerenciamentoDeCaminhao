using System.Threading.Tasks;
using Truck.Management.Test.Application.Events;

namespace Truck.Management.Test.Application.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
    }
}
