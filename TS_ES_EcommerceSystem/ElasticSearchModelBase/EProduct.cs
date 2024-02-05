using Nest;

namespace ElasticSearchModelBase
{
    public class EProduct
    {
        [Keyword]
        public int ProductID { get; set; }
        [Text]
        public string? ProductName { get; set; }
        [Number]
        public decimal UnitPrice { get; set; }
    }
}
