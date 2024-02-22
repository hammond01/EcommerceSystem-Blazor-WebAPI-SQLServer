using Models.WarehouseModel;
namespace Models.ResponseModel
{
    public class StockInBoundResponse
    {
        public int InboundID { get; set; }
        public int WareHouseID { get; set; }
        public DateTime DateInbound { get; set; }
        public int ProductionBatchID { get; set; }
        public double TotalPrice { get; set; }
        public int QuantityInbound { get; set; }
        public string? Note { get; set; }
        public ProductionBatch ProductionBatch { get; set; } = default!;
    }
}
