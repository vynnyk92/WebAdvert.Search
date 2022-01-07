using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace WebAdvert.Search
{
    public static class ElasticClientExtension
    {
        public static IServiceCollection AddElasticClient(this IServiceCollection serviceCollection,
            IConfiguration configuration)

        {
            var url = configuration.GetValue<string>("Url");
            var settings = new ConnectionSettings(new Uri(url)).DefaultIndex("adverts");
            var elasticClient = new ElasticClient(settings);
            serviceCollection.AddSingleton<IElasticClient>(elasticClient);
            return serviceCollection;
        }
    }
}
