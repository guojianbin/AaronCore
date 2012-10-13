using System;
using System.Configuration;
using System.Reflection;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aaron.Core;
using Aaron.Data.Mapping;
using Aaron.Data.Mapping.Configuration;
using Aaron.Domain;
using Aaron.Core.Data;

namespace Aaron.Data
{
    public class AaronDbContext : DbContext, IContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AaronDbContext"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public AaronDbContext(DataSettings settings)
            : base(settings.DataConnectionString)
        {
            
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //dynamically load all configuration and system configuration
            var count = 0;
            while (count < 2)
            {
                System.Type configType = Type.GetType((count == 0) ? 
                    ConfigurationManager.AppSettings["dataMapping"] :
                    "Aaron.Data.Mapping.Configuration.SettingMap, Aaron.Data");   //any of your configuration classes here
                var typesToRegister = Assembly.GetAssembly(configType).GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                    (type.BaseType.GetGenericTypeDefinition() == typeof(BaseEntityTypeConfiguration<,>) ||
                    type.BaseType.GetGenericTypeDefinition() == typeof(SEOEntityTypeConfiguration<,>) ||
                    type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)));
                foreach (var type in typesToRegister)
                {
                    dynamic configurationInstance = Activator.CreateInstance(type);
                    modelBuilder.Configurations.Add(configurationInstance);
                }
                count++;
            }
            
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Sets this instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Entries the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public new DbEntityEntry Entry<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            return base.Entry(entity);
        }
    }
}