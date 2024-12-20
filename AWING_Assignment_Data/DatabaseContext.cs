using AWING_Assignment_Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;

namespace AWING_Assignment_Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);   
        }
        public DbSet<InputHistory> inputHistories { get; set; }
    }
}
