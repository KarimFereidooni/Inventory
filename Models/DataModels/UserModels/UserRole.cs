namespace Inventory.Models.DataModels.UserModels
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    /// <summary>
    /// نقش کاربر
    /// </summary>
    public class UserRole : IdentityRole<int>
    {
        public UserRole()
        {
            this.Users = new HashSet<UserInRole>();
            this.UserRoleClaims = new HashSet<UserRoleClaim>();
        }

        public UserRole(string name, string title)
            : this()
        {
            this.Name = name;
            this.Title = title;
        }

        public override int Id { get => base.Id; set => base.Id = value; }

        /// <summary>
        /// نام
        /// </summary>
        public override string Name { get => base.Name; set => base.Name = value; }

        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// کاربران این نقش
        /// </summary>
        public ICollection<UserInRole> Users { get; set; }

        /// <summary>
        /// اطلاعات این نقش
        /// </summary>
        public ICollection<UserRoleClaim> UserRoleClaims { get; set; }
    }
}
