namespace Models.ResponseModel
{
    public class InformationStockOutboundFromWarehouse
    {
        public int OutboundID { get; set; }
        public string ProductionBatchName { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string UnitName { get; set; } = default!;
        public string Note { get; set; } = default!;
        public int QuantityOutbound { get; set; }
        public DateTime DateOutbound { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
