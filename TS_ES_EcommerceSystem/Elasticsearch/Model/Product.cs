namespace Elasticsearch.Model
{
    public class Product
    {
        public int Id { get; set; } = new Random().Next();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }

    }
}
