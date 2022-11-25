using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Truck.Management.Test.Domain.Models;

namespace Truck.Management.Test.Infra.Data.Mappings
{
    public class TruckFMMapping : IEntityTypeConfiguration<TruckFM>
    {
        public void Configure(EntityTypeBuilder<TruckFM> builder)
        {
        }
    }
}
