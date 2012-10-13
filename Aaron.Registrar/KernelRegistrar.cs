using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ninject;
using Ninject.Syntax;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.BindingGenerators;
using Aaron.Core.Infrastructure;
using Aaron.Core.Infrastructure.Config;
using Aaron.Core.SysConfiguration;
using Aaron.Core.Data;
using Aaron.Core.Caching;
using Aaron.Core.Threading;
using Aaron.Core.Services.Configuration;
using Aaron.Core.Services.Tasks;
using Aaron.Core.Services.Messages;
using Aaron.Data;
using Aaron.Domain;

namespace Aaron.Registrar
{
    public class KernelRegistrar : IDependencyResolver
    {
        protected readonly IKernel _kernel;

        public virtual void CustomConfigureContainer(IKernel kernel){}

        public KernelRegistrar() 
            : this(new StandardKernel())
        {
        }

        public KernelRegistrar(IKernel kernel)
        {
            if (kernel == null)
                throw new ArgumentNullException("kernel");

            this._kernel = kernel;

            ConfigureContainer(this._kernel);
        }

        private void ConfigureContainer(IKernel kernel)
        {
            // get assembly folder
            Uri uri = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase));
            DirectoryInfo directoryInfo = new DirectoryInfo(uri.LocalPath);

            //data layer
            kernel.Bind<DataSettingsManager>().ToSelf();
            kernel.Bind<DataSettings>().ToMethod(x => Resolve<DataSettingsManager>().LoadSettings());
            kernel.Bind<DataProviderManager>().ToConstructor<BaseDataProviderManager>(x => new BaseDataProviderManager(x.Inject<DataSettings>()));
            kernel.Bind<IDataProvider>().ToMethod(x => (IEfDataProvider)Resolve<DataProviderManager>().LoadDataProvider());
            kernel.Bind<IEfDataProvider>().ToMethod(x => (IEfDataProvider)Resolve<DataProviderManager>().LoadDataProvider());
            //init database
            if (Resolve<DataSettings>() != null && Resolve<DataSettings>().IsValid())
            {
                var dataProvider = (IEfDataProvider)Resolve<IDataProvider>();
                dataProvider.InitConnectionFactory();
            }
            kernel.Bind<IContext>().To<AaronDbContext>().InSingletonScope().WithConstructorArgument("settings", x => Resolve<DataSettings>());

            kernel.Bind(typeof(IRepository<>)).To(typeof(ImplRepository<>)).InSingletonScope();

            //register type
            var config = RegisterTypeConfig.GetConfig();
            if (config != null)
            {
                if (config.RegisterType.Count > 0)
                {
                    RegisterTypeElementColletion collection = config.RegisterType;
                    
                    for (int i = 0; i < collection.Count; i++)
                    {
                        kernel.Bind(Type.GetType(collection[i].Interface)).To(Type.GetType(collection[i].Class)).InSingletonScope();
                    }
                }
            }

            // register type in core
            kernel.Bind<ISettingService>().To<SettingService>().InSingletonScope();

            // message binding
            kernel.Bind<IEmailSender>().To<EmailSender>().InSingletonScope();
            kernel.Bind<IEmailAccountService>().To<EmailAccountService>().InSingletonScope();
            kernel.Bind<IQueuedEmailService>().To<QueuedEmailService>().InSingletonScope();
            kernel.Bind<ITokenizer>().To<Tokenizer>().InSingletonScope();
            kernel.Bind<ICampaignService>().To<CampaignService>().InSingletonScope();

            // cache manager
            kernel.Bind<ICacheManager>().To<MemoryCacheManager>();

            // thread manager
            kernel.Bind<IThreadManager>().To<ThreadManager>();

            // Schedule Task
            kernel.Bind<IScheduleTaskService>().To<ScheduleTaskService>();

            // system configuration
            kernel.Bind(typeof(ISysConfigurationProvider<>)).To(typeof(SysConfigurationProvider<>)).InSingletonScope();

            // bind ISettings
            kernel.Bind(x => x.FromAssembliesInPath(directoryInfo.FullName).Select(type => type != null && type.IsClass && typeof(ISettings).IsAssignableFrom(type)).BindWith(new BindingSettingSource()));

            CustomConfigureContainer(kernel);
        }

        public void Register<T>(T instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            _kernel.Bind<T>();
        }

        public void Inject<T>(T existing)
        {
            if (existing == null)
                throw new ArgumentNullException("existing");

            _kernel.Inject(existing);
        }

        public T Resolve<T>(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return (T)_kernel.Get(type);
        }

        public T Resolve<T>(Type type, string name)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (name == null)
                throw new ArgumentNullException("name");

            return (T)_kernel.Get(type, name);
        }

        public T Resolve<T>()
        {
            return _kernel.Get<T>();
        }

        public T Resolve<T>(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            return _kernel.Get<T>(name);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _kernel.GetAll<T>();
        }
    }

    public class BindingTaskSource : IBindingGenerator
    {
        public IEnumerable<IBindingWhenInNamedWithOrOnSyntax<object>> CreateBindings(Type type, Ninject.Syntax.IBindingRoot bindingRoot)
        {
            if (type != null && type.IsClass && typeof(Task).IsAssignableFrom(type))
            {
                yield return bindingRoot.Bind(type).ToMethod(x => typeof(BindingTaskSource)
                .GetMethod("BindingTaskProvider", BindingFlags.NonPublic | BindingFlags.Static)
                .Invoke(null, null));
            }
        }

        static ITask BindingTaskProvider()
        {
            return IoC.Resolve<ITask>();
        }
    }

    public class BindingSettingSource : IBindingGenerator
    {
        public IEnumerable<IBindingWhenInNamedWithOrOnSyntax<object>> CreateBindings(Type type, Ninject.Syntax.IBindingRoot bindingRoot)
        {
            if (type != null && type.IsClass && typeof(ISettings).IsAssignableFrom(type))
            {
                yield return bindingRoot.Bind(type).ToMethod(x => typeof(BindingSettingSource)
                .GetMethod("BindingSettingProvider", BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(type)
                .Invoke(null, null));
            }
        }

        static TSettings BindingSettingProvider<TSettings>() where TSettings : ISettings, new()
        {
            return IoC.Resolve<ISysConfigurationProvider<TSettings>>().Settings;
        }
    }
}
