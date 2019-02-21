using DotNetCore.EntityFrameworkCore;
using DotNetCoreArchitecture.Model;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Database
{
    public sealed class UserRepository : EntityFrameworkCoreRepository<UserEntity>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public Task<SignedInModel> SignInAsync(SignInModel signInModel)
        {
            return FirstOrDefaultAsync<SignedInModel>
            (
                userEntity => userEntity.Login.Equals(signInModel.Login)
                && userEntity.Password.Equals(signInModel.Password)
                && userEntity.Status == Status.Active
            );
        }
    }
}
