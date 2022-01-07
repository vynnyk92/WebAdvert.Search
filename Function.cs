using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Microsoft.Extensions.DependencyInjection;
using WebAdvert.Models.Messages;
using ILogger = Serilog.ILogger;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace WebAdvert.Search
{
    public class Function
    {
        private readonly IServiceProvider _serviceProvider;
        public Function() : this(ApplicationContainer.Instance)
        {

        }

        public Function(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task FunctionHandler(SNSEvent evnt, ILambdaContext context)
        {
            context.Logger.LogLine($"Processed record {evnt.Records.Count}");
            foreach (var record in evnt.Records)
            {
                await ProcessRecordAsync(record, context);
            }
        }

        private async Task ProcessRecordAsync(SNSEvent.SNSRecord record, ILambdaContext context)
        {
            context.Logger.LogLine($"Processed record {record.Sns.Message}");
            var elasticSearchService = _serviceProvider.GetRequiredService<IElasticSearchService>();
            await elasticSearchService.IndexMessage(new AdvertConfirmedMessage());

            // TODO: Do interesting work based on the new message
            await Task.CompletedTask;
        }
    }
}
