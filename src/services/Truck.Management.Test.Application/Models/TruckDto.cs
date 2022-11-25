using System;
using System.ComponentModel.DataAnnotations;
using Truck.Management.Test.Domain.Models;

namespace Truck.Management.Test.Application.Models
{
    public class TruckDto
    {

        [Required(ErrorMessage = "A cor do caminhão é obrigatório")]
        [MaxLength(100, ErrorMessage = "Tamanho maximo para a cor é 100")]
        public string Cor { get; set; }

        [EnumDataType(typeof(ModelTruckEnum), ErrorMessage = "Só é possivel cadastrar caminhão volvo do tipo 'FH' ou 'FM'")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O ano do modelo do caminhão é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public int AnoModelo { get; set; }

        public bool? DigitalPanel { get; set; }
    }
}
