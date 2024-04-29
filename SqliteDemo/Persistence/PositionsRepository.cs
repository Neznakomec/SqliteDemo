using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SqliteDemo.Persistence.Entities;
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

        private DatabaseContext _databaseContext;

        public bool IsSyncEnabled { get; set; }

        public Task InitializeAsync()
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
            return Task.CompletedTask;
        }

        public Task StoreAsync(PersistedFill fill)
        {
            if (IsSyncEnabled)
            {
                var StoreAsyncDelegate = delegate (DatabaseContext ctx)
                {
                    if (fill.AccountId == 0 && fill.Account == null)
                    {
                        throw new Exception("Unable to store fill because it is not linked to an account");
                    }
                    try
                    {
                        StoreImplAsync(fill, ctx);
                        ctx.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("StoreAsync" + $"StoreAsync(PersistedFill) failed.\nFill = {fill}\nException={ex.Message}\n{ex.StackTrace}");
                        throw;
                    }
                };

                StoreAsyncDelegate(_databaseContext);
            }
            return Task.CompletedTask;
        }

        private static void StoreImplAsync(PersistedFill fill, DatabaseContext ctx)
        {
            int id = fill.Id;
            if (id == 0)
            {
                ctx.Fills.Add(fill);
                return;
            }
            PersistedFill persistedFill = ctx.Fills.FirstOrDefault((PersistedFill _) => _.Id == id);
            if (persistedFill != null)
            {
                persistedFill.AccountId = fill.AccountId;
                persistedFill.ExchangeId = fill.ExchangeId;
                persistedFill.ExchangeOrderId = fill.ExchangeOrderId;
                persistedFill.Timestamp = fill.Timestamp;
                persistedFill.AssetPath = fill.AssetPath;
                persistedFill.InstrumentPath = fill.InstrumentPath;
                persistedFill.StrategyName = fill.StrategyName;
                persistedFill.Price = fill.Price;
                persistedFill.Quantity = fill.Quantity;
                persistedFill.Type = fill.Type;
            }
            else
            {
                ctx.Fills.Add(fill);
            }
        }

        public Task StoreAsync(PersistedAccount account)
        {
            if (!IsSyncEnabled)
            {
                return Task.CompletedTask;
            }
            var StoreAsyncDelegate = delegate (DatabaseContext ctx)
            {
                int id = account.Id;
                if (id != 0)
                {
                    PersistedAccount persistedAccount = ctx.Accounts.FirstOrDefault((PersistedAccount _) => _.Id == id);
                    if (!(persistedAccount != null))
                    {
                        ctx.Accounts.Add(account);
                    }
                    else
                    {
                        persistedAccount.Name = account.Name;
                        persistedAccount.Type = account.Type;
                    }
                }
                else
                {
                    ctx.Accounts.Add(account);
                }
                ctx.SaveChanges();
            };
            StoreAsyncDelegate(_databaseContext);
            return Task.CompletedTask;
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
            if (_databaseContext == null)
            {
                _databaseContext = new DatabaseContext(_connection);
            }

            return _databaseContext;
        }
    }
}
