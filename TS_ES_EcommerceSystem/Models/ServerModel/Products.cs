namespace Models
{
    public class Products
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = null!;
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public string QuantityPerUnit { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public short ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public Categories? Categories { get; set; }
        public Suppliers? Suppliers { get; set; }

    }
}
