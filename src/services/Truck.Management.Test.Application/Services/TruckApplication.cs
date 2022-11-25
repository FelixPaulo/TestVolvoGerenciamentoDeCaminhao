using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Truck.Management.Test.Application.Interfaces;
using Truck.Management.Test.Application.Models;
using Truck.Management.Test.Application.Notifications;
using Truck.Management.Test.Domain.Interfaces;
using Truck.Management.Test.Domain.Models;

namespace Truck.Management.Test.Application.Services
{
    public class TruckApplication : BaseApplication, ITruckApplication
    {
        private readonly IMediatorHandler _mediator;
        private readonly ITruckRepository _truckRepository;

        public TruckApplication(IUnitOfWork uow, IMediatorHandler mediator, INotificationHandler<ApplicationNotification> notifications, ITruckRepository truckRepository)
            : base(uow, mediator, notifications)
        {
            _mediator = mediator;
            _truckRepository = truckRepository;
        }

        public async Task<TruckResultDto> AddTruck(TruckDto truckDto)
        {
            var modelTruckEnum = (ModelTruckEnum)Enum.Parse(typeof(ModelTruckEnum), truckDto.Modelo, true);
            Domain.Models.Truck truck;

            switch (modelTruckEnum)
            {
                case ModelTruckEnum.FH:
                    var digitalPanel = truckDto.DigitalPanel.HasValue ? truckDto.DigitalPanel.Value : false;
                    truck = new TruckFH(truckDto.Cor, (int)ModelTruckEnum.FH, truckDto.AnoModelo, digitalPanel);
                    break;
                case ModelTruckEnum.FM:
                    truck = new TruckFM(truckDto.Cor, (int)ModelTruckEnum.FM, truckDto.AnoModelo);
                    break;
                default:
                    await _mediator.PublishEvent(new ApplicationNotification($"Model of truck not found!"));
                    return null;
            }

            if(!truck.IsValidYear(truckDto.AnoModelo))
            {
                await _mediator.PublishEvent(new ApplicationNotification("Invalid truck model year!"));
                return null;
            }

            if (!truck.IsValid())
            {
                foreach (var error in truck.ValidationResult.Errors)
                    await _mediator.PublishEvent(new ApplicationNotification(error.ErrorMessage));

                return null;
            }

            await _truckRepository.AddAsync(truck);
            var isCommit = await base.Commit();
            if (isCommit)
            {
                var currenteTruck = await _truckRepository.GetTruckById(truck.Id);
                return currenteTruck.MapToModel();
            }
            return null;
        }

        public async Task<TruckResultDto> UpdateTruck(TruckUpdateDto truckDto)
        {
            var truck = await _truckRepository.GetTruckById(truckDto.Id);
            if (truck == null)
            {
                await _mediator.PublishEvent(new ApplicationNotification($"Truck with Id {truckDto.Id} not found!"));
                return null;
            }

            if (!truck.IsValidYear(truckDto.AnoModelo))
            {
                await _mediator.PublishEvent(new ApplicationNotification("Invalid truck model year!"));
                return null;
            }

            var modelTruckEnum = (ModelTruckEnum)Enum.Parse(typeof(ModelTruckEnum), truckDto.Modelo, true);
            switch (modelTruckEnum)
            {
                case ModelTruckEnum.FH:
                    if (truck.TruckModel != (int)ModelTruckEnum.FH)
                    {
                        await _mediator.PublishEvent(new ApplicationNotification($"It is not possible to change the model of the truck, the current model is {ModelTruckEnum.FM}"));
                        return null;
                    }

                    var digitalPanel = truckDto.DigitalPanel.HasValue ? truckDto.DigitalPanel.Value : false;
                    ((TruckFH)truck).Update(truckDto.Cor, (int)ModelTruckEnum.FH, truckDto.AnoModelo, digitalPanel);
                    break;
                case ModelTruckEnum.FM:

                    if (truck.TruckModel != (int)ModelTruckEnum.FM)
                    {
                        await _mediator.PublishEvent(new ApplicationNotification($"It is not possible to change the model of the truck, the current model is {ModelTruckEnum.FH}"));
                        return null;
                    }

                    ((TruckFM)truck).Update(truckDto.Cor, (int)ModelTruckEnum.FM, truckDto.AnoModelo);
                    break;
                default:
                    await _mediator.PublishEvent(new ApplicationNotification($"Model of truck not found!"));
                    return null;
            }

            _truckRepository.Update(truck);
            await base.Commit();

            var currentTruck = await _truckRepository.GetTruckById(truck.Id);
            return currentTruck.MapToModel();
        }

        public async Task<TruckResultDto> GetTruckById(int id)
        {
            var truck = await _truckRepository.GetTruckById(id);
            if (truck == null)
            {
                await _mediator.PublishEvent(new ApplicationNotification($"Truck with Id: {id} not found"));
                return null;
            }
            return truck.MapToModel();
        }

        public async Task<IEnumerable<TruckResultDto>> ListAllTrucks()
        {
            var listTrucks = await _truckRepository.ListAllTrucks();
            return listTrucks.MapToModels();
        }

        public async Task<bool> RemoveTruck(int id)
        {
            var truck = await _truckRepository.GetTruckById(id);

            if (truck == null)
            {
                await _mediator.PublishEvent(new ApplicationNotification($"The truck id {id} not found!"));
                return false;
            }

            _truckRepository.Remove(truck);
            var isCommit = await base.Commit();
            return isCommit;
        }
    }
}
