using JsonPlaceholderApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JsonPlaceholderApi.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
    }
}
