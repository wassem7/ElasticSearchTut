using ElasticSearch.Models;
using Nest;

namespace ElasticSearch.Extensions;

public static class ElasticSearchExtensions
{
    public static void AddElasticSearch(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var url = configuration["ELKConfiguration:Uri"];
        var defaultIndex = configuration["ELKConfiguration:index"];
        var settings = new ConnectionSettings(new Uri(url)).PrettyJson().DefaultIndex(defaultIndex);
        // AddDefaultMappings(settings);
        var client = new ElasticClient(settings);
        services.AddSingleton<IElasticClient>(client);
        AddDefaultIndex(client, defaultIndex);
    }

    private static void AddDefaultMappings(ConnectionSettings settings)
    {
        // settings.DefaultMappingFor<Item>(
        //     i => i.Ignore(p => p.Price).Ignore(p => p.Id).Ignore(p => p.Quantity)
        // );
    }

    private static void AddDefaultIndex(IElasticClient client, string indexName)
    {
        client.Indices.Create(indexName, i => i.Map<Item>(a => a.AutoMap()));
    }
}
