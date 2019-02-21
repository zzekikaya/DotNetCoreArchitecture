using DotNetCore.Objects;
using DotNetCoreArchitecture.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application
{
    public interface IUserService
    {
        Task<IDataResult<long>> AddAsync(AddUserModel addUserModel);

        Task<IResult> DeleteAsync(long userId);

        Task<PagedList<UserModel>> ListAsync(PagedListParameters parameters);

        Task<IEnumerable<UserModel>> ListAsync();

        Task<UserModel> SelectAsync(long userId);

        Task<IDataResult<SignedInModel>> SignInAsync(SignInModel signInModel);

        Task<IDataResult<TokenModel>> SignInJwtAsync(SignInModel signInModel);

        Task SignOutAsync(SignOutModel signOutModel);

        Task<IResult> UpdateAsync(UpdateUserModel updateUserModel);
    }
}
