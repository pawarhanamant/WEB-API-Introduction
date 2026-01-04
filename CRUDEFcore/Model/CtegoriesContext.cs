using CRUDEFcore.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace CRUDEFcore.Model
{
    public class CtegoriesContext :DbContext
    {
        public CtegoriesContext(DbContextOptions options):base(options) { 

        }
        public DbSet<Ctegories> Ctegories { get; set; }

        public DbSet<ProductTbl> productTbls { get; set; }

        public DbSet<Role> roles { get; set; }

        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(

                new Role() { Id = 1,RoleName= "admin" },
                new Role() { Id = 2, RoleName = "User" }



                );

            modelBuilder.Entity<User>().HasData(

                new User() { Id= 1,Email="Pwr@gmil.com",Password= "12345" ,RoleId= 1 }
                );
        }


    }
}
