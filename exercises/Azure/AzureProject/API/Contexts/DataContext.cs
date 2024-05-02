using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<EmailEntity> Emails { get; set; }
    }
}
