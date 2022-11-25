using System;
using Truck.Management.Test.Domain.Utils;

namespace Truck.Management.Test.Domain.Models
{
    public abstract class Truck : Entity<Truck>
    {
        public int Id { get; protected set; }
        public string Color { get; protected set; }
        public int TruckModel { get; protected set; }
        public int YearManufacture { get; protected set; }
        public int YearModel { get; protected set; }

        public bool IsValidYear(int year)
        {
            var currentYear = DateTime.Now.Year;
            var subsequentYear = currentYear++;

            if (year == currentYear || subsequentYear == year)
                return true;
            return false;
        }
    }
}
