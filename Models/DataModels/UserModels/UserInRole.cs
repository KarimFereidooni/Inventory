namespace Inventory.Models.DataModels.UserModels
{
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// نقش هر کاربر
    /// </summary>
    public class UserInRole : IdentityUserRole<int>
    {
        public override int UserId { get => base.UserId; set => base.UserId = value; }

        /// <summary>
        /// کاربر
        /// </summary>
        public User User { get; set; }

        public override int RoleId { get => base.RoleId; set => base.RoleId = value; }

        /// <summary>
        /// نقش
        /// </summary>
        public UserRole UserRole { get; set; }
    }
}
