using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(T entity)
             => await _dbContext.AddAsync(entity);

        public void Delete(T entity)
              =>  _dbContext.Remove(entity);

        public async Task<IReadOnlyList<T>> GetAllAsnc()
        {
            //if(typeof(T)==typeof(product))
            //    return (IEnumerable<T>) await _dbContext.Set<product>().Include(P => P.ProductBrand).Include(P => P.ProductCategory).ToListAsync();
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsnc(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T?> GetAsnc(int Id)
        {
            //if (typeof(T) == typeof(product))
            //    return await _dbContext.Set<product>().Where(P => P.Id == Id).Include(P => P.ProductBrand).Include(P => P.ProductCategory).FirstOrDefaultAsync()as T;
            return await _dbContext.Set<T>().FindAsync(Id);
        }

        public async Task<int> GetCountWithPagination(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T?> GetWithSpecAsnc(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public void Update(T entity)
               => _dbContext.Update(entity);

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), spec);
        }
    }
}
