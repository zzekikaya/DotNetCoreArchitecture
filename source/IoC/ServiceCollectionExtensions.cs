using DotNetCore.IoC;
using DotNetCoreArchitecture.Application;
using DotNetCoreArchitecture.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNetCoreArchitecture.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHash();
            services.AddLogger(configuration);
            services.AddJsonWebToken(Guid.NewGuid().ToString(), TimeSpan.FromHours(12));

            services.AddDbContextEnsureCreatedMigrate<DatabaseContext>(options => options
                .UseSqlServer(configuration.GetConnectionString(nameof(DatabaseContext)))
                .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
            );

            services.AddClassesMatchingInterfacesFrom
            (
                typeof(IUserService).Assembly,
                typeof(IDatabaseUnitOfWork).Assembly
            );
        }
    }
}
