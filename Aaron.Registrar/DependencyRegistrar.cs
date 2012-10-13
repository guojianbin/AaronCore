//------------------------------------------------------------------------------
// The contents of this file are subject to the nopCommerce Public License Version 1.0 ("License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at  http://www.nopCommerce.com/License.aspx. 
// 
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. 
// See the License for the specific language governing rights and limitations under the License.
// 
// The Original Code is nopCommerce.
// The Initial Developer of the Original Code is NopSolutions.
// All Rights Reserved.
// 
// Contributor(s): _______. 
//------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using Aaron.Core.Infrastructure;
using Aaron.Core.Infrastructure.Config;
using Aaron.Core.Data;
using Aaron.Data;
using Aaron.Domain;

namespace Aaron.Registrar
{
    public class DependencyRegistrar : IDependencyResolver
    {
        #region Fields

        private readonly IUnityContainer _container;

        #endregion

        #region Ctor

        public DependencyRegistrar()
            : this(new UnityContainer())
        {
        }

        public DependencyRegistrar(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this._container = container;
            //configure container
            ConfigureContainer(this._container);
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Configure root container.Register types and life time managers for unity builder process
        /// </summary>
        /// <param name="container">Container to configure</param>
        protected virtual void ConfigureContainer(IUnityContainer container)
        {
            //Take into account that Types and Mappings registration could be also done using the UNITY XML configuration
            //But we prefer doing it here (C# code) because we'll catch errors at compiling time instead execution time, if any type has been written wrong.

            //Register repositories mappings
            //to be done

            //Register default cache manager            
            //container.RegisterType<ICacheManager, NopRequestCache>(new PerExecutionContextLifetimeManager());

            //Register managers(services) mappings

            //container.RegisterType<ICatalogService, CatalogService>(new UnityPerExecutionContextLifetimeManager())
            //    .RegisterType(typeof(IRepository<>), typeof(BaseAaronDbContext<>))
            //.RegisterType(typeof(AaronDbContext), typeof(BaseAaronDbContext<>));
                

            //container.RegisterType<IPosterService, PosterService>(new UnityPerExecutionContextLifetimeManager());
            //container.RegisterType<ICommentService, CommentService>(new UnityPerExecutionContextLifetimeManager());
            //container.RegisterType<IAvatarService, AvatarService>(new UnityPerExecutionContextLifetimeManager());

            var config = RegisterTypeConfig.GetConfig();
            if (config != null)
            {
                if (config.RegisterType.Count > 0)
                {
                    RegisterTypeElementColletion collection = config.RegisterType;
                    for (int i = 0; i < collection.Count; i++)
                    {
                        container.RegisterType(Type.GetType(collection[i].Interface), Type.GetType(collection[i].Class), new UnityPerExecutionContextLifetimeManager());
                            //.RegisterType(typeof(IRepository<>), typeof(BaseAaronDbContext<,>))
                            //.RegisterType(typeof(AaronDbContext), typeof(BaseAaronDbContext<,>));
                            
                    }
                    container.RegisterType(typeof(IRepository<>), typeof(ImplRepository<>));
                    container.RegisterType(typeof(IContext), typeof(AaronDbContext));
                }
            }
            //Object context

            //Connection string
            //if (ConfigurationManager.ConnectionStrings["CFDB"].ConnectionString != null)
            //{
            //    string connectionString = ConfigurationManager.ConnectionStrings["CFDB"].ConnectionString.ToString();
            //    InjectionConstructor connectionStringParam = new InjectionConstructor(connectionString);
            //    //Registering object context
            //    container.RegisterType<CFContext>(new UnityPerExecutionContextLifetimeManager(), connectionStringParam);
            //}
        }

        #endregion

        #region Methods

        /// <summary>
        /// Register instance
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="instance">Instance</param>
        public void Register<T>(T instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            _container.RegisterInstance(instance);
        }

        /// <summary>
        /// Inject
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="existing">Type</param>
        public void Inject<T>(T existing)
        {
            if (existing == null)
                throw new ArgumentNullException("existing");

            _container.BuildUp(existing);
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="type">Type</param>
        /// <returns>Result</returns>
        public T Resolve<T>(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return (T)_container.Resolve(type);
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="type">Type</param>
        /// <param name="name">Name</param>
        /// <returns>Result</returns>
        public T Resolve<T>(Type type, string name)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (name == null)
                throw new ArgumentNullException("name");

            return (T)_container.Resolve(type, name);
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Result</returns>
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="name">Name</param>
        /// <returns>Result</returns>
        public T Resolve<T>(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return _container.Resolve<T>(name);
        }

        /// <summary>
        /// Resolve all
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Result</returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            IEnumerable<T> namedInstances = _container.ResolveAll<T>();
            T unnamedInstance = default(T);

            try
            {
                unnamedInstance = _container.Resolve<T>();
            }
            catch (ResolutionFailedException)
            {
                //When default instance is missing
            }

            if (Equals(unnamedInstance, default(T)))
            {
                return namedInstances;
            }

            return new ReadOnlyCollection<T>(new List<T>(namedInstances) { unnamedInstance });
        }

        #endregion
    }
}
