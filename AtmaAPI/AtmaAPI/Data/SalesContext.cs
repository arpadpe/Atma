using AtmaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AtmaAPI.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options) 
            : base(options) 
        { 
        }

        public DbSet<Sale> Sales { get; set; }
    }
}
