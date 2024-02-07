namespace Models.WarehouseModel
{
    public class StockInbound
    {
        public int InboundID { get; set; }
        public DateTime DateInbound { get; set; }
        public string? ProductionBatchID { get; set; }
        public int QuantityInbound { get; set; }
        public string? Note { get; set; }
    }
}
