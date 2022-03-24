﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using Timelog.EF;




namespace Timelog.WebApp.Data
{
    class TimelogWebAppContextFactory :  IDesignTimeDbContextFactory<TimelogDbContext> 
    {
        public TimelogDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            var AppConfiguration = builder.Build();

            var connectionString = AppConfiguration.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<TimelogDbContext>();


            var options = optionsBuilder
                                .UseNpgsql(connectionString, b => b.MigrationsAssembly("Timelog.WebApp"))
                                .Options;
            return new TimelogDbContext(options);
            
        }
    }
}
