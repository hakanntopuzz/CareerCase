using CareerCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareerCase.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<UnfavorableWord> UnfavorableWords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<Company>().HasIndex(a => new { a.Phone }).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}