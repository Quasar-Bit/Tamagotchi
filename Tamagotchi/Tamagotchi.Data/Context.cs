using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Tamagotchi.Data.Repositories.Base.Interfaces;
using Tamagotchi.Data.Entities;

namespace Tamagotchi.Data
{
    public class Context : DbContext, IUnitOfWork
    {
        private IDbContextTransaction _dbContextTransaction;
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }

        public IDisposable BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _dbContextTransaction = Database.BeginTransaction(isolationLevel);
            return _dbContextTransaction;
        }
        public async Task<IDisposable> BeginTransactionAsync(
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
        {
            _dbContextTransaction = await Database.BeginTransactionAsync(isolationLevel, cancellationToken);
            return _dbContextTransaction;
        }
        public void CommitTransaction()
        {
            _dbContextTransaction.Commit();
        }
        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _dbContextTransaction.CommitAsync(cancellationToken);
        }
    }
}