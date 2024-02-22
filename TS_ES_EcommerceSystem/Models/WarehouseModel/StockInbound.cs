namespace Models.WarehouseModel
{
    public class StockInbound
    {
        public int InboundID { get; set; }
        public int WareHouseID { get; set; }
        public DateTime DateInbound { get; set; }
        public int ProductionBatchID { get; set; }
        public int QuantityInbound { get; set; }
        public double TotalPrice { get; set; }
        public string? Note { get; set; }
    }
}
