using Microsoft.EntityFrameworkCore;
using AbrigoGerenciamento.Models;

namespace AbrigoGerenciamento.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Abrigo> Abrigos { get; set; }
        public DbSet<LoteAlimento> LotesAlimentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abrigo>()
                .HasMany(a => a.LotesAlimentos)
                .WithOne(l => l.Abrigo)
                .HasForeignKey(l => l.AbrigoId);
        }
    }
}