namespace Models.WarehouseModel
{
    public class WareHouse
    {
        public int WareHouseID { get; set; }
        public double CostPrice { get; set; }
        public string? ProductionBatchID { get; set; }
        public int QuantityTotal { get; }
    }
}
