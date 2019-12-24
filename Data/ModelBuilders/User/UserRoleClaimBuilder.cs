namespace Inventory.Data.ModelBuilders.User
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class UserRoleClaimBuilder
    {
        public static void Build(EntityTypeBuilder<Models.DataModels.UserModels.UserRoleClaim> entity)
        {
            entity.HasKey(e => new { e.Id, e.RoleId });
            entity.Property(e => e.Id)
                .UseIdentityColumn();

            entity.HasOne(d => d.UserRole)
                .WithMany(p => p.UserRoleClaims)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
