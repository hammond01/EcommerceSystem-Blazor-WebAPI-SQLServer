namespace Models.ResponseModel
{
    public class WarehouseResponse
    {
        public int WareHouseID { get; set; }
        public string? WarehouseName { get; set; }
        public string? Address { get; set; }
        public double CostPrice { get; set; }
        public string? ProductionBatchName { get; set; }
        public string? UnitName { get; set; }
        public string? ProductName { get; set; }
        public int ActualWarehouse { get; set; }
        public int ProductionBatchID { get; set; }
        public int DetailWarehouseID { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? FormattedCostPrice { get; set; }

    }
}
