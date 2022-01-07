using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Serilog;
using WebAdvert.Models.Messages;

namespace WebAdvert.Search
{
    public interface IElasticSearchService
    {
        Task IndexMessage(AdvertConfirmedMessage advertConfirmedMessage);
    }

    public class ElasticSearchService : IElasticSearchService
    {
        private readonly IElasticClient _elasticClient;
        private readonly IFeatureConfig _featureConfig;
        private readonly ILogger _logger;

        public ElasticSearchService(IElasticClient elasticClient, IFeatureConfig featureConfig, ILogger logger)
        {
            _elasticClient = elasticClient;
            _featureConfig = featureConfig;
            _logger = logger;
        }

        public Task IndexMessage(AdvertConfirmedMessage advertConfirmedMessage)
        {
            _logger.Information($"rd {_featureConfig.Test}");
            return Task.CompletedTask;
        }
    }
}
