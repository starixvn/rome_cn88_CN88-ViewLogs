namespace Alpha.ViewLogs.Site.App_Start
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Mvc;
    using AppResources;
    using Controllers;
    using DataAccesses;
    using DataAccesses.Impl;
    using Fanex.Data.Repository;
    using Fanex.RemotingRef;
    using Handlers;
    using Helpers;
    using MediatR;
    using Services;
    using Services.Impl;
    using Services.Impl.Logs;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using Sunplus.Agent.Model.Base;
    using Sunplus.Agent.Model.Entities;
    using Utilities.IP;
    using ViewLogs.Helpers;
    using ViewLogs.Helpers.Impl;

    public static class DependencyConfig
    {
        public static void Init()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            RegisterMediator(container);

            RegisterCrossCuttingConcerns(container);

            RegisterDataAccesses(container);

            RegisterServices(container);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void RegisterMediator(Container container)
        {
            var assemblies = GetAssemblies();
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IAsyncRequestHandler<,>), assemblies);

            container.RegisterCollection(typeof(INotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(IAsyncNotificationHandler<>), assemblies);
            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton(new MultiInstanceFactory(container.GetAllInstances));
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(ViewLogBaseController).Assembly;
        }

        private static void RegisterCrossCuttingConcerns(Container container)
        {
            container.Register<IDateTime, DateTimeWrap>(Lifestyle.Singleton);
            container.Register<MicroSites, MicroSitesImpl>(Lifestyle.Singleton);
        }

        private static void RegisterServices(Container container)
        {
            container.RegisterInitializer<LogFormatterImpl>(logFormatter =>
            {
                logFormatter.ResourceManager = TrackingValueResources.ResourceManager;
            });

            container.Register<IUserSession, UserSessionExt>(Lifestyle.Scoped);

            container.Register<CustomerService, CustomerServiceImpl>(Lifestyle.Transient);
            container.Register<TransformService, TransformServiceImpl>(Lifestyle.Transient);
            container.Register<LogFormatter, LogFormatterImpl>(Lifestyle.Transient);
            container.Register<TrackingValueFormatter, TrackingValueFormatterImpl>(Lifestyle.Transient);

            container.Register<ViewLogIPService, ViewLogIPServiceImpl>(Lifestyle.Transient);
            container.Register<IPService, IPServiceImpl>(Lifestyle.Transient);
            container.RegisterSingleton<IRemotingRefService, RemotingRefService>();
            container.RegisterSingleton<RemotingRefWrapper, RemotingRefWrapperImpl>();

            container.RegisterConditional<LogService, SettingServiceImpl>(
                Lifestyle.Transient,
                c => c.Consumer.ImplementationType == typeof(SettingHandler));
            container.RegisterConditional<LogService, StatusServiceImpl>(Lifestyle.Transient, c => c.Consumer.ImplementationType == typeof(StatusHandler));
            container.RegisterConditional<LogService, CreditServiceImpl>(Lifestyle.Transient, c => c.Consumer.ImplementationType == typeof(CreditHandler));
            container.RegisterConditional<LogService, LoginServiceImpl>(Lifestyle.Transient, c => c.Consumer.ImplementationType == typeof(LoginHandler));
        }

        private static void RegisterDataAccesses(Container container)
        {
            container.Register<IDynamicRepository, DynamicRepository>(Lifestyle.Transient);
            container.RegisterConditional<LogDataAccess, LogDataAccessImpl>(
                Lifestyle.Transient,
                c =>
                    c.Consumer.ImplementationType == typeof(SettingServiceImpl)
                    || c.Consumer.ImplementationType == typeof(StatusServiceImpl)
                    || c.Consumer.ImplementationType == typeof(CreditServiceImpl));
            container.RegisterConditional<LogDataAccess, LoginDataAccessImpl>(Lifestyle.Transient, c => c.Consumer.ImplementationType == typeof(LoginServiceImpl));
        }
    }
}