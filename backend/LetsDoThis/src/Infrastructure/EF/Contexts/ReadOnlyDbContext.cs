using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Contexts
{
    public class ReadOnlyDbContext : DbContext
    {
        public ReadOnlyDbContext(DbContextOptions<ReadOnlyDbContext> options)
            : base(options)
        {
        }

        public DbSet<string> Users { get; set; }
    }
}
