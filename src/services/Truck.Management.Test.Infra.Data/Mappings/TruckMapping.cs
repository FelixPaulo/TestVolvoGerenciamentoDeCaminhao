using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Truck.Management.Test.Domain.Models;

namespace Truck.Management.Test.Infra.Data.Mappings
{
    public class TruckMapping : IEntityTypeConfiguration<Domain.Models.Truck>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Truck> builder)
        {
            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.RuleLevelCascadeMode);
            builder.Ignore(e => e.ClassLevelCascadeMode);
            builder.Ignore(e => e.CascadeMode);

            builder
                .ToTable("Truck")
                .HasDiscriminator(x => x.TruckModel)
                .HasValue<TruckFH>(ModelTruckEnum.FH.GetHashCode())
                .HasValue<TruckFM>(ModelTruckEnum.FM.GetHashCode());

            builder.HasKey(c => c.Id);

            builder
               .Property(c => c.Id)
               .HasColumnName("Id");

            builder
               .Property(c => c.Color)
               .HasColumnName("Color")
               .HasMaxLength(100)
               .IsRequired();
        }
    }
}
