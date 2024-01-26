// using ElasticSearch.Models;
// using Microsoft.AspNetCore.Mvc;
// using Nest;
//
// namespace ElasticSearch.Controllers;
//
// [ApiController]
// [Route("api/product")]
// public class ProductController : ControllerBase
// {
//     private readonly IElasticClient _elasticClient;
//
//     public ProductController(IElasticClient elasticClient)
//     {
//         _elasticClient = elasticClient;
//     }
//
//     [HttpGet]
//     public async Task<IActionResult> GetProducts([FromQuery] string query)
//     {
//         var products = await _elasticClient.SearchAsync<Product>(
//             s => s.Query(q => q.Match(m => m.Field(f => f.name).Query(query)))
//         );
//
//         return Ok(products.Documents.ToList());
//     }
// }
