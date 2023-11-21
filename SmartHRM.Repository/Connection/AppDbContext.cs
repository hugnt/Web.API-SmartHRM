using Microsoft.EntityFrameworkCore;
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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Allowance> Allowances { get; set; }
        public DbSet<Bonus> Bonus { get; set; }
        public DbSet<AllowanceDetails> AllowanceDetails { get; set; }
        public DbSet<BonusDetails> BonusDetails { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<DeductionDetails> DeductionDetails { get; set; }

    }
}
