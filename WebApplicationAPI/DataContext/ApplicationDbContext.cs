using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace WebApplicationAPI.DataContext
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public Microsoft.EntityFrameworkCore.DbSet<Employee> Employees { get; set; }
        

        //public DbSet<Year> Years { get; set; }
        //public DbSet<Courses> Courses { get; set; }
        //public DbSet<Studing> Studies { get; set; }
        //public DbSet<Teaches> Teaches { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
