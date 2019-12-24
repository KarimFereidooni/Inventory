namespace Inventory.Models.DataModels.UserModels
{
    using Microsoft.AspNetCore.Identity;

    public class UserClaim : IdentityUserClaim<int>
    {
        public User User { get; set; }
    }
}
