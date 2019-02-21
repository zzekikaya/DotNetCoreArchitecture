using DotNetCoreArchitecture.Model;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application
{
    public interface IUserLogService
    {
        Task AddAsync(UserLogModel userLogModel);
    }
}
