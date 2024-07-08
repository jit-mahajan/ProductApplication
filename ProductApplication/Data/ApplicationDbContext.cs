using Microsoft.EntityFrameworkCore;
using ProductApplication.Models.Entity;

namespace ProductApplication.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }


    }
}
