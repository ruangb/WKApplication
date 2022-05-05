using Microsoft.EntityFrameworkCore;
using WKDomain.Models;

namespace WKData
{
    public class WKContext : DbContext
    {
        public WKContext(DbContextOptions<WKContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
    }
}
