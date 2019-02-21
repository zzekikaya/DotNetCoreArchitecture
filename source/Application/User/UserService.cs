using DotNetCore.Mapping;
using DotNetCore.Objects;
using DotNetCore.Security;
using DotNetCoreArchitecture.Database;
using DotNetCoreArchitecture.Domain;
using DotNetCoreArchitecture.Model;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application
{
    public sealed class UserService : IUserService
    {
        public UserService
        (
            IDatabaseUnitOfWork databaseUnitOfWork,
            IJsonWebToken jsonWebToken,
            IUserLogService userLogService,
            IUserRepository userRepository
        )
        {
            DatabaseUnitOfWork = databaseUnitOfWork;
            JsonWebToken = jsonWebToken;
            UserLogService = userLogService;
            UserRepository = userRepository;
        }

        private IDatabaseUnitOfWork DatabaseUnitOfWork { get; }

        private IJsonWebToken JsonWebToken { get; }

        private IUserLogService UserLogService { get; }

        private IUserRepository UserRepository { get; }

        public async Task<IDataResult<long>> AddAsync(AddUserModel addUserModel)
        {
            var validation = new AddUserModelValidator().Valid(addUserModel);

            if (!validation.Success)
            {
                return new ErrorDataResult<long>(validation.Message);
            }

            var userDomain = UserDomainFactory.Create(addUserModel);
            userDomain.Add();

            var userEntity = userDomain.Map<UserEntity>();

            await UserRepository.AddAsync(userEntity);
            await DatabaseUnitOfWork.SaveChangesAsync();

            return new SuccessDataResult<long>(userEntity.UserId);
        }

        public async Task<IResult> DeleteAsync(long userId)
        {
            await UserRepository.DeleteAsync(userId);
            await DatabaseUnitOfWork.SaveChangesAsync();

            return new SuccessResult();
        }

        public async Task<PagedList<UserModel>> ListAsync(PagedListParameters parameters)
        {
            return await UserRepository.ListAsync<UserModel>(parameters);
        }

        public async Task<IEnumerable<UserModel>> ListAsync()
        {
            return await UserRepository.ListAsync<UserModel>();
        }

        public async Task<UserModel> SelectAsync(long userId)
        {
            return await UserRepository.SelectAsync<UserModel>(userId);
        }

        public async Task<IDataResult<SignedInModel>> SignInAsync(SignInModel signInModel)
        {
            var validation = new SignInModelValidator().Valid(signInModel);

            if (!validation.Success)
            {
                return new ErrorDataResult<SignedInModel>(validation.Message);
            }

            var userDomain = UserDomainFactory.Create(signInModel);
            userDomain.SignIn();

            signInModel = userDomain.Map<SignInModel>();

            var signedInModel = await UserRepository.SignInAsync(signInModel);

            validation = new SignedInModelValidator().Valid(signedInModel);

            if (!validation.Success)
            {
                return new ErrorDataResult<SignedInModel>(validation.Message);
            }

            await AddUserLogAsync(signedInModel.UserId, LogType.SignIn).ConfigureAwait(false);

            return new SuccessDataResult<SignedInModel>(signedInModel);
        }

        public async Task<IDataResult<TokenModel>> SignInJwtAsync(SignInModel signInModel)
        {
            var result = await SignInAsync(signInModel).ConfigureAwait(false);

            if (!result.Success)
            {
                return new ErrorDataResult<TokenModel>(result.Message);
            }

            var claims = new List<Claim>();
            claims.AddSub(result.Data.UserId.ToString());
            claims.AddRoles(result.Data.Roles.ToString().Split(", "));

            var jwt = JsonWebToken.Encode(claims);
            var tokenModel = new TokenModel(jwt);

            return new SuccessDataResult<TokenModel>(tokenModel);
        }

        public async Task SignOutAsync(SignOutModel signOutModel)
        {
            await AddUserLogAsync(signOutModel.UserId, LogType.SignOut).ConfigureAwait(false);
        }

        public async Task<IResult> UpdateAsync(UpdateUserModel updateUserModel)
        {
            var validation = new UpdateUserModelValidator().Valid(updateUserModel);

            if (!validation.Success)
            {
                return new ErrorResult(validation.Message);
            }

            var userEntity = await UserRepository.SelectAsync(updateUserModel.UserId);

            var userDomain = UserDomainFactory.Create(userEntity);
            userDomain.Update(updateUserModel);

            userEntity = userDomain.Map<UserEntity>();

            await UserRepository.UpdateAsync(userEntity, userEntity.UserId);
            await DatabaseUnitOfWork.SaveChangesAsync();

            return new SuccessResult();
        }

        private async Task AddUserLogAsync(long userId, LogType logType)
        {
            var userLogModel = new UserLogModel(userId, logType);

            await UserLogService.AddAsync(userLogModel);
        }
    }
}
