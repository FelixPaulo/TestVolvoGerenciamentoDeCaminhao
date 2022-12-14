// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Truck.Management.Test.Infra.Data.Context;

namespace Truck.Management.Test.Infra.Data.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Truck.Management.Test.Domain.Models.Truck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnName("Color")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("TruckModel")
                        .HasColumnType("int");

                    b.Property<int>("YearManufacture")
                        .HasColumnType("int");

                    b.Property<int>("YearModel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Truck");

                    b.HasDiscriminator<int>("TruckModel");
                });

            modelBuilder.Entity("Truck.Management.Test.Domain.Models.TruckFH", b =>
                {
                    b.HasBaseType("Truck.Management.Test.Domain.Models.Truck");

                    b.Property<bool?>("DigitalPanel")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DigitalPanel")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Truck.Management.Test.Domain.Models.TruckFM", b =>
                {
                    b.HasBaseType("Truck.Management.Test.Domain.Models.Truck");

                    b.HasDiscriminator().HasValue(2);
                });
#pragma warning restore 612, 618
        }
    }
}
