namespace Inventory.Models.DataModels.UserModels
{
    using Microsoft.AspNetCore.Identity;

    public class UserRoleClaim : IdentityRoleClaim<int>
    {
        public UserRole UserRole { get; set; }
    }
}
