
using Assessment.Application.AutoMapper;
using Assessment.Application.Interfaces;
using Assessment.Application.Services;
using Assessment.Domain.Interfaces;
using Assessment.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Assessment.Infra.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Application Layer Dependecies

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IWorkingDaysService, WorkingDaysService>();

            #endregion

            #region Infra.Data Layer Dependecies

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IWorkingDaysRepository, WorkingDaysRepository>();

            #endregion

            #region Persenation Layer Dependencies
            
            services.AddAutoMapper(typeof(AutoMapperConfiguration).Assembly);
            services.AddMemoryCache();
            
            #endregion

        }
    }
}