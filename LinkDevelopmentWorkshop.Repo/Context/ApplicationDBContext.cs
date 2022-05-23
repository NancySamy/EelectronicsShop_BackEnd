using LinkDevelopmentWorkshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkDevelopmentWorkshop.Repo.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options):base(options)
        {
                
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
    }
}
