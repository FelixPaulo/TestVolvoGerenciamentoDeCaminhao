using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Truck.Management.Test.Domain.Models;

namespace Truck.Management.Test.Application.Models
{
    public class TruckResultDto
    {
        [Key]
        public int Id { get; set; }
        public string Cor { get; set; }
        public string Modelo { get; set; }
        public bool? DigitalPanel { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
    }
    public static class TruckResultDtoExtensions
    {
        public static TruckResultDto MapToModel(this Domain.Models.Truck truck)
        {
            var truckDto = new TruckResultDto
            {
                Id = truck.Id,
                Cor = truck.Color,
                AnoFabricacao = truck.YearManufacture,
                AnoModelo = truck.YearModel,
            };

            if (truck.TruckModel == (int)ModelTruckEnum.FH)
            {
                var truckFHDto = (TruckFH)truck;
                truckDto.DigitalPanel = truckFHDto.DigitalPanel;
                truckDto.Modelo = $"Volvo {ModelTruckEnum.FM}";
            }
            else
            {
                truckDto.Modelo = $"Volvo {ModelTruckEnum.FH}";
            }

            return truckDto;
        }

        public static IEnumerable<TruckResultDto> MapToModels(this IEnumerable<Domain.Models.Truck> trucks)
        {
            var lstTruckDtos = new List<TruckResultDto>();
            foreach (var truck in trucks)
                lstTruckDtos.Add(truck.MapToModel());
            return lstTruckDtos;
        }
    }
}
