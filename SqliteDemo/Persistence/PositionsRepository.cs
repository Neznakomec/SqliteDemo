using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDemo.Persistence
{
    public class PositionsRepository
    {
        private const string DbFileName = "positions.db";

        private SqliteConnection _connection;

        public bool IsSyncEnabled { get; set; }

        public void InitializeAsync()
        {
            try
            {
                string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                string dbFolder = Path.Combine(appFolder, "db");
                string dbFilePath = Path.Combine(dbFolder, "positions.db");

                string connectionString = $"Data Source = \"{dbFilePath}\";";
                EnsureDirectoryExists(dbFolder);
                _connection = new SqliteConnection(connectionString);
                _connection.Open();
                CreateContext().Database.Migrate();
                IsSyncEnabled = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine("InitializeAsync" + exception + "Failed while creating PositionsRepository");
            }

        }

        private void EnsureDirectoryExists(string dbDirectoryPath)
        {
            if (!Directory.Exists(dbDirectoryPath))
            {
                try
                {
                    Console.WriteLine("EnsureDirectoryExists: " + $"Directory {dbDirectoryPath} doesn't exist, trying to create");
                    Directory.CreateDirectory(dbDirectoryPath);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("EnsureDirectoryExists" + exception + $"Failed to create directory {dbDirectoryPath}");
                    throw;
                }
            }
        }

        private DatabaseContext CreateContext()
        {
            return new DatabaseContext(_connection);
        }
    }
}
