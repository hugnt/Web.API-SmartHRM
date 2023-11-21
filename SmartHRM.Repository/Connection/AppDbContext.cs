using Microsoft.EntityFrameworkCore;
using SmartHRM.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
          
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<RefeshToken> RefeshTokens { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<InsuranceDetails> InsuranceDetails { get; set; }
 

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskDetails> TaskDetails { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<Allowance> Allowances { get; set; }
        public DbSet<AllowanceDetails> AllowanceDetails { get; set; }
        public DbSet<Bonus> Bonus { get; set; }
        public DbSet<BonusDetails> BonusDetails { get; set; }
        public DbSet<Deduction> Deductions { get; set;}
        public DbSet<DeductionDetails> DeductionDetails { get; set; }
        public DbSet<TimeKeeping> TimeKeepings { get; set; }

    }
}
