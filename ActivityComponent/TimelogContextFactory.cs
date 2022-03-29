using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using Timelog.EF;


namespace Timelog.Services
{
    class TimelogContextFactory : IDesignTimeDbContextFactory<TimelogDbContext>
    {
        public TimelogDbContext CreateDbContext(string[] args)
        {
            

            return null;
        }
    }
}
