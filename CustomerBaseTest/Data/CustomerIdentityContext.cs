using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using CustomerBaseTest.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomerBaseTest.Data
{
    public class CustomerIdentiyContext : IdentityDbContext<AppUser,AppRole,int>
    {

            readonly IConfiguration _configuration;
            public CustomerIdentiyContext(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Identity"));
            }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);
                builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            }


        }
    }

