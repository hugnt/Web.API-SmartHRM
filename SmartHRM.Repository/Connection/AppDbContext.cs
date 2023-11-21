﻿using Microsoft.EntityFrameworkCore;
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


      
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskDetails> TaskDetails { get; set; }


    }
}
