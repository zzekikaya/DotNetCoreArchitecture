using DotNetCore.Security;
using DotNetCoreArchitecture.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreArchitecture.Database
{
    public sealed class DatabaseContextSeed
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(new UserEntity
            {
                UserId = 1,
                Name = "Administrator",
                Surname = "Administrator",
                Email = "administrator@administrator.com",
                Login = new Hash().Create("admin"),
                Password = new Hash().Create("admin"),
                Roles = Roles.User | Roles.Admin,
                Status = Status.Active
            });
        }
    }
}
