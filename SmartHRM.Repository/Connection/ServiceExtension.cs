using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            services.AddScoped<RoleRepository>();
            services.AddScoped<AccountRepository>();
            services.AddScoped<RefeshTokenRepository>();

            services.AddScoped<EmployeeRepository>();
            services.AddScoped<PositionRepository>();
            services.AddScoped<InsuranceRepository>();
            services.AddScoped<InsuranceDetailsRepository>();
            services.AddScoped<StatisticalRepository>();
		
            services.AddScoped<ContractRepository>();
            services.AddScoped<DepartmentRepository>();
        
            services.AddScoped<AllowanceRepository>();
            services.AddScoped<AllowanceDetailsRepository>();
            services.AddScoped<BonusRepository>();
            services.AddScoped<BonusDetailsRepository>();
            services.AddScoped<DeductionRepository>();
            services.AddScoped<DeductionDetailsRepository>();
            services.AddScoped<TimeKeepingRepository>();

            //Khai baos Appsetting map voweis cais  gif, cho pheps copy
            services.Configure<AppSetting>(configuration.GetSection("AppSettings"));
            

            var secretKey = configuration["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    //tự cấp token
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    //ký vào token
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                    ClockSkew = TimeSpan.Zero
                };
            });

            //QR
            //services.AddScoped<IQRCodeService, QRCodeService>();
            services.AddScoped<TaskRepository>();
            services.AddScoped<TaskDetailsRepository>();

            return services;
        }
    }
}
