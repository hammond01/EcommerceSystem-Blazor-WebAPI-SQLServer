namespace Models.ElasticsearchModel
{
    public class EProduct
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
    }
}
