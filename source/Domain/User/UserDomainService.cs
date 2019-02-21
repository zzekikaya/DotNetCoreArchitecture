using DotNetCore.Security;

namespace DotNetCoreArchitecture.Domain
{
    public static class UserDomainService
    {
        public static string CreateHash(string text)
        {
            return new Hash().Create(text);
        }
    }
}
