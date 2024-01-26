namespace ElasticSearch.Models;

public class Product
{
    public string name { get; set; }

    public int price { get; set; }

    public int in_stock { get; set; }

    public int sold { get; set; }

    public string[] tags { get; set; }

    public string description { get; set; }

    public Boolean is_active { get; set; }

    public DateOnly created { get; set; }
}
