namespace Models.ResponseModel
{
    public class DetailWarehouseResponse
    {
        public int DetailWarehouseID { get; set; }
        public int WarehouseID { get; set; }
        public int ActualWarehouse { get; set; }
        public double CostPrice { get; set; }
        public int ProductionBatchID { get; set; }
        public string Note { get; set; } = default!;
    }
}
