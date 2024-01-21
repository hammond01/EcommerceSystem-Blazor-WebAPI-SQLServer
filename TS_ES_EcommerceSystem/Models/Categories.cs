namespace Models
{
    public class Categories
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public byte[] Picture { get; set; } = null!;
    }
}
