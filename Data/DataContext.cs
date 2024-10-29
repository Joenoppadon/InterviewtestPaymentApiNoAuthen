using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentsApi.Data.Model;

namespace PaymentsApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> User {get; set;}
        public DbSet<Deposit> Deposit {get; set;}
        public DbSet<Withdraw> Withdraw {get; set;}
        public DbSet<Transfer> Transfer {get; set;}
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.Migrate();
            //Database.EnsureCreated();
        }

        //set relation
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}