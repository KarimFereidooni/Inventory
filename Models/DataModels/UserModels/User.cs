namespace Inventory.Models.DataModels.UserModels
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// کاربر سیستم
    /// </summary>
    public class User : IdentityUser<int>
    {
        public User()
        {
            this.Roles = new HashSet<UserInRole>();
            this.UserClaims = new HashSet<UserClaim>();
            this.RegisterDateTime = DateTimeOffset.Now;
        }

        public User(int id, string email, bool emailConfirmed, string userName, string phoneNumber, bool phoneNumberConfirmed, string title, string name, string surname, string avatar, string nationCode, string idNumber, string homePhoneNumber, string workPhoneNumber, string fax, string address, string postalCode, DateTimeOffset registerDateTime, DateTimeOffset? lastLoginDateTime, DateTimeOffset? loginDateTime, DateTimeOffset? lastUpdateDateTime, string chatId, bool disabled)
        {
            this.Id = id;
            this.Email = email;
            this.NormalizedEmail = email?.ToUpper();
            this.EmailConfirmed = emailConfirmed;
            this.UserName = userName;
            this.NormalizedUserName = userName.ToUpper();
            this.PhoneNumber = phoneNumber;
            this.PhoneNumberConfirmed = phoneNumberConfirmed;
            this.Title = title;
            this.Name = name;
            this.Surname = surname;
            this.Avatar = avatar;
            this.NationCode = nationCode;
            this.IdNumber = idNumber;
            this.HomePhoneNumber = homePhoneNumber;
            this.WorkPhoneNumber = workPhoneNumber;
            this.Fax = fax;
            this.Address = address;
            this.PostalCode = postalCode;
            this.RegisterDateTime = registerDateTime;
            this.LastLoginDateTime = lastLoginDateTime;
            this.LoginDateTime = loginDateTime;
            this.LastUpdateDateTime = lastUpdateDateTime;
            this.ChatId = chatId;
            this.Disabled = disabled;
        }

        public static void Copy(User from, User to)
        {
            // to.Id = from.Id;
            to.Email = from.Email;
            to.NormalizedEmail = from.Email?.ToUpper();

            // to.EmailConfirmed = from.EmailConfirmed;
            to.UserName = from.UserName;
            to.NormalizedUserName = from.UserName.ToUpper();
            to.PhoneNumber = from.PhoneNumber;

            // to.PhoneNumberConfirmed = from.PhoneNumberConfirmed;
            to.Title = from.Title;
            to.Name = from.Name;
            to.Surname = from.Surname;
            to.Avatar = from.Avatar;
            to.NationCode = from.NationCode;
            to.IdNumber = from.IdNumber;
            to.HomePhoneNumber = from.HomePhoneNumber;
            to.WorkPhoneNumber = from.WorkPhoneNumber;
            to.Fax = from.Fax;
            to.Address = from.Address;
            to.PostalCode = from.PostalCode;

            // to.RegisterDateTime = from.RegisterDateTime;
            // to.LastLoginDateTime = from.LastLoginDateTime;
            // to.LoginDateTime = from.LoginDateTime;
            to.LastUpdateDateTime = from.LastUpdateDateTime;
            to.ChatId = from.ChatId;

            // to.Disabled = from.Disabled;
        }

        public override int Id { get => base.Id; set => base.Id = value; }

        public override string Email { get => base.Email; set => base.Email = value; }

        public override string NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }

        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }

        public override string UserName { get => base.UserName; set => base.UserName = value; }

        public override string NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }

        /// <summary>
        /// شماره موبایل
        /// </summary>
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        public override bool PhoneNumberConfirmed { get => base.PhoneNumberConfirmed; set => base.PhoneNumberConfirmed = value; }

        /// <summary>
        /// تعداد دفعاتی که رمز ورود را اشتباه وارد کرده است
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public override int AccessFailedCount { get => base.AccessFailedCount; set => base.AccessFailedCount = value; }

        /// <summary>
        /// حساب کاربر قفل شده است
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public override bool LockoutEnabled { get => base.LockoutEnabled; set => base.LockoutEnabled = value; }

        /// <summary>
        /// مدت زمان قفل شدن حساب کاربر
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public override DateTimeOffset? LockoutEnd { get => base.LockoutEnd; set => base.LockoutEnd = value; }

        /// <summary>
        /// ورود دو مرحله ای فعال است
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public override bool TwoFactorEnabled { get => base.TwoFactorEnabled; set => base.TwoFactorEnabled = value; }

        /// <summary>
        /// رمزعبور بصورت هش شده
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }

        /// <summary>
        /// عنوان - آقای، خانم، شرکت و ...
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// نام شخص یا شرکت
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// نام خانوادگی شخص حقیقی
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Title + Name + Surname
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// تصویر
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// کد ملی
        /// </summary>
        public string NationCode { get; set; }

        /// <summary>
        /// شماره شناسنامه
        /// </summary>
        public string IdNumber { get; set; }

        public string HomePhoneNumber { get; set; }

        public string WorkPhoneNumber { get; set; }

        public string Fax { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        /// <summary>
        /// تاریخ و ساعت ثبت
        /// </summary>
        public DateTimeOffset RegisterDateTime { get; set; }

        /// <summary>
        /// تاریخ و ساعت آخرین ورود
        /// </summary>
        public DateTimeOffset? LastLoginDateTime { get; set; }

        /// <summary>
        /// تاریخ و ساعت ورود
        /// </summary>
        public DateTimeOffset? LoginDateTime { get; set; }

        /// <summary>
        /// تاریخ و ساعت آخرین ویرایش
        /// </summary>
        public DateTimeOffset? LastUpdateDateTime { get; set; }

        /// <summary>
        /// شناسه کاربر در سیستم چت انلاین
        /// </summary>
        public string ChatId { get; set; }

        /// <summary>
        /// فعال یا غیرفعال بودن کاربر
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// نقش های کاربر
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<UserInRole> Roles { get; set; }

        /// <summary>
        /// سایر اطلاعات کاربر
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<UserClaim> UserClaims { get; set; }
    }
}
