using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using Aaron.Core;
using Aaron.Core.Data;

namespace Aaron.Data
{
    public class ImplRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IContext _dbContext;

        private IDbSet<T> _entity;

        private IDbSet<T> Entity
        {
            get 
            {
                if (_entity == null)
                    _entity = _dbContext.Set<T>();
                
                return _entity;
            }
        }

        public ImplRepository(IContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Insert(T obj)
        {
            this.Entity.Add(obj);
            this._dbContext.SaveChanges();
        }

        public void Update(T obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            this.Entity.Attach(obj);
            this._dbContext.Entry<T>(obj).State = System.Data.EntityState.Modified;
            this._dbContext.SaveChanges();
        }

        public void Delete(object id)
        {
            var obj = this.Entity.Find(id);
            Delete(obj);
        }

        public void Delete(T obj)
        {
            this.Entity.Remove(obj);
            this._dbContext.SaveChanges();
        }

        public void Delete(IQueryable<T> data)
        {
            foreach (var entity in data)
            {
                this.Entity.Remove(entity);
            }
            this._dbContext.SaveChanges();
        }

        public IQueryable<T> Table
        {
            get
            {
                return this.Entity;
            }
        }

        public T GetById(object id)
        {
            var entity = this.Entity.Find(id);
            return entity;
        }

        public IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null,
            int skip = 0,
            int take = 0,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null)
        {
            IQueryable<T> query = Table;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (skip > 0)
            {
                query = query.Skip(skip);
            }
            if (take > 0)
            {
                query = query.Take(take);
            }
            if (orderBy != null)
            {
                return orderBy(query);
            }
            return query;
        }
    }
}
