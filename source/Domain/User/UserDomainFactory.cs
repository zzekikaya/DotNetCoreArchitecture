using DotNetCoreArchitecture.Model;

namespace DotNetCoreArchitecture.Domain
{
    public static class UserDomainFactory
    {
        public static UserDomain Create(AddUserModel addUserModel)
        {
            return new UserDomain
            (
                addUserModel.UserId,
                addUserModel.Name,
                addUserModel.Surname,
                addUserModel.Email,
                addUserModel.Login,
                addUserModel.Password,
                addUserModel.Roles
            );
        }

        public static UserDomain Create(UpdateUserModel updateUserModel)
        {
            return new UserDomain
            (
                updateUserModel.UserId,
                updateUserModel.Name,
                updateUserModel.Surname,
                updateUserModel.Email,
                null,
                null,
                updateUserModel.Roles
            );
        }

        public static UserDomain Create(UserEntity userEntity)
        {
            return new UserDomain
            (
                userEntity.UserId,
                userEntity.Name,
                userEntity.Surname,
                userEntity.Email,
                userEntity.Login,
                userEntity.Password,
                userEntity.Roles
            );
        }

        public static UserDomain Create(SignInModel signInModel)
        {
            return new UserDomain
            (
                signInModel.Login,
                signInModel.Password
            );
        }
    }
}
