using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repository.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsnc(int Id);
        Task<IReadOnlyList<T>> GetAllAsnc();
        Task<IReadOnlyList<T>> GetAllWithSpecAsnc(ISpecification<T> spec);
        Task<T?> GetWithSpecAsnc(ISpecification<T> spec);
        Task<int> GetCountWithPagination(ISpecification<T> spec);
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);

    }
}
