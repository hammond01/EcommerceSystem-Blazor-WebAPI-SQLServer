namespace Models
{
    public class Users
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
