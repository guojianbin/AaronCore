using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Aaron.Core.Data
{
    public interface IContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
        DbEntityEntry Entry<TEntity>(TEntity entity) where TEntity : BaseEntity;
    }
}