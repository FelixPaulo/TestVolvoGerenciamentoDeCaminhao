using MediatR;
using System.Threading.Tasks;
using Truck.Management.Test.Application.Interfaces;
using Truck.Management.Test.Application.Notifications;
using Truck.Management.Test.Domain.Interfaces;

namespace Truck.Management.Test.Application.Services
{
    public class BaseApplication
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _mediator;
        private readonly ApplicationNotificationHandler _notifications;


        public BaseApplication(IUnitOfWork uow, IMediatorHandler mediator, INotificationHandler<ApplicationNotification> notifications)
        {
            _uow = uow;
            _mediator = mediator;
            _notifications = (ApplicationNotificationHandler)notifications;
        }

        protected async Task<bool> Commit()
        {
            if (_notifications.HasNotifications()) return false;

            if (_uow.Commit())
                return true;

            await _mediator.PublishEvent(new ApplicationNotification("Problem to register the truck!"));
            return false;
        }
    }
}