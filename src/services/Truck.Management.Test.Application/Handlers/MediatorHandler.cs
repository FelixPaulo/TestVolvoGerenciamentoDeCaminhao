using MediatR;
using System.Threading.Tasks;
using Truck.Management.Test.Application.Events;
using Truck.Management.Test.Application.Interfaces;

namespace Truck.Management.Test.Application.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }
    }
}
