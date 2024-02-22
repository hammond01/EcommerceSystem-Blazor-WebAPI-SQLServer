using Models.WarehouseModel;
namespace Models.ResponseModel
{
    public class StockOutBoundResponse
    {
        public int OutboundID { get; set; }
        public int WareHouseID { get; set; }
        public DateTime DateOutbound { get; set; }
        public int ProductionBatchID { get; set; }
        public int QuantityOutbound { get; set; }
        public string? Note { get; set; }
        public ProductionBatch ProductionBatch { get; set; } = default!;
    }
}
