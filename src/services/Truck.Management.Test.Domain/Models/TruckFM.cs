using FluentValidation;
using System;

namespace Truck.Management.Test.Domain.Models
{
    public class TruckFM : Truck
    {
        protected TruckFM() { }

        public TruckFM(string color, int truckModel, int yearModel)
        {
            Color = color;
            TruckModel = truckModel;
            YearManufacture = DateTime.Now.Year;
            YearModel = yearModel;
        }
        public void Update(string color, int truckModel, int yearModel)
        {
            Color = color;
            TruckModel = truckModel;
            YearModel = yearModel;
        }

        public override bool IsValid()
        {
            RuleFor(c => c.Color)
             .NotEmpty().WithMessage("The core is mandatory")
             .MaximumLength(100).WithMessage("The maximum length of the color is 100");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}