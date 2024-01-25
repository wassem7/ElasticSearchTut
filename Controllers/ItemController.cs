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

    [HttpGet]
    public async Task<IActionResult> GetItems(string keyword)
    {
        var items = await _elasticClient.SearchAsync<Item>(
            i => i.Query(a => a.QueryString(b => b.Query('*' + keyword + '*'))).Size(200)
        );

        return Ok(items.Documents.ToList());
    }

    [HttpPost("AddItems")]
    public async Task<IActionResult> AddItems(Item item)
    {
        var addedDocument = await _elasticClient.IndexDocumentAsync(item);

        return Ok(addedDocument.Result == Result.Created);
    }
}
