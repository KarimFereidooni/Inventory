namespace Inventory.Models.DataModels.UserModels
{
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// ورود های خارجی کاربر مانند ورود از طریق حساب گوگل
    /// </summary>
    public class UserLogin : IdentityUserLogin<int>
    {
        public override int UserId { get => base.UserId; set => base.UserId = value; }

        public override string LoginProvider { get => base.LoginProvider; set => base.LoginProvider = value; }

        public override string ProviderDisplayName { get => base.ProviderDisplayName; set => base.ProviderDisplayName = value; }

        public override string ProviderKey { get => base.ProviderKey; set => base.ProviderKey = value; }
    }
}
