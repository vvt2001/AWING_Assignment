using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWING_Assignment_Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {

            var connectionString = "Server=VVT;Database=InputHistory;User Id=vvt1508;Password=ttnh7677;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True";

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
