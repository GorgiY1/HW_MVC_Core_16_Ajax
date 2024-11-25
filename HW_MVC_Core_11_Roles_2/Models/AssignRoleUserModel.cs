namespace HW_MVC_Core_11_Roles_2.Models
{
    public class RoleModel
    {
        public string roleName { get; set; }
    }
    public class UserModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class AssignRoleUserModel
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
