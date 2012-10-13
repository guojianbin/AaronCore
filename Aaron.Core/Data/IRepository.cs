using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Aaron.Core.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Delete(T obj);
        void Delete(IQueryable<T> data);
        IQueryable<T> Table { get; }
        IQueryable<T> Get(Expression<Func<T, bool>> filter = null,
            int skip = 0,
            int take = 0,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);
        T GetById(object id);
    }
}
