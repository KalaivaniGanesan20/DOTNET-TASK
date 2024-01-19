using InterfaceExample;
using Microsoft.EntityFrameworkCore;
using UserData;
namespace UserDatabase
{
    public class InheritanceDbContext:DbContext
    { public DbSet<Employee> employee{get;set;}
          public DbSet<Admin> admin{get;set;}
          public DbSet<Manager> manager{get;set;}

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                //tph example by adding discriminator

                modelBuilder.Entity<Employee>()
                   .HasDiscriminator<string>("employee_type")
        .HasValue<Manager>("manager")
        .HasValue<Admin>("admin");

               //tpc example

               modelBuilder.Entity<Employee>().UseTpcMappingStrategy().ToTable("Employee");
    modelBuilder.Entity<Admin>().ToTable("admin");
    modelBuilder.Entity<Manager>().ToTable("manager");

    
            }   
    }
}
