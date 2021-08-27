using ApplicationTZ.Core.Models;
using FileContextCore;
using Microsoft.EntityFrameworkCore;

namespace TZ.Infrastructure
{
    class PhoneContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseFileContextDatabase();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>().ToTable("custom_table");
        }
    }
}