using ElasticSearch.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ElasticSearch.Controllers;

[Route("api/item")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly ILogger<Item> _logger;

    private readonly IElasticClient _elasticClient;

    public ItemController(ILogger<Item> logger, IElasticClient elasticClient)
    {
        _logger = logger;
        _elasticClient = elasticClient;
    }

    [HttpGet("get-by-keyword")]
    public async Task<IActionResult> GetItems(string keyword)
    {
        var items = await _elasticClient.SearchAsync<Item>(
            s =>
                s.Query(
                    q =>
                        q.MultiMatch(
                            m => m.Fields(f => f.Fields("name", "description")).Query(keyword)
                        )
                )
        );

        return Ok(items.Documents.ToList());
    }

    [HttpGet("get-by-keyword-complex-query")]
    public async Task<IActionResult> GetItemsComplexQuery(string keyword)
    {
        var items = await _elasticClient.SearchAsync<Item>(
            s =>
                s.Query(
                    q =>
                        q.Bool(
                            b =>
                                b.Filter(f => f.Match(m => m.Field("tags").Query(keyword)))
                                    .Should(sh => sh.Match(m => m.Query(keyword).Field("name")))
                        )
                )
        );

        return Ok(items.Documents.ToList());
    }

    [HttpGet("get-all-items")]
    public async Task<IActionResult> GetAllItems()
    {
        var allItems = await _elasticClient.SearchAsync<Item>(s => s.Query(q => q.MatchAll()));
        return Ok(allItems.Documents.ToList());
    }

    [HttpPost("add-items")]
    public async Task<IActionResult> AddItems(Item item)
    {
        var addedDocument = await _elasticClient.IndexDocumentAsync(item);

        return Ok(addedDocument.Result == Result.Created);
    }

    [HttpDelete("delete-all-items")]
    public async Task<IActionResult> DeleteAllItems()
    {
        var deletedAllItems = await _elasticClient.DeleteByQueryAsync<Item>(
            d => d.Query(q => q.MatchAll())
        );

        return Ok(deletedAllItems.Deleted);
    }
}
