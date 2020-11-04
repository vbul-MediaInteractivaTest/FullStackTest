using MediaInteractivaTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaInteractivaTest.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>()
                .Property(c => c.Type)
                .HasConversion<int>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
