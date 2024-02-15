namespace Models.ResponseModel
{
    public class ResProductionBatch
    {
        public int ProductionBatchID { get; set; }
        public string? ProductionBatchName { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Products? Products { get; set; } = default!;
    }
}
