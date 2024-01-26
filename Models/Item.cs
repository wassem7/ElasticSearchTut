using System.ComponentModel.DataAnnotations;

namespace ElasticSearch.Models;

public class Item
{
    public string Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Name { get; set; }
    public decimal Price { get; set; }

    public Boolean Is_active { get; set; }

    public DateTime Created { get; set; }

    public int In_stock { get; set; }

    public int Sold { get; set; }

    public string[] Tags { get; set; }
}
