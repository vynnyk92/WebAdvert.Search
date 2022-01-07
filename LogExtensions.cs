using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;
using ILogger = Serilog.ILogger;

namespace WebAdvert.Search
{
    public static class LogExtensions
    {
		public static IServiceCollection AddLogger(this IServiceCollection serviceCollection)
        {
            var logger = new LoggerConfiguration()
                // writes to the console in clef format
                .WriteTo.Console(formatter: new CompactJsonFormatter())
                .CreateLogger();

            serviceCollection.AddSingleton<ILogger>(logger);
            return serviceCollection;
        }
	}
}
