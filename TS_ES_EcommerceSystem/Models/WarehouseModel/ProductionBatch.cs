namespace Models.WarehouseModel
{
    public class ProductionBatch
    {
        public string? ProductionBatchID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
