using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Text;
using VehicleManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace VihicleManagementTests
{
    class SQLiteDbContextFactory : IDisposable
    {
        private DbConnection _connection;

        private DbContextOptions<AppDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;
        }

        public AppDbContext CreateContext()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                var options = CreateOptions();
                using (var context = new AppDbContext(options))
                {
                    context.Database.EnsureCreated();
                }
            }

            return new AppDbContext(CreateOptions());
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
