using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SampleApi.Authentication.Data
{
    public class DataContext : IdentityDbContext, IDataProtectionKeyContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    }
}