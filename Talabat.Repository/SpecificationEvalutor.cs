using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    internal static class SpecificationEvalutor<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery,ISpecification<T> spec)
        {
            var query = InputQuery;
            if (spec.Criteria is not null)
                 query = query.Where(spec.Criteria);
            if (spec.orderBy is not null)
                 query = query.OrderBy(spec.orderBy);
            else if (spec.orderByDec is not null)
                 query = query.OrderBy(spec.orderByDec);
            if(spec.IsPaginationEnabeld)
                 query = query.Skip(spec.Skip).Take(spec.Take);
            query = spec.Includes.Aggregate(query, (CurrentQuery, Includes) => CurrentQuery.Include(Includes));
            return query;
        }
    }
}
