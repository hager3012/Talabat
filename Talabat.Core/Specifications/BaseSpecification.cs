using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> orderBy { get ; set ; }
        public Expression<Func<T, object>> orderByDec { get ; set ; }
        public int Skip { get ; set ; }
        public int Take { get; set; }
        public bool IsPaginationEnabeld { get ; set ; }

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }
       public void AddOrderBy(Expression<Func<T, object>> expression)
        {
            orderBy = expression;
        }
        public void AddOrderByDec(Expression<Func<T, object>> expression)
        {
            orderByDec = expression;
        }
        public void AddPagination(int skip,int take)
        {
            IsPaginationEnabeld = true;
            Skip = skip;
            Take = take;
        }
    }
}
