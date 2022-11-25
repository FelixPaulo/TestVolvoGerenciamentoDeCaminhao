using Microsoft.EntityFrameworkCore;
using Truck.Management.Test.Domain.Models;
using Truck.Management.Test.Infra.Data.Mappings;

namespace Truck.Management.Test.Infra.Data.Context
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<TruckFH> TruckFHs { get; set; }
        public DbSet<TruckFM> TruckFMs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TruckMapping());
            modelBuilder.ApplyConfiguration(new TruckFHMapping());
            modelBuilder.ApplyConfiguration(new TruckFMMapping());
        }

        public virtual int SaveChanges(string userName, int companyId)
        {
            var result = SaveChanges();
            return result;
        }
    }
}
