using InterfaceExample;
using Microsoft.EntityFrameworkCore;
using UserData;
namespace UserDatabase
{
    public class ApplicationDbContext:DbContext
    {
          public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
          {

          }
          public DbSet<User> user{get;set;}
         
            
     }
}