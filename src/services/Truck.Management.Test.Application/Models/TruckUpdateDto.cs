using System.ComponentModel.DataAnnotations;
using Truck.Management.Test.Domain.Models;

namespace Truck.Management.Test.Application.Models
{
    public class TruckUpdateDto
    {
        [Key]
        public int Id { get; set; }

        public string Cor { get; set; }


        [EnumDataType(typeof(ModelTruckEnum), ErrorMessage = "Só é possivel cadastrar caminhão volvo do tipo 'FH' ou 'FM'")]
        public string Modelo { get; set; }

        public int AnoModelo { get; set; }

        public bool? DigitalPanel { get; set; }
    }
}
