using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options
      ) : base(options)
        {


        }

        public DbSet<Product> products { get; set; }

        public DbSet<User> users { get; set; }
    }
}
