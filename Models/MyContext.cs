using Microsoft.EntityFrameworkCore;

namespace C_Sharp_Login_Registration.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        public DbSet<RegistrationUser> RegUser{get;set;}
        // public DbSet<LoginUser> RegistrationUser {get;set;}
    }
}