using Nest;

namespace Elasticsearch.Model
{
    public class Product
    {
        [Keyword]
        public string? ProductID { get; set; }
        [Text]
        public string? ProductName { get; set; }
        [Number]
        public decimal UnitPrice { get; set; }

    }
}
