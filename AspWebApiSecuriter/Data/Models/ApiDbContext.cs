using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AspWebApiSecuriter.Data.Models
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Personne> People { get; set; }
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personne>(c =>
            {
                c.ToTable("Personne");
                c.Property(p => p.Name).HasMaxLength(256);
                c.Property(p => p.LastName).HasMaxLength(256);
                c.Property(p => p.DisplayId).HasMaxLength(16);
                c.HasIndex(p => p.DisplayId).IsUnique();
            });

        }
    }
}
