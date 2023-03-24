using Microsoft.EntityFrameworkCore;
using RedisWebApi.Models;

namespace RedisWebApi.Data;

class AppDbContext : DbContext
{
    public DbSet<Driver> Drivers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}