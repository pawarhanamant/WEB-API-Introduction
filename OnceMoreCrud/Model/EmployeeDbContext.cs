using Microsoft.EntityFrameworkCore;

namespace OnceMoreCrud.Model
{
    public class EmployeeDbContext :DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }

       public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(

                new Employee() { id =1 ,name="pawar",salary=12345}
                );
        }
    }
}
