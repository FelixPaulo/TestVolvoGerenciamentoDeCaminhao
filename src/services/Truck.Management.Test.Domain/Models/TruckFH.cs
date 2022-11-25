using FluentValidation;
using System;

namespace Truck.Management.Test.Domain.Models
{
    public class TruckFH : Truck
    {
        protected TruckFH() { }

        public TruckFH(string color, int truckModel, int yearModel, bool digitalPanel)
        {
            Color = color;
            TruckModel = truckModel;
            YearManufacture = DateTime.Now.Year;
            YearModel = yearModel;
            DigitalPanel = digitalPanel;
        }

        public bool? DigitalPanel { get; protected set; }

        public void Update(string color, int truckModel, int yearModel, bool ditialPanel)
        {
            Color = color;
            TruckModel = truckModel;
            YearModel = yearModel;
            DigitalPanel = ditialPanel;
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
