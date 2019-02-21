using DotNetCoreArchitecture.Model;

namespace DotNetCoreArchitecture.Domain
{
    public class UserDomain
    {
        protected internal UserDomain(string login, string password)
        {
            Login = login;
            Password = password;
        }

        protected internal UserDomain
        (
            long userId,
            string name,
            string surname,
            string email,
            string login,
            string password,
            Roles roles
        )
        {
            UserId = userId;
            Name = name;
            Surname = surname;
            Email = email;
            Login = login;
            Password = password;
            Roles = roles;
        }

        public string Email { get; private set; }

        public string Login { get; private set; }

        public string Name { get; private set; }

        public string Password { get; private set; }

        public Roles Roles { get; private set; }

        public Status Status { get; private set; }

        public string Surname { get; private set; }

        public long UserId { get; private set; }

        public void Add()
        {
            Roles = Roles.User;
            Status = Status.Active;
            CreateLoginPasswordHash();
        }

        public void SignIn()
        {
            CreateLoginPasswordHash();
        }

        public void Update(UpdateUserModel updateUserModel)
        {
            Name = updateUserModel.Name;
            Surname = updateUserModel.Surname;
            Email = updateUserModel.Email;
        }

        private void CreateLoginPasswordHash()
        {
            Login = UserDomainService.CreateHash(Login);
            Password = UserDomainService.CreateHash(Password);
        }
    }
}
