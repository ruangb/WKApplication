using Microsoft.EntityFrameworkCore;
using WKDomain.Models;

namespace WKData
{
    public class WKContext : DbContext
    {
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

        public WKContext(DbContextOptions<WKContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
