using DotNetCore.Repositories;
using DotNetCoreArchitecture.Model;

namespace DotNetCoreArchitecture.Database
{
    public interface IUserLogRepository : IRelationalRepository<UserLogEntity> { }
}
