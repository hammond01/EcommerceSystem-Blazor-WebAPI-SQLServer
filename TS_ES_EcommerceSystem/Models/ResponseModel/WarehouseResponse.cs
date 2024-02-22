using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ResponseModel
{
    public class WarehouseResponse
    {
        public int WareHouseID { get; set; }
        public string? WarehouseName { get; set; }
        public string? Address { get; set; }
        public decimal? CostPrice { get; set; }
        public string? ProductionBatchName { get; set; }
        public string? UnitName { get; set; }
        public string? ProductName { get; set; }
        public int ActualWarehouse { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? FormattedCostPrice { get; set; }

    }
}
