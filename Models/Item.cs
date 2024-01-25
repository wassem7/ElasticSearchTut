using System.ComponentModel.DataAnnotations;

namespace ElasticSearch.Models;

public class Item
{
    public string Id { get; set; }

    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}
