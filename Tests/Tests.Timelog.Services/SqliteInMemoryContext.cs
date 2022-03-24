using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Timelog.EF;

namespace Tests.Timelog.Component
{
    public class SqliteInMemoryContext: IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<TimelogDbContext> _contextOptions;

        private readonly TimelogDbContext _context;

        public SqliteInMemoryContext()
        {
            _connection = new SqliteConnection("Filename=:memory:");

            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<TimelogDbContext>()
                .UseSqlite(_connection)
                .Options;



            _context = new TimelogDbContext(_contextOptions);
            _context.Database.EnsureCreated();
        }

        internal TimelogDbContext TestContext { get { return _context; } }
        public void Dispose() => _connection.Dispose();
    }
}
