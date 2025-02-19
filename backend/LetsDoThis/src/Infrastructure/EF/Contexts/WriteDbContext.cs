using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Contexts
{
    public class WriteDbContext : DbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options)
            : base(options)
        {
        }

        public DbSet<string> users { get; set; }
    }
}