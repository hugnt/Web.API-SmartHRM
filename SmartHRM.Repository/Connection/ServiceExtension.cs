using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHRM.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository.Connection
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

           
            services.AddScoped<EmployeeRepository>();
		
            services.AddScoped<ContractRepository>();
            services.AddScoped<DepartmentRepository>();
            services.AddScoped<TaskRepository>();
            services.AddScoped<TaskDetailsRepository>();

            return services;
        }
    }
}
