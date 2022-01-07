using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace WebAdvert.Search
{
    public class ApplicationContainer : IServiceProvider
    {
        public static readonly ApplicationContainer Instance = new ApplicationContainer();
        private readonly Lazy<IServiceProvider> _serviceProvider;

        protected ApplicationContainer(IConfiguration config = null, ServiceProviderOptions options = null)
        {
            config ??= BuildConfigurationRoot();

            _serviceProvider = new Lazy<IServiceProvider>(() =>
                ConfigureServices(new Microsoft.Extensions.DependencyInjection.ServiceCollection(), config)
                    .BuildServiceProvider());
        }

        public object GetService(Type serviceType) => 
            _serviceProvider.Value.GetService(serviceType);

        public static IServiceCollection ConfigureServices(IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.AddSingleton<IElasticSearchService, ElasticSearchService>();

            return serviceCollection
                .AddFeatureConfiguration(config)
                .AddElasticClient(config)
                .AddLogger();
        }

        private static IConfigurationRoot BuildConfigurationRoot() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
    }
}