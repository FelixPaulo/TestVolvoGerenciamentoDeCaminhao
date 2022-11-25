using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Truck.Management.Test.Domain.Models;

namespace Truck.Management.Test.Infra.Data.Mappings
{
    public class TruckFHMapping : IEntityTypeConfiguration<TruckFH>
    {
        public void Configure(EntityTypeBuilder<TruckFH> builder)
        {
            builder
                .Property(c => c.DigitalPanel)
                .HasColumnName("DigitalPanel")
                .HasDefaultValue(false);
        }
    }
}
