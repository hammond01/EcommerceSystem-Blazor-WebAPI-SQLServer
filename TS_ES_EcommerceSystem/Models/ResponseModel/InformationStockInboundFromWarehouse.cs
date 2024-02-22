namespace Models.ResponseModel
{
    public class InformationStockInboundFromWarehouse
    {
        public int InboundID { get; set; }
        public string ProductionBatchName { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string UnitName { get; set; } = default!;
        public string Note { get; set; } = default!;
        public double TotalPrice { get; set; }
        public int QuantityInbound { get; set; }
        public DateTime DateInbound { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string? FormattedCostPrice { get; set; }

    }
}
