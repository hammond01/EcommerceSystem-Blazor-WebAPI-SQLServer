using Models.WarehouseModel;

namespace Models.ResponseModel
{
    public class ProductBathResponse
    {
        public int ProductionBatchID { get; set; }
        public string? ProductionBatchName { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int UnitID { get; set; }
        public double PriceOfBatch { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Products? Products { get; set; } = default!;
        public Units? Units { get; set; } = default!;
    }
}
