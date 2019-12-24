namespace Inventory.Models.DataModels.UserModels
{
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// توکن ورود خارجی کاربر
    /// </summary>
    public class UserToken : IdentityUserToken<int>
    {
        public override int UserId { get => base.UserId; set => base.UserId = value; }

        public override string LoginProvider { get => base.LoginProvider; set => base.LoginProvider = value; }

        public override string Name { get => base.Name; set => base.Name = value; }

        public override string Value { get => base.Value; set => base.Value = value; }
    }
}
