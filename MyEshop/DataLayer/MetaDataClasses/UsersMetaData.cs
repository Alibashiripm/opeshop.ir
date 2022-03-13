namespace DataLayer
{
    public class UsersMetaData
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ActiveCode { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime RegisterDate { get; set; }
    }
}