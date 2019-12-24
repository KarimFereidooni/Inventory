namespace Inventory.Data.ModelBuilders.User
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class UserInRoleBuilder
    {
        public static void Build(EntityTypeBuilder<Models.DataModels.UserModels.UserInRole> entity)
        {
            entity.HasOne(d => d.User)
                .WithMany(p => p.Roles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.UserRole)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
