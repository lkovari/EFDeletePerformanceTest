using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace DataModel
{
    public class DataModel
    {
        public class DataContext : DbContext
        {
            private const string MSSQL_CONNECTION
                = "Data Source=LT-LKOVARI;Initial Catalog=LKEFPlayground;Integrated Security=True;Trust Server Certificate=True";

            public DbSet<ZipCode> ZipCode { get; set; }
            // public DbSet<Address> Address { get; set; } // Will be used in the future.

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(MSSQL_CONNECTION);
            }
            /*
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                var zipCodeList = PrepareMigrationsDataHelper.ProvideSeedData();
                modelBuilder.Entity<ZipCode>().HasData(zipCodeList);
            }
            */
        }
    }
}
