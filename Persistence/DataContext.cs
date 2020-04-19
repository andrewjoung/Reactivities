using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        // For the Value entity we created in Domain
        public DbSet<Value> Values { get; set; }

        // For the Activity Entity we created in DOmain
        public DbSet<Activity> Activities { get; set; }

        // Configure our entities (Values) as migration is being created in API 
        // Override from DBContext
        // Protected is accessable only in this class and derived classes
        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<Value>()
                .HasData(
                    new Value {Id = 1, Name = "Value 101"},
                    new Value {Id = 2, Name = "Value 102"},
                    new Value {Id = 3, Name = "Value 103"}
                );
        }
    }
}
