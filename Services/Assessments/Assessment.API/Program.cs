using Serilog;
using Serilog.Sinks.Elasticsearch;
namespace Assessments.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((ctx, lc) =>
                {
                    lc.WriteTo.Console();

                    // Elasticsearch sink configuration
                    lc.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                    {
                        AutoRegisterTemplate = true,
                        IndexFormat = "applogs-{0:yyyy.MM.dd}" 
                    });
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}