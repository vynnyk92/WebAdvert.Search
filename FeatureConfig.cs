using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAdvert.Search
{
    public interface IFeatureConfig
    {
        string Url { get; }
        string Test { get; }
    }

    public class FeatureConfig : IFeatureConfig
    {
        protected IConfiguration Configuration { get; set; }
        public FeatureConfig(IConfiguration config)
        {
            Configuration = config;
        }

        public string Url => Configuration.GetValue<string>("Url");
        public string Test => Configuration.GetValue<string>("Test");
    }

    public static class FeatureConfigExtension
    {
        public static IServiceCollection AddFeatureConfiguration(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            var featureConfig = new FeatureConfig(configuration);
            serviceCollection.AddSingleton<IFeatureConfig>(featureConfig);
            return serviceCollection;
        }
    }
}
