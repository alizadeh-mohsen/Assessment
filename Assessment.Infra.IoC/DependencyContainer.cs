
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
            //Application Layer 
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IWorkingDaysService, WorkingDaysService>();

            //Infra.Data Layer
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IWorkingDaysRepository, WorkingDaysRepository>();
        }
    }
}