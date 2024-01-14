using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataModel
{
    public class DataModel
    {
        public class DataContext : DbContext
        {
            private const bool DoExecuteLogging = false;
            private const string MSSQL_CONNECTION
                = "Data Source=LT-LKOVARI;Initial Catalog=LKEFPlayground;Integrated Security=True;Trust Server Certificate=True";

            public DbSet<ZipCode> ZipCode { get; set; }
            // public DbSet<Address> Address { get; set; } // Will be used in the future.

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (DoExecuteLogging)
                {
                    _ = optionsBuilder.UseSqlServer(MSSQL_CONNECTION).LogTo(Console.WriteLine, new[]
                        { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
                }
                else
                {
                    optionsBuilder.UseSqlServer(MSSQL_CONNECTION);
                }
            }

            /*
            public override void Dispose()
            {
                base.Dispose();
                _streamWriter.Dispose();
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                var zipCodeList = PrepareMigrationsDataHelper.ProvideSeedData();
                modelBuilder.Entity<ZipCode>().HasData(zipCodeList);
            }
            */
        }
    }
}
