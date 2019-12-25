namespace Inventory.Data
{
    using Inventory.Models.DataModels.UserModels;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public partial class ApplicationDbContext : IdentityDbContext<User, UserRole, int, UserClaim, UserInRole, UserLogin, UserRoleClaim, UserToken>
    {
        private readonly IConfiguration configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
             .UseSqlServer(this.configuration.GetConnectionString("DefaultConnection"))
             .AddInterceptors(new Extensions.HintCommandInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<UserInRole>().ToTable("UserInRole");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<UserRoleClaim>().ToTable("UserRoleClaim");
            modelBuilder.Entity<UserToken>().ToTable("UserToken");

            modelBuilder.Entity<User>(ModelBuilders.User.UserBuilder.Build);
            modelBuilder.Entity<UserRole>(ModelBuilders.User.UserRoleBuilder.Build);
            modelBuilder.Entity<UserClaim>(ModelBuilders.User.UserClaimBuilder.Build);
            modelBuilder.Entity<UserInRole>(ModelBuilders.User.UserInRoleBuilder.Build);
            modelBuilder.Entity<UserRoleClaim>(ModelBuilders.User.UserRoleClaimBuilder.Build);
            modelBuilder.Entity<Product>(Inventory.Data.ModelBuilders.ProductBuilder.Build);
        }
    }
}
