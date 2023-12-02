using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private Hashtable _repository;
        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _repository = new Hashtable();
        }
        public async Task<int> CompleteAsync()
             => await _dbContext.SaveChangesAsync();

        public  ValueTask DisposeAsync()
          =>  _dbContext.DisposeAsync();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var Key = typeof(TEntity).Name;
            if (!_repository.ContainsKey(Key))
            {
                var repostory = new GenericRepository<TEntity>(_dbContext);
                _repository.Add(Key, repostory);
            }
            return _repository[Key] as IGenericRepository<TEntity>;
        }

    }
}
