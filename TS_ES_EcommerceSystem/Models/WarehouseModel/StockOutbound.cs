namespace Models.WarehouseModel
{
    public class StockOutbound
    {
        public int OutboundID { get; set; }

        public DateTime DateOutbound { get; set; }
        public int ProductionBatchID { get; set; }
        public int WarehouseID { get; set; }
        public int QuantityOutbound { get; set; }
        public string? Note { get; set; }

    }
}
