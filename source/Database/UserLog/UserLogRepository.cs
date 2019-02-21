using DotNetCore.EntityFrameworkCore;
using DotNetCoreArchitecture.Model;

namespace DotNetCoreArchitecture.Database
{
    public sealed class UserLogRepository : EntityFrameworkCoreRepository<UserLogEntity>, IUserLogRepository
    {
        public UserLogRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
