using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Truck.Management.Test.Application.Interfaces;
using Truck.Management.Test.Application.Models;
using Truck.Management.Test.Application.Notifications;

namespace Truck.Management.Test.API.Controllers
{
    public class TruckController : BaseController
    {
        private readonly ITruckApplication _truckApplication;

        public TruckController(INotificationHandler<ApplicationNotification> notifications, ITruckApplication truckApplication) : base(notifications)
        {
            _truckApplication = truckApplication;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(TruckDto truckDto)
        {
            if (!ModelState.IsValid)
                return base.ResponseError(ModelState.Values);

            Console.WriteLine("Creating truck!");
            var result = await _truckApplication.AddTruck(truckDto);

            return base.ResponseResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TruckUpdateDto truckDto)
        {
            if (!ModelState.IsValid)
                return base.ResponseError(ModelState.Values);

            Console.WriteLine("Updating truck!");
            var result = await _truckApplication.UpdateTruck(truckDto);

            return base.ResponseResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _truckApplication.GetTruckById(id);
            return base.ResponseResult(result);
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await _truckApplication.ListAllTrucks();
            return base.ResponseResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _truckApplication.RemoveTruck(id);
            return base.ResponseResult(result);
        }
    }
}
