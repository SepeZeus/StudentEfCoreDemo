using Microsoft.EntityFrameworkCore;
using StudentEfCoreDemo.Domain.Entities;

namespace StudentEfCoreDemo.Infrastructure.Data
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Player> Players{ get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players);

            base.OnModelCreating(modelBuilder);
        }
    }
} 